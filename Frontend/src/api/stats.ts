import axios from "axios";
import { StatsOverviewDTO } from "types/statsTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const getStatsOverview = async (): Promise<StatsOverviewDTO> => {
  const response = await axios.get(`${API_URL}/api/Stats/overview`, {
    withCredentials: true,
  });
  return response.data;
};