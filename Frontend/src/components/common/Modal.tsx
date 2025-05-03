import { Modal as BootstrapModal } from 'react-bootstrap';
import { ReactNode } from 'react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: ReactNode;
  size?: 'sm' | 'lg' | 'xl';
  centered?: boolean;
}

export const Modal = ({
  isOpen,
  onClose,
  title,
  children,
  size = 'lg',
  centered = true,
}: ModalProps) => {
  return (
    <BootstrapModal
      show={isOpen}
      onHide={onClose}
      size={size}
      centered={centered}
    >
      <BootstrapModal.Header closeButton>
        <BootstrapModal.Title>{title}</BootstrapModal.Title>
      </BootstrapModal.Header>
      <BootstrapModal.Body>{children}</BootstrapModal.Body>
    </BootstrapModal>
  );
}; 