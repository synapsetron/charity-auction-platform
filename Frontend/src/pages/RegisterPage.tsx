import { useState } from "react";
import { Container, Row, Col, Form, Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { registerUser } from "../api/auth";
import { UserRegisterDTO } from "../types/authTypes";
import { useTranslation } from "react-i18next";

const RegisterPage = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [formData, setFormData] = useState<UserRegisterDTO>({
    firstName: "",
    lastName: "",
    email: "",
    password: ""
  });

  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if (formData.password !== confirmPassword) {
      setError(t("registerpage.error_passwords_mismatch"));
      return;
    }

    try {
      setLoading(true);
      await registerUser(formData);
      navigate("/login");
    } catch (err: any) {
      setError(err.response?.data?.message || "Error");
    } finally {
      setLoading(false);
    }
  };

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
          <h3 className="mb-3">{t("registerpage.title")}</h3>
          <div className="d-flex align-items-center gap-3">
            <h5 className="text-success fw-normal">{t("registerpage.breadcrumb_home")}</h5>
            <h5 className="text-white fw-normal">/</h5>
            <h5 className="text-white fw-normal">{t("registerpage.breadcrumb_register")}</h5>
          </div>
        </Container>
      </div>

      <Container className="my-5">
        <Row className="justify-content-center">
          <Col md={6} lg={5}>
            <div className="bg-white p-4 rounded shadow">
              <div className="text-center mb-4">
                <h5>{t("registerpage.title")}</h5>
                <p className="mt-2">
                  {t("registerpage.already_have_account")}{" "}
                  <Link to="/login">{t("registerpage.login_link")}</Link>
                </p>
              </div>

              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formFirstName">
                  <Form.Label>{t("registerpage.form.first_name")}</Form.Label>
                  <Form.Control
                    type="text"
                    name="firstName"
                    placeholder={t("registerpage.form.placeholder_first_name")}
                    value={formData.firstName}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formLastName">
                  <Form.Label>{t("registerpage.form.last_name")}</Form.Label>
                  <Form.Control
                    type="text"
                    name="lastName"
                    placeholder={t("registerpage.form.placeholder_last_name")}
                    value={formData.lastName}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formEmail">
                  <Form.Label>{t("registerpage.form.email")}</Form.Label>
                  <Form.Control
                    type="email"
                    name="email"
                    placeholder={t("registerpage.form.placeholder_email")}
                    value={formData.email}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formPassword">
                  <Form.Label>{t("registerpage.form.password")}</Form.Label>
                  <Form.Control
                    type="password"
                    name="password"
                    placeholder={t("registerpage.form.placeholder_password")}
                    value={formData.password}
                    onChange={handleChange}
                    required
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formConfirmPassword">
                  <Form.Label>{t("registerpage.form.confirm_password")}</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder={t("registerpage.form.placeholder_confirm_password")}
                    value={confirmPassword}
                    onChange={e => setConfirmPassword(e.target.value)}
                    required
                  />
                </Form.Group>

                {error && <p className="text-danger">{error}</p>}

                <Button
                  type="submit"
                  variant="success"
                  className="w-100 rounded-0 mb-3"
                  disabled={loading}
                >
                  {loading
                    ? t("registerpage.submitting")
                    : t("registerpage.submit")}
                </Button>
              </Form>

              <p className="text-center mt-4">
                {t("registerpage.terms_text")}{" "}
                <span className="text-success text-decoration-underline mx-1">
                  {t("registerpage.terms")}
                </span>{" "}
                {t("registerpage.privacy")}
              </p>
            </div>
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

export default RegisterPage;
