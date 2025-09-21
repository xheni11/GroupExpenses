import React, {useCallback, useEffect, useMemo, useState} from 'react';
import {
  ActivityIndicator,
  FlatList,
  ListRenderItemInfo,
  Pressable,
  RefreshControl,
  StyleSheet,
  Text,
  View,
} from 'react-native';

import ErrorState from '../components/ErrorState';
import {fetchEvents} from '../services/api';
import {EventSummary} from '../types/event';

type EventListScreenProps = {
  onSelectEvent: (event: EventSummary) => void;
};

const EventListScreen: React.FC<EventListScreenProps> = ({onSelectEvent}) => {
  const [events, setEvents] = useState<EventSummary[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [refreshing, setRefreshing] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const loadEvents = useCallback(
    async (isRefresh = false) => {
      if (isRefresh) {
        setRefreshing(true);
      } else {
        setLoading(true);
      }

      setError(null);

      try {
        const data = await fetchEvents();
        setEvents(data);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Unable to load events.');
      } finally {
        if (isRefresh) {
          setRefreshing(false);
        } else {
          setLoading(false);
        }
      }
    },
    [],
  );

  useEffect(() => {
    loadEvents();
  }, [loadEvents]);

  const onRefresh = useCallback(() => {
    loadEvents(true);
  }, [loadEvents]);

  const renderItem = useCallback(
    ({item}: ListRenderItemInfo<EventSummary>) => {
      const participantCount = item.participants?.length ?? 0;
      const participantLabel = `${participantCount} participant${participantCount === 1 ? '' : 's'}`;

      return (
        <Pressable
          accessibilityRole="button"
          onPress={() => onSelectEvent(item)}
          style={({pressed}) => [styles.card, pressed && styles.cardPressed]}>
          <Text style={styles.cardTitle}>{item.name}</Text>
          {item.details ? (
            <Text numberOfLines={2} style={styles.cardSubtitle}>
              {item.details}
            </Text>
          ) : null}
          <View style={styles.cardFooter}>
            {item.location ? <Text style={styles.cardMeta}>{item.location}</Text> : null}
            <Text style={styles.cardMeta}>{participantLabel}</Text>
          </View>
        </Pressable>
      );
    },
    [onSelectEvent],
  );

  const listEmptyComponent = useMemo(() => {
    if (loading) {
      return null;
    }

    return (
      <View style={styles.emptyContainer}>
        <Text style={styles.emptyTitle}>No events yet</Text>
        <Text style={styles.emptyMessage}>
          Create an event in the web application to start tracking receipts and see them here.
        </Text>
      </View>
    );
  }, [loading]);

  if (loading && events.length === 0 && !refreshing) {
    return (
      <View style={styles.centered}>
        <ActivityIndicator color="#1d3557" size="large" />
        <Text style={styles.loadingText}>Loading events…</Text>
      </View>
    );
  }

  if (error && events.length === 0) {
    return <ErrorState message={error} onRetry={() => loadEvents()} />;
  }

  return (
    <FlatList
      data={events}
      keyExtractor={item => item.id.toString()}
      renderItem={renderItem}
      contentContainerStyle={events.length === 0 ? styles.flatListEmptyContent : styles.flatListContent}
      refreshControl={<RefreshControl refreshing={refreshing} onRefresh={onRefresh} tintColor="#1d3557" />}
      ListEmptyComponent={listEmptyComponent}
    />
  );
};

const styles = StyleSheet.create({
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
  card: {
    backgroundColor: '#fff',
    borderRadius: 16,
    padding: 20,
    marginHorizontal: 16,
    marginVertical: 8,
    shadowColor: '#000',
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: {width: 0, height: 2},
    elevation: 2,
  },
  cardPressed: {
    opacity: 0.85,
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#1d3557',
    marginBottom: 8,
  },
  cardSubtitle: {
    fontSize: 14,
    color: '#475569',
    marginBottom: 16,
  },
  cardFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  cardMeta: {
    fontSize: 12,
    color: '#64748b',
  },
  emptyContainer: {
    paddingHorizontal: 32,
    paddingTop: 48,
    alignItems: 'center',
  },
  emptyTitle: {
    fontSize: 18,
    fontWeight: '600',
    color: '#1d3557',
    marginBottom: 12,
  },
  emptyMessage: {
    fontSize: 14,
    color: '#475569',
    textAlign: 'center',
    lineHeight: 20,
  },
  flatListContent: {
    paddingVertical: 12,
    paddingBottom: 24,
  },
  flatListEmptyContent: {
    flexGrow: 1,
    justifyContent: 'center',
  },
});

export default EventListScreen;
