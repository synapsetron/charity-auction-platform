import { Modal as BootstrapModal, Button } from 'react-bootstrap';
import { ReactNode } from 'react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: ReactNode;
  size?: 'sm' | 'lg' | 'xl';
  centered?: boolean;
  showFooter?: boolean;
  confirmText?: string;
  cancelText?: string;
  onConfirm?: () => void;
}

export const Modal = ({
  isOpen,
  onClose,
  title,
  children,
  size = 'lg',
  centered = true,
  showFooter = false,
  confirmText = 'Confirm',
  cancelText = 'Cancel',
  onConfirm,
}: ModalProps) => {
  return (
    <BootstrapModal show={isOpen} onHide={onClose} size={size} centered={centered}>
      <BootstrapModal.Header closeButton>
        <BootstrapModal.Title>{title}</BootstrapModal.Title>
      </BootstrapModal.Header>

      <BootstrapModal.Body>{children}</BootstrapModal.Body>

      {showFooter && (
        <BootstrapModal.Footer>
          <Button variant="secondary" onClick={onClose}>
            {cancelText}
          </Button>
          <Button variant="danger" onClick={onConfirm}>
            {confirmText}
          </Button>
        </BootstrapModal.Footer>
      )}
    </BootstrapModal>
  );
};
