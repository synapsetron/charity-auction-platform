import { useState, useCallback } from 'react';

export interface FilterState<T> {
  filters: T;
  searchQuery: string;
}

export interface FilterControls<T> {
  setFilters: (filters: Partial<T>) => void;
  setSearchQuery: (query: string) => void;
  resetFilters: () => void;
}

export const useFilter = <T extends Record<string, any>>(
  initialFilters: T,
  initialSearchQuery = ''
): [FilterState<T>, FilterControls<T>] => {
  const [state, setState] = useState<FilterState<T>>({
    filters: initialFilters,
    searchQuery: initialSearchQuery,
  });

  const setFilters = useCallback((newFilters: Partial<T>) => {
    setState((prev) => ({
      ...prev,
      filters: { ...prev.filters, ...newFilters },
    }));
  }, []);

  const setSearchQuery = useCallback((query: string) => {
    setState((prev) => ({
      ...prev,
      searchQuery: query,
    }));
  }, []);

  const resetFilters = useCallback(() => {
    setState({
      filters: initialFilters,
      searchQuery: initialSearchQuery,
    });
  }, [initialFilters, initialSearchQuery]);

  return [
    state,
    {
      setFilters,
      setSearchQuery,
      resetFilters,
    },
  ];
}; 