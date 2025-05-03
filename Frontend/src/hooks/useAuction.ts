import { useCallback } from 'react';
import { createAuction, getAuctionById, updateAuction, getMyAuctions } from '../api/auction';
import { AuctionResponseWithBidsDTO } from '../types/auctionTypes';
import { useLoading } from './useLoading';
import { useNotification } from './useNotification';

export const useAuction = () => {
  const { loading, error, startLoading, stopLoading, setErrorState } = useLoading();
  const { showNotification } = useNotification();

  const fetchMyAuctions = useCallback(async () => {
    try {
      startLoading();
      const data = await getMyAuctions();
      return data;
    } catch (error) {
      console.error("Помилка при завантаженні аукціонів", error);
      setErrorState("Не вдалося завантажити аукціони");
      showNotification('error', 'Не вдалося завантажити аукціони');
      throw error;
    } finally {
      stopLoading();
    }
  }, [startLoading, stopLoading, setErrorState, showNotification]);

  const fetchAuctionById = useCallback(async (auctionId: string) => {
    try {
      startLoading();
      const auction = await getAuctionById(auctionId);
      return auction;
    } catch (error) {
      console.error('Помилка при завантаженні аукціону', error);
      setErrorState('Не вдалося завантажити дані аукціону');
      showNotification('error', 'Не вдалося завантажити дані аукціону');
      throw error;
    } finally {
      stopLoading();
    }
  }, [startLoading, stopLoading, setErrorState, showNotification]);

  const createNewAuction = useCallback(async (formData: any) => {
    try {
      startLoading();
      const formattedData = {
        ...formData,
        startTime: new Date(formData.startTime),
        endTime: new Date(formData.endTime),
      };
      await createAuction(formattedData);
      showNotification('success', 'Аукціон успішно створено!');
    } catch (error) {
      console.error('Помилка при створенні аукціону', error);
      setErrorState('Не вдалося створити аукціон');
      showNotification('error', 'Не вдалося створити аукціон');
      throw error;
    } finally {
      stopLoading();
    }
  }, [startLoading, stopLoading, setErrorState, showNotification]);

  const updateExistingAuction = useCallback(async (auctionId: string, formData: any) => {
    try {
      startLoading();
      const formattedData = {
        ...formData,
        id: auctionId,
        startTime: new Date(formData.startTime),
        endTime: new Date(formData.endTime),
      };
      await updateAuction(formattedData);
      showNotification('success', 'Аукціон успішно оновлено!');
    } catch (error) {
      console.error('Помилка при оновленні аукціону', error);
      setErrorState('Не вдалося оновити аукціон');
      showNotification('error', 'Не вдалося оновити аукціон');
      throw error;
    } finally {
      stopLoading();
    }
  }, [startLoading, stopLoading, setErrorState, showNotification]);

  return {
    loading,
    error,
    fetchMyAuctions,
    fetchAuctionById,
    createNewAuction,
    updateExistingAuction,
  };
}; 