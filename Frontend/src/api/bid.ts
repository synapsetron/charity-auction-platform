import axios from "axios";
import { BidResponseWithWinnerDTO, CreateBidRequestDTO, BidResponseDTO } from "types/bidTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const getBidHistory = async (): Promise<BidResponseWithWinnerDTO[]> => {
  const response = await axios.get(`${API_URL}/api/Bid/history`, {
    withCredentials: true,
  });
  return response.data;
};

export const getWinningBids = async (): Promise<BidResponseWithWinnerDTO[]> => {
  const response = await axios.get(`${API_URL}/api/Bid/winning`, {
    withCredentials: true,
  });
  return response.data;
};

export const donateBid = async (bidId: string): Promise<void> => {
  await axios.post(`${API_URL}/api/Bid/${bidId}/donate`, null, {
    withCredentials: true,
  });
};
export const createBid = async (auctionId: string, amount: number): Promise<BidResponseDTO> => {
  const payload: CreateBidRequestDTO = { auctionId, amount };
  const response = await axios.post(`${API_URL}/api/Bid`, payload, {
    withCredentials: true,
  });
  return response.data;
};