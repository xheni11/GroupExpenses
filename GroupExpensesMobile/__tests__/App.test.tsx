/**
 * @format
 */

import 'react-native';
import React from 'react';
import App from '../App';

// Note: import explicitly to use the types shipped with jest.
import {beforeEach, afterEach, it, jest} from '@jest/globals';

// Note: test renderer must be required after react-native.
import renderer, {act} from 'react-test-renderer';

beforeEach(() => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: true,
      status: 200,
      text: () => Promise.resolve(JSON.stringify([])),
    }),
  ) as jest.Mock;
});

afterEach(() => {
  jest.resetAllMocks();
});

it('renders correctly', async () => {
  await act(async () => {
    renderer.create(<App />);
    await Promise.resolve();
  });
});
