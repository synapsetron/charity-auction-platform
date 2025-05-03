import { useCallback } from 'react';
import { usePagination } from './usePagination';
import { useFilter } from './useFilter';
import { useSort } from './useSort';

export const useTableState = <T extends Record<string, any>>(
  initialFilters: T,
  initialSortField: keyof T,
  defaultItemsPerPage = 10
) => {
  const [paginationState, paginationControls] = usePagination(defaultItemsPerPage);
  const [filterState, filterControls] = useFilter(initialFilters);
  const [sortState, sortControls] = useSort(initialSortField);

  const resetAll = useCallback(() => {
    paginationControls.resetPagination();
    filterControls.resetFilters();
    sortControls.resetSort();
  }, [paginationControls, filterControls, sortControls]);

  return {
    pagination: {
      state: paginationState,
      controls: paginationControls,
    },
    filter: {
      state: filterState,
      controls: filterControls,
    },
    sort: {
      state: sortState,
      controls: sortControls,
    },
    resetAll,
  };
}; 