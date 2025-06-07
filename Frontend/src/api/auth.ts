import axios from 'axios';
import {
  UserLoginDTO,
  UserRegisterDTO,
  UserResponseDTO,
  UpdateUserProfileDTO,
  ResetPasswordRequestDTO,
  ResetPasswordResponseDTO,
  ConfirmEmailRequestDTO,
  RefreshTokenRequestDTO,
  RevokeRefreshTokenResponseDTO,
  CurrentUserResponseDTO,
} from '../types/authTypes';

const API_URL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL: `${API_URL}/api/v1/auth`,
  withCredentials: true,
});

// Регистрация нового пользователя
export const registerUser = async (data: UserRegisterDTO): Promise<UserResponseDTO> => {
  const response = await api.post('/register', data);
  return response.data;
};

// Логин пользователя
export const loginUser = async (data: UserLoginDTO): Promise<UserResponseDTO> => {
  const response = await api.post('/login', data);
  localStorage.setItem('was_logged_in', 'true');
  return response.data;
};

// Логин через Google OAuth
export const googleLoginUser = async (idToken: string): Promise<UserResponseDTO> => {
  const response = await api.post('/login-google', { idToken });
  localStorage.setItem('was_logged_in', 'true');
  return response.data;
};

// Получить данные текущего пользователя
export const getCurrentUser = async (): Promise<CurrentUserResponseDTO> => {
  const response = await api.get('/current-user');
  return response.data;
};

// Обновить профиль текущего пользователя
export const updateUserProfile = async (data: UpdateUserProfileDTO): Promise<UserResponseDTO> => {
  const response = await api.put('/user/update-profile', data);
  return response.data;
};

// Выход из аккаунта
export const logoutUser = async (): Promise<void> => {
  localStorage.setItem('was_logged_in', 'false');
  await api.post('/logout');
};

// Сброс пароля
export const resetPassword = async (data: ResetPasswordRequestDTO): Promise<ResetPasswordResponseDTO> => {
  const response = await api.post('/reset-password', data);
  return response.data;
};

// Подтверждение email
export const confirmEmail = async (data: ConfirmEmailRequestDTO): Promise<void> => {
  await api.post('/confirm-email', data);
};

// Обновление access токена через refresh token
export const refreshToken = async (data: RefreshTokenRequestDTO): Promise<CurrentUserResponseDTO> => {
  const response = await api.post('/refresh-token', data);
  return response.data;
};

// Отзыв refresh токена
export const revokeRefreshToken = async (data: RefreshTokenRequestDTO): Promise<RevokeRefreshTokenResponseDTO> => {
  const response = await api.post('/revoke-refresh-token', data);
  return response.data;
};
