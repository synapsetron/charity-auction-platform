import { useEffect, useState } from "react";
import axios from "axios";
import { StatsOverviewDTO } from "../types/statsTypes";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const useStats = () => {
  const [stats, setStats] = useState<StatsOverviewDTO | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const response = await axios.get(`${API_URL}/api/Stats/overview`, {
          withCredentials: true,
        });
        setStats(response.data);
      } catch (err) {
        console.error("Error loading stats", err);
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  return { stats, loading };
};
