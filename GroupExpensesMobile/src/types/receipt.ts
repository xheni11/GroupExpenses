import {Participant} from './event';

export interface Receipt {
  id: number;
  eventId: number;
  name: string;
  price: number;
  priceInEur: number;
  description: string | null;
  currency: number | string;
  paidBy: Participant;
  paidFor: Participant[];
}
