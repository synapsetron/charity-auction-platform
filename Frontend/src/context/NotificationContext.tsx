// src/context/NotificationContext.tsx
import {
  createContext,
  useContext,
  useEffect,
  useRef,
  useState,
  ReactNode,
} from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useAuth } from "./AuthContext";

const API_URL = process.env.REACT_APP_BACKEND_URL;

export type Notification = {
  title: string;
  message: string;
  createdAt: string;
};

interface NotificationContextType {
  notifications: Notification[];
  unreadCount: number;
  clearUnread: () => void;
}

const NotificationContext = createContext<NotificationContextType>({
  notifications: [],
  unreadCount: 0,
  clearUnread: () => {},
});

export const NotificationProvider = ({ children }: { children: ReactNode }) => {
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [unreadCount, setUnreadCount] = useState(0);
  const connectionRef = useRef<HubConnection | null>(null);
  const isMounted = useRef(true);
  const { user } = useAuth();

  const clearUnread = () => setUnreadCount(0);

  useEffect(() => {
    isMounted.current = true;

    const setupConnection = async () => {
      if (!user || connectionRef.current?.state === "Connected") return;

      try {
        const connection = new HubConnectionBuilder()
          .withUrl(`${API_URL}/auctionHub`, {
            withCredentials: true,
          })
          .withAutomaticReconnect()
          .build();

        connection.on("ReceiveNotification", (notification: Notification) => {
          if (!isMounted.current) return;

          setNotifications((prev) => {
            const exists = prev.some(
              (n) =>
                n.title === notification.title &&
                n.message === notification.message &&
                n.createdAt === notification.createdAt
            );
            return exists ? prev : [...prev, notification];
          });

          setUnreadCount((prev) => prev + 1);
        });

        await connection.start();
        connectionRef.current = connection;
      } catch (error) {
        console.error("Ошибка при подключении к SignalR:", error);
      }
    };

    setupConnection();

    return () => {
      isMounted.current = false;
      if (connectionRef.current) {
        connectionRef.current.stop();
        connectionRef.current = null;
      }
    };
  }, [user]);

  return (
    <NotificationContext.Provider
      value={{ notifications, unreadCount, clearUnread }}
    >
      {children}
    </NotificationContext.Provider>
  );
};

export const useNotifications = () => useContext(NotificationContext);
