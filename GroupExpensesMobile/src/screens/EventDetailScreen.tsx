import React, {useCallback, useEffect, useMemo, useState} from 'react';
import {
  ActivityIndicator,
  RefreshControl,
  ScrollView,
  StyleSheet,
  Text,
  View,
} from 'react-native';

import SegmentedControl from '../components/SegmentedControl';
import ErrorState from '../components/ErrorState';
import {
  CURRENT_USER_ID,
  fetchEventById,
  fetchReceiptsByEvent,
  fetchReportForEvent,
} from '../services/api';
import {EventSummary, Participant} from '../types/event';
import {Receipt} from '../types/receipt';
import {ParticipantReportRow} from '../types/report';
import {formatAmount, formatCurrency, toCurrencyCode} from '../utils/currency';
import {buildFullName, getInitials} from '../utils/strings';

type TabValue = 'overview' | 'receipts' | 'report';

type EventDetailScreenProps = {
  eventId: number;
  initialEvent?: EventSummary | null;
};

const EventDetailScreen: React.FC<EventDetailScreenProps> = ({eventId, initialEvent = null}) => {
  const [event, setEvent] = useState<EventSummary | null>(initialEvent);
  const [receipts, setReceipts] = useState<Receipt[]>([]);
  const [reportRows, setReportRows] = useState<ParticipantReportRow[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [refreshing, setRefreshing] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState<TabValue>('overview');

  const loadData = useCallback(
    async (isRefresh = false) => {
      if (isRefresh) {
        setRefreshing(true);
      } else {
        setLoading(true);
      }

      setError(null);

      try {
        const [eventResponse, receiptsResponse, reportResponse] = await Promise.all([
          fetchEventById(eventId),
          fetchReceiptsByEvent(eventId),
          fetchReportForEvent(CURRENT_USER_ID, eventId),
        ]);

        setEvent(eventResponse);
        setReceipts(receiptsResponse);
        setReportRows(reportResponse);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load event details.');
      } finally {
        if (isRefresh) {
          setRefreshing(false);
        } else {
          setLoading(false);
        }
      }
    },
    [eventId],
  );

  useEffect(() => {
    setActiveTab('overview');
    loadData();
  }, [eventId, loadData]);

  const onRefresh = useCallback(() => {
    loadData(true);
  }, [loadData]);

  const participantList = useMemo(() => event?.participants ?? [], [event]);

  if (loading && !refreshing && !event) {
    return (
      <View style={styles.centered}>
        <ActivityIndicator color="#1d3557" size="large" />
        <Text style={styles.loadingText}>Loading event details…</Text>
      </View>
    );
  }

  if (error && !loading) {
    return <ErrorState message={error} onRetry={() => loadData()} />;
  }

  const renderOverview = () => (
    <View style={styles.section}>
      <View style={styles.sectionCard}>
        <Text style={styles.sectionTitle}>About this event</Text>
        {event?.details ? (
          <Text style={styles.sectionBody}>{event.details}</Text>
        ) : (
          <Text style={styles.placeholder}>No description provided.</Text>
        )}
        {event?.location ? (
          <View style={styles.infoRow}>
            <Text style={styles.infoLabel}>Location</Text>
            <Text style={styles.infoValue}>{event.location}</Text>
          </View>
        ) : null}
      </View>

      <View style={styles.sectionCard}>
        <Text style={styles.sectionTitle}>Participants</Text>
        {participantList && participantList.length > 0 ? (
          participantList.map(participant => (
            <View key={participant.id} style={styles.participantRow}>
              <View style={styles.avatar}>
                <Text style={styles.avatarLabel}>
                  {getInitials(participant.firstName, participant.lastName)}
                </Text>
              </View>
              <Text style={styles.participantName}>
                {buildFullName(participant.firstName, participant.lastName)}
              </Text>
            </View>
          ))
        ) : (
          <Text style={styles.placeholder}>No participants added yet.</Text>
        )}
      </View>
    </View>
  );

  const renderReceipts = () => (
    <View style={styles.section}>
      {receipts.length === 0 ? (
        <View style={styles.sectionCard}>
          <Text style={styles.placeholder}>No receipts have been recorded for this event.</Text>
        </View>
      ) : (
        receipts.map(receipt => (
          <View key={receipt.id} style={styles.sectionCard}>
            <View style={styles.receiptHeader}>
              <Text style={styles.receiptTitle}>{receipt.name}</Text>
              <Text style={styles.receiptAmount}>{formatCurrency(receipt.price, receipt.currency)}</Text>
            </View>
            {receipt.description ? (
              <Text style={styles.sectionBody}>{receipt.description}</Text>
            ) : (
              <Text style={styles.placeholder}>No description</Text>
            )}
            <View style={styles.infoRow}>
              <Text style={styles.infoLabel}>Paid by</Text>
              <Text style={styles.infoValue}>
                {buildFullName(receipt.paidBy?.firstName, receipt.paidBy?.lastName)}
              </Text>
            </View>
            <View style={styles.infoRow}>
              <Text style={styles.infoLabel}>Shared with</Text>
              <Text style={styles.infoValue}>
                {formatParticipants(receipt.paidFor)}
              </Text>
            </View>
            <Text style={styles.receiptConversion}>
              ≈ {formatAmount(receipt.priceInEur)} € (converted from {toCurrencyCode(receipt.currency)})
            </Text>
          </View>
        ))
      )}
    </View>
  );

  const renderReport = () => (
    <View style={styles.section}>
      <View style={[styles.sectionCard, styles.reportCard]}>
        <Text style={styles.sectionTitle}>Balance with participants</Text>
        {reportRows.length === 0 ? (
          <Text style={styles.placeholder}>No report data is available yet.</Text>
        ) : (
          <View>
            <View style={[styles.reportRow, styles.reportHeaderRow]}>
              <Text style={[styles.reportCell, styles.reportHeaderText]}>Participant</Text>
              <Text style={[styles.reportCell, styles.reportHeaderText]}>Debit</Text>
              <Text style={[styles.reportCell, styles.reportHeaderText]}>Credit</Text>
              <Text style={[styles.reportCell, styles.reportHeaderText]}>Total</Text>
            </View>
            {reportRows.map(row => (
              <View key={row.participantName} style={styles.reportRow}>
                <Text style={styles.reportCell}>{row.participantName}</Text>
                <Text style={styles.reportCell}>{formatAmount(row.debit)}</Text>
                <Text style={styles.reportCell}>{formatAmount(row.kredit)}</Text>
                <Text
                  style={[
                    styles.reportCell,
                    row.total > 0 ? styles.reportPositive : row.total < 0 ? styles.reportNegative : null,
                  ]}>
                  {formatAmount(row.total)}
                </Text>
              </View>
            ))}
            <Text style={styles.reportHint}>
              Positive totals mean others owe you money, negative totals mean you owe them.
            </Text>
          </View>
        )}
      </View>
    </View>
  );

  return (
    <ScrollView
      contentContainerStyle={styles.scrollContent}
      refreshControl={<RefreshControl refreshing={refreshing} onRefresh={onRefresh} tintColor="#1d3557" />}>
      <SegmentedControl
        options={[
          {value: 'overview', label: 'Overview'},
          {value: 'receipts', label: 'Receipts'},
          {value: 'report', label: 'Report'},
        ]}
        value={activeTab}
        onChange={setActiveTab}
      />

      {activeTab === 'overview' && renderOverview()}
      {activeTab === 'receipts' && renderReceipts()}
      {activeTab === 'report' && renderReport()}
    </ScrollView>
  );
};

const formatParticipants = (participants?: Participant[]): string => {
  if (!participants || participants.length === 0) {
    return '—';
  }

  return participants
    .map(participant => buildFullName(participant.firstName, participant.lastName))
    .join(', ');
};

const styles = StyleSheet.create({
  scrollContent: {
    paddingHorizontal: 16,
    paddingBottom: 32,
  },
  centered: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 24,
  },
  loadingText: {
    marginTop: 16,
    color: '#4a5568',
    fontSize: 15,
  },
  section: {
    gap: 16,
  },
  sectionCard: {
    backgroundColor: '#fff',
    borderRadius: 16,
    padding: 20,
    shadowColor: '#000',
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: {width: 0, height: 2},
    elevation: 2,
  },
  sectionTitle: {
    fontSize: 16,
    fontWeight: '700',
    color: '#1d3557',
    marginBottom: 12,
  },
  sectionBody: {
    fontSize: 14,
    color: '#475569',
    lineHeight: 20,
  },
  placeholder: {
    fontSize: 14,
    color: '#94a3b8',
  },
  infoRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 12,
  },
  infoLabel: {
    fontSize: 13,
    color: '#64748b',
  },
  infoValue: {
    fontSize: 13,
    color: '#1f2937',
    flexShrink: 1,
    textAlign: 'right',
  },
  participantRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 12,
  },
  avatar: {
    width: 36,
    height: 36,
    borderRadius: 18,
    backgroundColor: '#e2e8f0',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 12,
  },
  avatarLabel: {
    fontWeight: '700',
    color: '#1d3557',
  },
  participantName: {
    fontSize: 14,
    color: '#1f2937',
  },
  receiptHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 8,
  },
  receiptTitle: {
    fontSize: 16,
    fontWeight: '700',
    color: '#1d3557',
  },
  receiptAmount: {
    fontSize: 15,
    fontWeight: '700',
    color: '#0f766e',
  },
  receiptConversion: {
    marginTop: 12,
    fontSize: 12,
    color: '#64748b',
  },
  reportCard: {
    paddingHorizontal: 16,
    paddingVertical: 20,
  },
  reportRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 8,
    borderBottomWidth: StyleSheet.hairlineWidth,
    borderBottomColor: '#e2e8f0',
  },
  reportHeaderRow: {
    borderBottomWidth: 1,
    borderBottomColor: '#cbd5f5',
  },
  reportCell: {
    flex: 1,
    fontSize: 13,
    color: '#1f2937',
  },
  reportHeaderText: {
    fontWeight: '700',
    color: '#1d3557',
  },
  reportPositive: {
    color: '#15803d',
  },
  reportNegative: {
    color: '#b91c1c',
  },
  reportHint: {
    fontSize: 12,
    color: '#64748b',
    marginTop: 12,
  },
});

export default EventDetailScreen;
