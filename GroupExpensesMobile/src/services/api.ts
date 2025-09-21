import {Platform} from 'react-native';

import {EventSummary} from '../types/event';
import {Receipt} from '../types/receipt';
import {ParticipantReportRow} from '../types/report';

const DEFAULT_BASE_URL = 'http://localhost:5140';

export const API_BASE_URL =
  Platform.select({
    ios: DEFAULT_BASE_URL,
    android: 'http://10.0.2.2:5140',
    default: DEFAULT_BASE_URL,
  }) ?? DEFAULT_BASE_URL;

export const CURRENT_USER_ID = 3;

const sanitizePath = (path: string): string => (path.startsWith('/') ? path : `/${path}`);

const parseErrorMessage = (status: number, body: string | null): string => {
  if (body && body.trim().length > 0) {
    return body;
  }

  switch (status) {
    case 404:
      return 'The requested resource could not be found.';
    case 500:
      return 'The server encountered an error while processing the request.';
    default:
      return `Request failed with status ${status}.`;
  }
};

const request = async <T>(path: string, init?: RequestInit): Promise<T> => {
  const url = `${API_BASE_URL}${sanitizePath(path)}`;

  try {
    const response = await fetch(url, {
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        ...(init?.headers ?? {}),
      },
      ...init,
    });

    const text = await response.text();

    if (!response.ok) {
      throw new Error(parseErrorMessage(response.status, text));
    }

    if (!text) {
      // Avoid returning undefined when the API sends an empty body.
      return {} as T;
    }

    try {
      return JSON.parse(text) as T;
    } catch (error) {
      throw new Error('Received an unexpected response from the server.');
    }
  } catch (error) {
    if (error instanceof Error) {
      if (error.message === 'Network request failed') {
        throw new Error(
          'Unable to reach the GroupExpenses API. Make sure it is running and reachable from your device.',
        );
      }

      throw error;
    }

    throw new Error('Something went wrong while communicating with the server.');
  }
};

const requestWithFallback = async <T>(paths: string[]): Promise<T> => {
  let lastError: Error | null = null;

  for (const path of paths) {
    try {
      return await request<T>(path);
    } catch (error) {
      if (error instanceof Error) {
        lastError = error;
      } else {
        lastError = new Error('Unexpected error');
      }
    }
  }

  throw lastError ?? new Error('Request failed.');
};

export const fetchEvents = (): Promise<EventSummary[]> => request<EventSummary[]>('/Event');

export const fetchEventById = (eventId: number): Promise<EventSummary> => request<EventSummary>(`/Event/${eventId}`);

export const fetchReceiptsByEvent = (eventId: number): Promise<Receipt[]> =>
  requestWithFallback<Receipt[]>([`/Receipt/by-event/${eventId}`, `/by-event/${eventId}`]);

export const fetchReportForEvent = (userId: number, eventId: number): Promise<ParticipantReportRow[]> =>
  requestWithFallback<ParticipantReportRow[]>([
    `/Report/kredit-debit/${userId}/${eventId}`,
    `/kredit-debit/${userId}/${eventId}`,
  ]);
