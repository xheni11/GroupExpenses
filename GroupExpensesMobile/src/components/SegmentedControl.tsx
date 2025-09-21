import React from 'react';
import {Pressable, StyleSheet, Text, View} from 'react-native';

type Option<T extends string> = {
  value: T;
  label: string;
};

type SegmentedControlProps<T extends string> = {
  options: Option<T>[];
  value: T;
  onChange: (value: T) => void;
};

const SegmentedControl = <T extends string>({options, value, onChange}: SegmentedControlProps<T>) => (
  <View style={styles.container}>
    {options.map(option => {
      const isSelected = option.value === value;
      return (
        <Pressable
          key={option.value}
          accessibilityRole="button"
          accessibilityState={{selected: isSelected}}
          onPress={() => onChange(option.value)}
          style={({pressed}) => [
            styles.option,
            isSelected && styles.optionSelected,
            pressed && styles.optionPressed,
          ]}>
          <Text style={[styles.label, isSelected && styles.labelSelected]}>{option.label}</Text>
        </Pressable>
      );
    })}
  </View>
);

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    backgroundColor: '#edf2f7',
    borderRadius: 20,
    padding: 4,
    marginVertical: 12,
  },
  option: {
    flex: 1,
    paddingVertical: 10,
    borderRadius: 16,
    alignItems: 'center',
  },
  optionSelected: {
    backgroundColor: '#1d3557',
  },
  optionPressed: {
    opacity: 0.85,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: '#334155',
  },
  labelSelected: {
    color: '#fff',
  },
});

export default SegmentedControl;
