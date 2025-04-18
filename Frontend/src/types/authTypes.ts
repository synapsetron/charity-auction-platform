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
  }

export interface UpdateUserProfileDTO {
  firstName: string;
  lastName: string;
  oldPassword?: string;
  newPassword?: string;
}
  