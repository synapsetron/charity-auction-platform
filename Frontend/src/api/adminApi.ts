import axios from 'axios';

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const getPendingAuctions = async () => {
    const response = await axios.get(`${API_URL}/api/admin/pending-auctions`, { withCredentials: true });
    return response.data;
};
export const getAllAuctions = async () => {
    const response = await axios.get(`${API_URL}/api/Auction/all-auctions`, { withCredentials: true });
    return response.data;
  };

export const approveAuction = async (auctionId: string) => {
    await axios.post(`${API_URL}/api/admin/approve-auction/${auctionId}`, {}, { withCredentials: true });
};

export const rejectAuction = async (auctionId: string, reason: string) => {
    await axios.post(`${API_URL}/api/admin/reject-auction/${auctionId}`, { reason }, { withCredentials: true });
};

export const deleteAuction = async (auctionId: string) => {
    await axios.delete(`${API_URL}/api/admin/delete-auction/${auctionId}`, { withCredentials: true });
};

export const blockAuction = async (auctionId: string) => {
  await axios.post(`${API_URL}/api/admin/block-auction/${auctionId}`, {}, { withCredentials: true });
};

export const unblockAuction = async (auctionId: string) => {
    await axios.post(`${API_URL}/api/admin/unblock-auction/${auctionId}`, {}, { withCredentials: true });
  };

  export const getAllUsers = async () => {
    const response = await axios.get(`${API_URL}/api/admin/users-get-all`, { withCredentials: true });
    return response.data;
  };
  
  export const blockUser = async (userId: string) => {
    await axios.put(`${API_URL}/api/admin/block-user/${userId}`, {}, { withCredentials: true });
  };
  
  export const unblockUser = async (userId: string) => {
    await axios.put(`${API_URL}/api/admin/unblock-user/${userId}`, {}, { withCredentials: true });
  };
  
  export const deleteUser = async (userId: string) => {
    await axios.delete(`${API_URL}/api/admin/delete-user/${userId}`, { withCredentials: true });
  };