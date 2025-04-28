import axios from "axios";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { CreateAuctionRequestDTO, UpdateAuctionRequestDTO } from "../types/auctionTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const getAllAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
    const response = await axios.get(`${API_URL}/api/Auction/all`);
    return response.data;
};
export const getApprovedAuctions = async (): Promise<AuctionResponseWithBidsDTO[]> => {
    const response = await axios.get(`${API_URL}/api/Auction/approved-auctions`);
    return response.data;
  };

export const getAuctionById = async (auctionId: string): Promise<AuctionResponseWithBidsDTO> => {
    const response = await axios.get(`${API_URL}/api/Auction/${auctionId}`);
    return response.data;
};

export const createAuction = async (data: CreateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    const accessToken = user.accessToken;

    const response = await axios.post(`${API_URL}/api/auction`, data, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });

    return response.data;
};

export const updateAuction = async (data: UpdateAuctionRequestDTO): Promise<AuctionResponseWithBidsDTO> => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    const accessToken = user.accessToken;

    const response = await axios.put(`${API_URL}/api/auction`, data, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });

    return response.data;
};

export const deleteAuction = async (auctionId: string): Promise<void> => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    const accessToken = user.accessToken;

    await axios.delete(`${API_URL}/api/auction/${auctionId}`, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });
};

export const closeAuction = async (auctionId: string): Promise<void> => {
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    const accessToken = user.accessToken;

    await axios.patch(`${API_URL}/api/auction/close/${auctionId}`, {}, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });
};
