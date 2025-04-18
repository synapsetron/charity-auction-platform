import axios from 'axios';
import { UserLoginDTO, UserRegisterDTO, UserResponseDTO, UpdateUserProfileDTO } from '../types/authTypes';

const API_URL = process.env.REACT_APP_BACKEND_URL;

export const registerUser = async (data: UserRegisterDTO): Promise<UserResponseDTO> => {
  const response = await axios.post(`${API_URL}/api/register`, data);
  return response.data;
};

export const loginUser = async (data: UserLoginDTO): Promise<UserResponseDTO> => {
  const response = await axios.post(`${API_URL}/api/login`, data);
  return response.data;
};

export const googleLoginUser = async (idToken: string): Promise<UserResponseDTO> => {
  const response = await axios.post(`${API_URL}/api/login-google`, { idToken });
  return response.data;
};


export const logoutUser = () => {
  localStorage.removeItem('user');
  document.cookie = 'accessToken=; Max-Age=0; path=/;';
};

export const updateUserProfile = async (data: UpdateUserProfileDTO): Promise<UserResponseDTO> => {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const accessToken = user.accessToken;

  const response = await axios.put(`${API_URL}/api/user/update-profile`, data, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });

  return response.data; // возвращаем нового юзера
};


export const resetPassword = async (email: string): Promise<void> => {
  await axios.post(`${API_URL}/api/reset-password`, { email });
};