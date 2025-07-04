import { Container, Row, Col, Nav, Button, Card, Spinner } from "react-bootstrap";
import { FaUserCircle } from "react-icons/fa";
import { MdDashboard, MdOutlineCategory } from "react-icons/md";
import { CiMedal } from "react-icons/ci";
import { GiBarbedStar } from "react-icons/gi";
import { Link, Outlet, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { logoutUser } from "../../api/auth";
import { useTranslation } from "react-i18next";

const ProfileLayout = () => {
  const { user, setUser} = useAuth();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const handleLogout = () => {
    logoutUser();
    setUser(null);
    navigate("/login");
  };
  if (!user) {
    return (
      <Container className="py-5">
        <h3>{t("profile.not_found")}</h3>
      </Container>
    );
  }

  return (
    <Container fluid className="py-5">
      <Row className="g-4">
        <Col md={3}>
          <Card className="p-4 d-flex flex-column align-items-center shadow-sm" style={{ minHeight: "650px" }}>
            <FaUserCircle size={96} className="text-secondary mb-3" />
            <h5 className="mb-1">
              {user.firstName} {user.lastName}
            </h5>
            <small className="text-muted mb-4">{user.email}</small>

            <Nav className="flex-column w-100 gap-3 mb-5">
              <Nav.Link as={Link} to="/profile/dashboard" className="d-flex align-items-center gap-2 text-dark fw-semibold">
                <MdDashboard /> {t("profile.nav.dashboard")}
              </Nav.Link>
              <Nav.Link as={Link} to="/profile/winning-bids" className="d-flex align-items-center gap-2 text-dark fw-semibold">
                <CiMedal /> {t("profile.nav.my_bids")}
              </Nav.Link>
              <Nav.Link as={Link} to="/profile/favorites" className="d-flex align-items-center gap-2 text-dark fw-semibold">
                <GiBarbedStar /> {t("profile.nav.favorites")}
              </Nav.Link>
              <Nav.Link as={Link} to="/profile/personal" className="d-flex align-items-center gap-2 text-success fw-bold">
                <FaUserCircle /> {t("profile.nav.profile")}
              </Nav.Link>

              {user?.role === "Admin" && (
                <Nav.Link as={Link} to="/profile/admin-panel" className="d-flex align-items-center gap-2 text-danger fw-semibold">
                  <MdOutlineCategory /> {t("profile.nav.admin_panel")}
                </Nav.Link>
              )}
              {user?.role === "Seller" && (
                <>
                  <Nav.Link as={Link} to="/profile/my-products" className="d-flex align-items-center gap-2 text-primary fw-semibold">
                    <MdOutlineCategory /> {t("profile.nav.my_products")}
                  </Nav.Link>
                  <Nav.Link as={Link} to="/profile/add-product" className="d-flex align-items-center gap-2 text-primary fw-semibold">
                    <MdOutlineCategory /> {t("profile.nav.add_product")}
                  </Nav.Link>
                </>
              )}
            </Nav>

            <Button variant="danger" onClick={handleLogout} className="w-100 mt-auto">
              {t("profile.logout")}
            </Button>
          </Card>
        </Col>

        <Col md={9}>
          <Card className="p-4 shadow-sm" style={{ minHeight: "650px" }}>
            <Outlet />
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default ProfileLayout;
