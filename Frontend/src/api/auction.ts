import axios from "axios";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { CreateAuctionRequestDTO, UpdateAuctionRequestDTO } from "../types/auctionTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const getAllAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/Auction/all`, { withCredentials: true });
  return response.data;
};

export const getApprovedAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/Auction/approved-auctions`, { withCredentials: true });
  return response.data;
};

export const getAuctionById = async (auctionId: string): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.get(`${API_URL}/api/Auction/${auctionId}`, { withCredentials: true });
  return response.data;
};

export const getMyAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/auction/my-auctions`, { withCredentials: true });
  return response.data;
};

export const createAuction = async (data: CreateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.post(`${API_URL}/api/Auction/create/`, data, { withCredentials: true });
  return response.data;
};

export const updateAuction = async (data: UpdateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.put(`${API_URL}/api/auction/update/`, data, { withCredentials: true });
  return response.data;
};

export const deleteAuction = async (auctionId: string): Promise<void> => {
  await axios.delete(`${API_URL}/api/auction/${auctionId}`, { withCredentials: true });
};

export const closeAuction = async (auctionId: string): Promise<void> => {
  await axios.patch(`${API_URL}/api/auction/close/${auctionId}`, {}, { withCredentials: true });
};
