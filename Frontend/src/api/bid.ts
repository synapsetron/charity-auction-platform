import axios from "axios";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const donateBid = async (bidId: string) => {
  const response = await axios.post(`${API_URL}/api/bid/${bidId}/donate`);
  return response.data;
};
