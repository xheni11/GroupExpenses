import React, {useMemo, useState} from 'react';
import {Pressable, SafeAreaView, StatusBar, StyleSheet, Text, View} from 'react-native';

import EventDetailScreen from './src/screens/EventDetailScreen';
import EventListScreen from './src/screens/EventListScreen';
import {EventSummary} from './src/types/event';

type ScreenState =
  | {key: 'list'}
  | {
      key: 'details';
      event: EventSummary;
    };

const App = (): React.JSX.Element => {
  const [screen, setScreen] = useState<ScreenState>({key: 'list'});

  const headerTitle = useMemo(() => {
    if (screen.key === 'details') {
      return screen.event.name;
    }

    return 'Group Expenses';
  }, [screen]);

  const showBackButton = screen.key === 'details';

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#1d3557" />
      <View style={styles.header}>
        <View style={styles.headerSide}>
          {showBackButton ? (
            <Pressable
              accessibilityRole="button"
              onPress={() => setScreen({key: 'list'})}
              style={({pressed}) => [styles.backButton, pressed && styles.backButtonPressed]}>
              <Text style={styles.backButtonText}>‹ Events</Text>
            </Pressable>
          ) : null}
        </View>
        <Text numberOfLines={1} style={styles.headerTitle}>
          {headerTitle}
        </Text>
        <View style={styles.headerSide} />
      </View>
      <View style={styles.body}>
        {screen.key === 'list' ? (
          <EventListScreen onSelectEvent={event => setScreen({key: 'details', event})} />
        ) : (
          <EventDetailScreen eventId={screen.event.id} initialEvent={screen.event} />
        )}
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#f8fafc',
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: '#1d3557',
  },
  headerSide: {
    width: 90,
    alignItems: 'flex-start',
  },
  headerTitle: {
    flex: 1,
    color: '#fff',
    fontSize: 18,
    fontWeight: '700',
    textAlign: 'center',
    paddingHorizontal: 8,
  },
  backButton: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 16,
  },
  backButtonPressed: {
    opacity: 0.8,
  },
  backButtonText: {
    color: '#f1f5f9',
    fontSize: 14,
  },
  body: {
    flex: 1,
  },
});

export default App;
