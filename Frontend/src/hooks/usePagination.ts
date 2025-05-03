import { useState, useCallback } from 'react';

export interface PaginationState {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
}

export interface PaginationControls {
  setCurrentPage: (page: number) => void;
  setItemsPerPage: (itemsPerPage: number) => void;
  setTotalItems: (totalItems: number) => void;
  nextPage: () => void;
  prevPage: () => void;
  goToPage: (page: number) => void;
  resetPagination: () => void;
}

export const usePagination = (
  defaultItemsPerPage = 10
): [PaginationState, PaginationControls] => {
  const [state, setState] = useState<PaginationState>({
    currentPage: 1,
    itemsPerPage: defaultItemsPerPage,
    totalItems: 0,
  });

  const setCurrentPage = useCallback((page: number) => {
    setState((prev) => ({
      ...prev,
      currentPage: page,
    }));
  }, []);

  const setItemsPerPage = useCallback((itemsPerPage: number) => {
    setState((prev) => ({
      ...prev,
      itemsPerPage,
      currentPage: 1, // Reset to first page when changing items per page
    }));
  }, []);

  const setTotalItems = useCallback((totalItems: number) => {
    setState((prev) => ({
      ...prev,
      totalItems,
    }));
  }, []);

  const nextPage = useCallback(() => {
    setState((prev) => ({
      ...prev,
      currentPage: Math.min(
        prev.currentPage + 1,
        Math.ceil(prev.totalItems / prev.itemsPerPage)
      ),
    }));
  }, []);

  const prevPage = useCallback(() => {
    setState((prev) => ({
      ...prev,
      currentPage: Math.max(prev.currentPage - 1, 1),
    }));
  }, []);

  const goToPage = useCallback((page: number) => {
    setState((prev) => ({
      ...prev,
      currentPage: Math.min(
        Math.max(page, 1),
        Math.ceil(prev.totalItems / prev.itemsPerPage)
      ),
    }));
  }, []);

  const resetPagination = useCallback(() => {
    setState({
      currentPage: 1,
      itemsPerPage: defaultItemsPerPage,
      totalItems: 0,
    });
  }, [defaultItemsPerPage]);

  return [
    state,
    {
      setCurrentPage,
      setItemsPerPage,
      setTotalItems,
      nextPage,
      prevPage,
      goToPage,
      resetPagination,
    },
  ];
}; 