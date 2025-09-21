export interface Participant {
  id: number;
  firstName: string;
  lastName: string;
}

export interface EventSummary {
  id: number;
  name: string;
  details: string | null;
  location?: string | null;
  participants?: Participant[] | null;
}
