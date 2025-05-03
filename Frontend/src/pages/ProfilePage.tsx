import {
  Container,
  Row,
  Col,
  Form,
  Button,
  OverlayTrigger,
  Tooltip,
  Toast,
  ToastContainer
} from "react-bootstrap";
import { FaUserCircle } from "react-icons/fa";
import { useAuth } from "../context/AuthContext";
import { useState } from "react";
import { updateUserProfile, logoutUser } from "../api/auth";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

const ProfilePage = () => {
  const { user, setUser } = useAuth();
  const [firstName, setFirstName] = useState(user?.firstName || "");
  const [lastName, setLastName] = useState(user?.lastName || "");
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [showToast, setShowToast] = useState(false);
  const navigate = useNavigate();
  const { t } = useTranslation();

  if (!user) {
    return (
      <Container className="py-5">
        <h3>{t("profilepage.not_found")}</h3>
      </Container>
    );
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrorMessage("");

    if (newPassword && oldPassword === newPassword) {
      setErrorMessage(t("profilepage.error_same_password"));
      return;
    }

    try {
      const updatedUser = await updateUserProfile({
        firstName,
        lastName,
        oldPassword: oldPassword || undefined,
        newPassword: newPassword || undefined
      });

      setUser(updatedUser);
      localStorage.setItem("user", JSON.stringify(updatedUser));
      document.cookie = `accessToken=${updatedUser.accessToken}; path=/;`;
      setShowToast(true);

      setTimeout(() => {
        if (newPassword) {
          logoutUser();
          navigate("/login");
        } else {
          setOldPassword("");
          setNewPassword("");
        }
      }, 2000);
    } catch (error: any) {
      setErrorMessage(
        error.response?.data?.message || t("profilepage.error_update")
      );
    }
  };

  return (
    <Container className="py-3">
      <section className="bg-white p-4 rounded shadow">
        <div className="d-flex align-items-center gap-4 mb-4">
          <FaUserCircle size={96} className="text-secondary" />
          <div>
            <h5 className="mb-0">
              {firstName} {lastName}
            </h5>
            <small className="text-muted">{user.email}</small>
          </div>
        </div>

        <Form onSubmit={handleSubmit}>
          <Row className="mb-3">
            <Col md={4}>
              <Form.Label>{t("profilepage.title_name")}</Form.Label>
              <Form.Control
                type="text"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                required
              />
            </Col>
            <Col md={4}>
              <Form.Label>{t("profilepage.title_lastname")}</Form.Label>
              <Form.Control
                type="text"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                required
              />
            </Col>
          </Row>

          <Row className="mb-3">
            <Col md={6}>
              <OverlayTrigger
                placement="top"
                overlay={
                  <Tooltip id="tooltip-email">{t("profilepage.email_tooltip")}</Tooltip>
                }
              >
                <div>
                  <Form.Label>{t("profilepage.title_email")}</Form.Label>
                  <Form.Control type="email" value={user.email} disabled />
                </div>
              </OverlayTrigger>
            </Col>
            <Col md={6}>
              <Form.Label>{t("profilepage.title_role")}</Form.Label>
              <Form.Control type="text" value={user.role} readOnly />
            </Col>
          </Row>

          <Row className="mb-3">
            <Col md={6}>
              <Form.Label>{t("profilepage.title_old_password")}</Form.Label>
              <Form.Control
                type="password"
                value={oldPassword}
                onChange={(e) => setOldPassword(e.target.value)}
              />
            </Col>
            <Col md={6}>
              <Form.Label>{t("profilepage.title_new_password")}</Form.Label>
              <Form.Control
                type="password"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
            </Col>
          </Row>

          {errorMessage && (
            <div className="alert alert-danger" role="alert">
              {errorMessage}
            </div>
          )}

          <Button type="submit" variant="success" className="mt-3 w-100">
            {t("profilepage.submit_button")}
          </Button>
        </Form>
      </section>

      <ToastContainer position="top-center" className="p-3">
        <Toast
          bg="success"
          onClose={() => setShowToast(false)}
          show={showToast}
          delay={2000}
          autohide
        >
          <Toast.Body className="text-white">
            {t("profilepage.success_toast")}
          </Toast.Body>
        </Toast>
      </ToastContainer>
    </Container>
  );
};

export default ProfilePage;
