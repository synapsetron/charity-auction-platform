import axios from "axios";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { CreateAuctionRequestDTO, UpdateAuctionRequestDTO } from "../types/auctionTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

// Get all auctions
export const getAllAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/v1/auctions/all`, { withCredentials: true });
  return response.data;
};

// Get approved auctions
export const getApprovedAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/v1/auctions/approved`, { withCredentials: true });
  return response.data;
};

// Get auction by ID
export const getAuctionById = async (auctionId: string): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.get(`${API_URL}/api/v1/auctions/${auctionId}`, { withCredentials: true });
  return response.data;
};

// Get current user's auctions
export const getMyAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
  const response = await axios.get(`${API_URL}/api/v1/auctions/my-auctions`, { withCredentials: true });
  return response.data;
};

// Create a new auction
export const createAuction = async (data: CreateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.post(`${API_URL}/api/v1/auctions`, data, { withCredentials: true });
  return response.data;
};

// Update an existing auction
export const updateAuction = async (data: UpdateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
  const response = await axios.put(`${API_URL}/api/v1/auctions/update`, data, { withCredentials: true });
  return response.data;
};