export type CurrencyCode = 'EUR' | 'GBP' | 'ALL' | 'USD';

const currencyFromNumber: Record<number, CurrencyCode> = {
  1: 'EUR',
  2: 'GBP',
  3: 'ALL',
  4: 'USD',
};

const currencySymbols: Record<CurrencyCode, string> = {
  EUR: '€',
  GBP: '£',
  ALL: 'L',
  USD: '$',
};

export const toCurrencyCode = (value: number | string | null | undefined): CurrencyCode => {
  if (typeof value === 'string') {
    const uppercase = value.toUpperCase() as CurrencyCode;
    if (currencySymbols[uppercase]) {
      return uppercase;
    }
  }

  if (typeof value === 'number' && currencyFromNumber[value]) {
    return currencyFromNumber[value];
  }

  return 'EUR';
};

export const formatAmount = (amount: number): string => {
  if (Number.isNaN(amount)) {
    return '0.00';
  }

  return amount.toFixed(2);
};

export const formatCurrency = (amount: number, currency: number | string | null | undefined): string => {
  const code = toCurrencyCode(currency);
  const symbol = currencySymbols[code];
  return `${symbol}${formatAmount(amount)} ${code}`;
};
