import { createContext, useContext, useState, useEffect } from "react";
import { UserResponseDTO } from "../types/authTypes";
import { getCurrentUser } from "../api/auth";

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
      const wasLoggedIn = localStorage.getItem("was_logged_in");
      if (wasLoggedIn !== "true") {
        return;
      }

      try {
        const currentUser = await getCurrentUser();
        setUser(currentUser);
      } catch (error: any) {
        if (error.response?.status === 401) {
        } else {
          console.error("Error via fetching user:", error);
        }
        setUser(null);
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
