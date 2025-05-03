import { useEffect } from 'react';
import { Toast, ToastContainer } from 'react-bootstrap';
import { useNotification } from '../../hooks/useNotification';

export const NotificationContainer = () => {
  const { notifications, removeNotification } = useNotification();

  return (
    <ToastContainer position="top-end" className="p-3">
      {notifications.map(notification => (
        <Toast
          key={notification.id}
          bg={notification.type}
          show={true}
          onClose={() => removeNotification(notification.id)}
          delay={notification.duration}
          autohide
        >
          <Toast.Body className="text-white">
            {notification.message}
          </Toast.Body>
        </Toast>
      ))}
    </ToastContainer>
  );
}; 