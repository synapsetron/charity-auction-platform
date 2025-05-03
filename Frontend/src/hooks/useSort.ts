import { useState, useCallback } from 'react';

export type SortDirection = 'asc' | 'desc';

export interface SortState<T> {
  field: keyof T;
  direction: SortDirection;
}

export interface SortControls<T> {
  setSort: (field: keyof T, direction?: SortDirection) => void;
  toggleSort: (field: keyof T) => void;
  resetSort: () => void;
}

export const useSort = <T extends Record<string, any>>(
  initialField: keyof T,
  initialDirection: SortDirection = 'asc'
): [SortState<T>, SortControls<T>] => {
  const [state, setState] = useState<SortState<T>>({
    field: initialField,
    direction: initialDirection,
  });

  const setSort = useCallback((field: keyof T, direction?: SortDirection) => {
    setState({
      field,
      direction: direction || 'asc',
    });
  }, []);

  const toggleSort = useCallback((field: keyof T) => {
    setState((prev) => ({
      field,
      direction: prev.field === field && prev.direction === 'asc' ? 'desc' : 'asc',
    }));
  }, []);

  const resetSort = useCallback(() => {
    setState({
      field: initialField,
      direction: initialDirection,
    });
  }, [initialField, initialDirection]);

  return [
    state,
    {
      setSort,
      toggleSort,
      resetSort,
    },
  ];
}; 