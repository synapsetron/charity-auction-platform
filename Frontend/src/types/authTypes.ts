// Существующие типы
export interface UserRegisterDTO {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface UserLoginDTO {
  email: string;
  password: string;
}

export interface UserResponseDTO {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  photoUrl: string;
  role: string;
  commissionBalance: number;
  balance: number;
  createdAt: string;
  updatedAt: string;
  accessToken?: string;
  refreshToken?: string;
  accessTokenExpiresAt?: string;
}

export interface UpdateUserProfileDTO {
  firstName: string;
  lastName: string;
  oldPassword?: string;
  newPassword?: string;
}

// 🔥 Новые типы для сброса пароля, обновления токенов и пр.

export interface ResetPasswordRequestDTO {
  email: string;
}

export interface ResetPasswordResponseDTO {
  email: string;
  newPassword: string;
}

export interface ConfirmEmailRequestDTO {
  userId: string;
  token: string;
}

export interface RefreshTokenRequestDTO {
  refreshToken: string;
}

export interface RevokeRefreshTokenResponseDTO {
  message: string;
}

export interface CurrentUserResponseDTO {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  photoUrl: string;
  role: string;
  commissionBalance: number;
  balance: number;
  createdAt: string;
  updatedAt: string;
  accessToken: string;
}
