import { useState, useCallback } from 'react';

type ModalState = {
  isOpen: boolean;
  data: any;
};

export const useModal = <T = any>(initialData?: T) => {
  const [modalState, setModalState] = useState<ModalState>({
    isOpen: false,
    data: initialData,
  });

  const openModal = useCallback((data?: T) => {
    setModalState({
      isOpen: true,
      data: data || initialData,
    });
  }, [initialData]);

  const closeModal = useCallback(() => {
    setModalState({
      isOpen: false,
      data: initialData,
    });
  }, [initialData]);

  const updateModalData = useCallback((data: T) => {
    setModalState(prev => ({
      ...prev,
      data,
    }));
  }, []);

  return {
    isOpen: modalState.isOpen,
    data: modalState.data,
    openModal,
    closeModal,
    updateModalData,
  };
}; 