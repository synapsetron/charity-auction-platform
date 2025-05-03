import { SortState, SortControls, SortDirection } from '../../hooks/useSort';

interface SortableTableHeaderProps<T> {
  field: keyof T;
  label: string;
  sortState: SortState<T>;
  sortControls: SortControls<T>;
}

export const SortableTableHeader = <T extends Record<string, any>>({
  field,
  label,
  sortState,
  sortControls,
}: SortableTableHeaderProps<T>) => {
  const { toggleSort } = sortControls;
  const isActive = sortState.field === field;
  const direction = isActive ? sortState.direction : null;

  const getSortIcon = () => {
    if (!isActive) return '↕️';
    return direction === 'asc' ? '↑' : '↓';
  };

  return (
    <th
      onClick={() => toggleSort(field)}
      style={{ cursor: 'pointer' }}
      className={isActive ? 'active-sort' : ''}
    >
      {label} {getSortIcon()}
    </th>
  );
}; 