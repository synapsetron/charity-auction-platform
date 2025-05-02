import { Container, Row, Col, Nav, Button, Card } from "react-bootstrap";
import { FaUserCircle } from "react-icons/fa";
import { MdDashboard, MdOutlineCategory } from "react-icons/md";
import { CiMedal } from "react-icons/ci";
import { GiBarbedStar } from "react-icons/gi";
import { Link, Outlet, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { logoutUser } from "../../api/auth";

const ProfileLayout = () => {
  const { user, setUser } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logoutUser();
    setUser(null);
    navigate("/login");
  };

  if (!user) {
    return (
      <Container className="py-5">
        <h3>Користувач не знайдений. Потрібен вхід.</h3>
      </Container>
    );
  }

  return (
    <Container fluid className="py-5">
      <Row className="g-4">
        {/* Sidebar */}
        <Col md={3}>
          <Card
            className="p-4 d-flex flex-column align-items-center shadow-sm"
            style={{ minHeight: "650px" }}
          >
            <FaUserCircle size={96} className="text-secondary mb-3" />
            <h5 className="mb-1">
              {user.firstName} {user.lastName}
            </h5>
            <small className="text-muted mb-4">{user.email}</small>

            <Nav className="flex-column w-100 gap-3 mb-5">
              <Nav.Link
                as={Link}
                to="/profile/dashboard"
                className="d-flex align-items-center gap-2 text-dark fw-semibold"
              >
                <MdDashboard /> Dashboard
              </Nav.Link>
              <Nav.Link
                as={Link}
                to="/profile/winning-bids"
                className="d-flex align-items-center gap-2 text-dark fw-semibold"
              >
                <CiMedal /> Мої ставки
              </Nav.Link>
              <Nav.Link
                as={Link}
                to="/profile/favorites"
                className="d-flex align-items-center gap-2 text-dark fw-semibold"
              >
                <GiBarbedStar /> Улюблені
              </Nav.Link>
              <Nav.Link
                as={Link}
                to="/profile/personal"
                className="d-flex align-items-center gap-2 text-success fw-bold"
              >
                <FaUserCircle /> Профіль
              </Nav.Link>

              {user?.role === "Admin" && (
                <Nav.Link
                  as={Link}
                  to="/profile/admin-panel"
                  className="d-flex align-items-center gap-2 text-danger fw-semibold"
                >
                  <MdOutlineCategory /> Панель Адміністратора
                </Nav.Link>
              )}
              {user?.role === "Seller" && (
                <>
                  <Nav.Link
                    as={Link}
                    to="/profile/my-products"
                    className="d-flex align-items-center gap-2 text-primary fw-semibold"
                  >
                    <MdOutlineCategory /> Мої товари
                  </Nav.Link>
                  <Nav.Link
                    as={Link}
                    to="/profile/add-product"
                    className="d-flex align-items-center gap-2 text-primary fw-semibold"
                  >
                    <MdOutlineCategory /> Додати товар
                  </Nav.Link>
                </>
              )}
            </Nav>

            <Button
              variant="danger"
              onClick={handleLogout}
              className="w-100 mt-auto"
            >
              Вийти
            </Button>
          </Card>
        </Col>

        {/* Main Content */}
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
