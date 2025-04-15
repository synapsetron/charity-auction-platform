import axios from 'axios';
import { UserLoginDTO, UserRegisterDTO, UserResponseDTO } from '../types/authTypes';

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const registerUser = async (data: UserRegisterDTO): Promise<UserResponseDTO> => {
  const response = await axios.post(`${API_URL}/api/register`, data);
  return response.data;
};

export const loginUser = async (data: UserLoginDTO): Promise<UserResponseDTO> => {
  const response = await axios.post(`${API_URL}/api/login`, data);
  return response.data;
};

export const logoutUser = () => {
  localStorage.removeItem('user');
  document.cookie = 'accessToken=; Max-Age=0; path=/;';
};

export const resetPassword = async (email: string): Promise<void> => {
  await axios.post(`${API_URL}/api/reset-password`, { email });
};