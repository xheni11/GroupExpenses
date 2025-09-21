export const buildFullName = (firstName?: string | null, lastName?: string | null): string => {
  const parts = [firstName, lastName].filter(Boolean) as string[];
  if (parts.length === 0) {
    return 'Unknown';
  }
  return parts.join(' ');
};

export const getInitials = (firstName?: string | null, lastName?: string | null): string => {
  const firstInitial = firstName?.trim().charAt(0) ?? '';
  const lastInitial = lastName?.trim().charAt(0) ?? '';
  const combined = `${firstInitial}${lastInitial}`;
  if (combined.length === 0) {
    return '?';
  }
  return combined.toUpperCase();
};
