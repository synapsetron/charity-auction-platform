import axios from 'axios';

const API_URL = process.env.REACT_APP_BACKEND_URL;


export const getAllAuctions = async () => {
  const response = await axios.get(`${API_URL}/api/Auction/all-auctions`, { withCredentials: true });
  return response.data;
};

export const getPendingAuctions = async () => {
  const response = await axios.get(`${API_URL}/api/v1/admin/auctions/pending`, { withCredentials: true });
  return response.data;
};

export const approveAuction = async (auctionId: string) => {
  await axios.post(`${API_URL}/api/v1/admin/auctions/${auctionId}/approve`, {}, { withCredentials: true });
};

export const rejectAuction = async (auctionId: string, reason: string) => {
  await axios.post(`${API_URL}/api/v1/admin/auctions/${auctionId}/reject`, { reason }, { withCredentials: true });
};

export const deleteAuction = async (auctionId: string) => {
  await axios.delete(`${API_URL}/api/v1/admin/${auctionId}`, { withCredentials: true });
};

export const blockAuction = async (auctionId: string) => {
  await axios.post(`${API_URL}/api/v1/admin/auctions/${auctionId}/block`, {}, { withCredentials: true });
};

export const unblockAuction = async (auctionId: string) => {
  await axios.post(`${API_URL}/api/v1/admin/auctions/${auctionId}/unblock`, {}, { withCredentials: true });
};

export const closeAuction = async (auctionId: string) => {
  await axios.patch(`${API_URL}/api/v1/admin/close/${auctionId}`, {}, { withCredentials: true });
};

// ------------------- Users -------------------

export const getAllUsers = async () => {
  const response = await axios.get(`${API_URL}/api/v1/admin/users`, { withCredentials: true });
  return response.data;
};

export const blockUser = async (userId: string) => {
  await axios.put(`${API_URL}/api/v1/admin/users/${userId}/block`, {}, { withCredentials: true });
};

export const unblockUser = async (userId: string) => {
  await axios.put(`${API_URL}/api/v1/admin/users/${userId}/unblock`, {}, { withCredentials: true });
};

export const deleteUser = async (userId: string) => {
  await axios.delete(`${API_URL}/api/v1/admin/users/${userId}`, { withCredentials: true });
};