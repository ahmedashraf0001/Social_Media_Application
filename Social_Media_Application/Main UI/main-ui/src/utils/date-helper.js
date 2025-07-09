import { format, formatDistanceToNow, isToday } from 'date-fns';

export function formatPostDate(dateString) {
  if (!dateString) return "";

  // Convert "2025-07-04 14:38:12.3218033" to "2025-07-04T14:38:12Z"
  const utcString = dateString.replace(' ', 'T').split('.')[0] + 'Z';
  const utcDate = new Date(utcString);

  if (isToday(utcDate)) {
    return formatDistanceToNow(utcDate, { addSuffix: true });
  } else {
    return format(utcDate, 'd MMM');
  }
}
