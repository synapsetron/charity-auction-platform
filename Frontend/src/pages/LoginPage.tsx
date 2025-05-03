import { Container, Row, Col } from "react-bootstrap";
import { useState } from "react";
import LoginForm from "../components/auth/LoginForm";
import ResetPasswordForm from "../components/auth/ResetPasswordForm";
import { useTranslation } from "react-i18next";

const LoginPage = () => {
  const [isResetMode, setIsResetMode] = useState(false);
  const { t } = useTranslation();

  return (
    <section
      className="py-5 position-relative"
      style={{ backgroundColor: "#f0f2f5", minHeight: "100vh" }}
    >
      <div
        className="position-absolute top-50 start-0 translate-middle bg-success rounded-circle opacity-25"
        style={{ width: "300px", height: "300px", filter: "blur(100px)" }}
      ></div>

      <div className="py-5 bg-dark text-white">
        <Container>
          <h3 className="mb-3">
            {isResetMode ? t("loginpage.title_reset") : t("loginpage.title_login")}
          </h3>
          <div className="d-flex align-items-center gap-3">
            <h5 className="text-success fw-normal">{t("loginpage.breadcrumb_home")}</h5>
            <h5 className="text-white fw-normal">/</h5>
            <h5 className="text-white fw-normal">
              {isResetMode ? t("loginpage.breadcrumb_reset") : t("loginpage.breadcrumb_login")}
            </h5>
          </div>
        </Container>
      </div>

      <Container className="my-5">
        <Row className="justify-content-center">
          <Col md={6} lg={5}>
            {isResetMode ? (
              <ResetPasswordForm onBackToLogin={() => setIsResetMode(false)} />
            ) : (
              <LoginForm onForgotPassword={() => setIsResetMode(true)} />
            )}
          </Col>
        </Row>
      </Container>

      <div
        className="position-absolute bottom-0 end-0 translate-middle bg-success rounded-circle opacity-25"
        style={{ width: "300px", height: "300px", filter: "blur(100px)" }}
      ></div>
    </section>
  );
};

export default LoginPage;
