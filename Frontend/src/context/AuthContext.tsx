import { createContext, useContext, useState, useEffect } from 'react';
import { UserResponseDTO } from '../types/authTypes';
import { getCurrentUser, logoutUser } from '../api/auth'; // обязательно подтяни сюда новую функцию

interface AuthContextType {
  user: UserResponseDTO | null;
  setUser: (user: UserResponseDTO | null) => void;
}

const AuthContext = createContext<AuthContextType>({
  user: null,
  setUser: () => {},
});

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<UserResponseDTO | null>(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const currentUser = await getCurrentUser();
        setUser(currentUser);
      } catch (error) {
        console.error('Не удалось получить пользователя:', error);
        setUser(null); // если ошибка 401 — обнуляем
        await logoutUser(); // очищаем куки на всякий случай
      }
    };

    fetchUser();
  }, []);

  return (
    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
