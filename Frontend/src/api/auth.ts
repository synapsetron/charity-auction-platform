import axios from 'axios';
import {
  UserLoginDTO,
  UserRegisterDTO,
  UserResponseDTO,
  UpdateUserProfileDTO,
  ResetPasswordResponseDTO,
  ConfirmEmailRequestDTO,
  RefreshTokenRequestDTO,
  RevokeRefreshTokenResponseDTO
} from '../types/authTypes';

const API_URL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL: `${API_URL}/api`,
  withCredentials: true, // ⬅️ обязательно!
});

// Регистрация
export const registerUser = async (data: UserRegisterDTO): Promise<UserResponseDTO> => {
  const response = await api.post('/register', data);
  return response.data;
};

// Логин
export const loginUser = async (data: UserLoginDTO): Promise<UserResponseDTO> => {
  const response = await api.post('/login', data);
  return response.data;
};

// Логин через Google
export const googleLoginUser = async (idToken: string): Promise<UserResponseDTO> => {
  const response = await api.post('/login-google', { idToken });
  return response.data;
};

// Получение текущего пользователя
export const getCurrentUser = async (): Promise<UserResponseDTO> => {
  const response = await api.get('/current-user');
  return response.data;
};

// Обновление профиля
export const updateUserProfile = async (data: UpdateUserProfileDTO): Promise<UserResponseDTO> => {
  const response = await api.put('/user/update-profile', data);
  return response.data;
};

// Выход из аккаунта
export const logoutUser = async (): Promise<void> => {
  await api.post('/logout');
};

// Сброс пароля
export const resetPassword = async (email: string): Promise<ResetPasswordResponseDTO> => {
  const response = await api.post('/reset-password', { email });
  return response.data;
};

// Подтверждение почты
export const confirmEmail = async (data: ConfirmEmailRequestDTO): Promise<void> => {
  await api.post('/confirm-email', data);
};

// Обновить access токен через refresh
export const refreshToken = async (data: RefreshTokenRequestDTO): Promise<UserResponseDTO> => {
  const response = await api.post('/refresh-token', data);
  return response.data;
};

// Отозвать refresh токен
export const revokeRefreshToken = async (data: RefreshTokenRequestDTO): Promise<RevokeRefreshTokenResponseDTO> => {
  const response = await api.post('/revoke-refresh-token', data);
  return response.data;
};
