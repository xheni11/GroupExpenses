import React from 'react';
import {Pressable, StyleSheet, Text, View} from 'react-native';

type ErrorStateProps = {
  message: string;
  onRetry?: () => void;
  actionLabel?: string;
};

const ErrorState: React.FC<ErrorStateProps> = ({message, onRetry, actionLabel = 'Try again'}) => (
  <View style={styles.container}>
    <Text style={styles.title}>Something went wrong</Text>
    <Text style={styles.message}>{message}</Text>
    {onRetry ? (
      <Pressable
        accessibilityRole="button"
        onPress={onRetry}
        style={({pressed}) => [styles.button, pressed && styles.buttonPressed]}>
        <Text style={styles.buttonLabel}>{actionLabel}</Text>
      </Pressable>
    ) : null}
  </View>
);

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 24,
  },
  title: {
    fontSize: 18,
    fontWeight: '600',
    color: '#1d3557',
    marginBottom: 12,
    textAlign: 'center',
  },
  message: {
    fontSize: 15,
    color: '#4a5568',
    textAlign: 'center',
    marginBottom: 16,
  },
  button: {
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 20,
    backgroundColor: '#1d3557',
  },
  buttonPressed: {
    opacity: 0.8,
  },
  buttonLabel: {
    color: '#fff',
    fontWeight: '600',
  },
});

export default ErrorState;
