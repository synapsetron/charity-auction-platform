import { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Button from "react-bootstrap/Button";
import Dropdown from "react-bootstrap/Dropdown";
import { Link, useNavigate } from "react-router-dom";
import { FaSearch, FaUserCircle, FaBell } from "react-icons/fa";
import { useAuth } from "../../context/AuthContext";
import { logoutUser } from "../../api/auth";
import logo from "../../assets/icons/hammer.png";
import { useTranslation } from "react-i18next";
import { useNotifications } from "../../context/NotificationContext";

function Header() {
  const [isScrolled, setIsScrolled] = useState(false);
  const { user, setUser } = useAuth();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();

  const [language, setLanguage] = useState<"ua" | "en">(
    (i18n.language as "ua" | "en") || "ua"
  );

  const { notifications, unreadCount, clearUnread } = useNotifications();

  const toggleLanguage = (lang: "ua" | "en") => {
    setLanguage(lang);
    i18n.changeLanguage(lang);
  };

  useEffect(() => {
    const onScroll = () => {
      setIsScrolled(window.scrollY > 10);
    };
    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  const handleLogout = () => {
    logoutUser();
    setUser(null);
    navigate("/login");
  };

  return (
    <Navbar
      expand="lg"
      sticky="top"
      style={{
        transition: "background-color 0.3s, color 0.3s",
        backgroundColor: isScrolled ? "#fff" : "#153738",
        color: isScrolled ? "#000" : "#fff",
      }}
      variant={isScrolled ? "light" : "dark"}
      className={isScrolled ? "shadow-sm" : ""}
    >
      <Container fluid className="px-4">
        <Navbar.Brand
          as={Link}
          to="/"
          className="d-flex align-items-center gap-2"
        >
          <img src={logo} alt="Logo" style={{ height: "32px" }} />
          <span
            className="fw-bold"
            style={{ color: isScrolled ? "#000" : "#fff" }}
          >
            ДоброBid
          </span>
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="main-navbar" />
        <Navbar.Collapse id="main-navbar" className="justify-content-between">
          <Nav className="gap-3">
            {[
              { text: t("home"), link: "/home" },
              { text: t("auctions"), link: "/auctions" },
              { text: t("blog"), link: "/blog" },
              { text: t("about"), link: "/about" },
              { text: t("services"), link: "/services" },
              { text: t("contact"), link: "/contact" },
            ].map(({ text, link }) => (
              <Nav.Link
                as={Link}
                to={link}
                key={text}
                className={isScrolled ? "text-dark" : "text-white"}
              >
                {text}
              </Nav.Link>
            ))}
          </Nav>

          <div className="d-flex align-items-center gap-3 position-relative">
            {/* Language Switch Dropdown */}
            <Dropdown>
              <Dropdown.Toggle
                variant="outline-light"
                size="sm"
                className="rounded-pill px-3"
                style={{
                  backgroundColor: isScrolled ? "#f0f0f0" : "transparent",
                  color: isScrolled ? "#000" : "#fff",
                  border: "1px solid",
                  borderColor: isScrolled ? "#ccc" : "#fff",
                }}
              >
                {language.toUpperCase()}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                <Dropdown.Item onClick={() => toggleLanguage("ua")}>
                  UA - Українська
                </Dropdown.Item>
                <Dropdown.Item onClick={() => toggleLanguage("en")}>
                  EN - English
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>

            <FaSearch
              style={{
                color: isScrolled ? "black" : "white",
                cursor: "pointer",
              }}
            />

            {user && (
              <Dropdown align="end" onToggle={(isOpen) => isOpen && clearUnread()}>
                <Dropdown.Toggle
                  variant="link"
                  id="dropdown-notifications"
                  style={{
                    color: isScrolled ? "black" : "white",
                    position: "relative",
                  }}
                >
                  <FaBell size={24} />
                  {unreadCount > 0 && (
                    <span
                      style={{
                        backgroundColor: "red",
                        color: "white",
                        borderRadius: "50%",
                        fontSize: "10px",
                        padding: "2px 6px",
                        position: "absolute",
                        top: "-5px",
                        right: "-5px",
                      }}
                    >
                      {unreadCount}
                    </span>
                  )}
                </Dropdown.Toggle>

                <Dropdown.Menu style={{ minWidth: "300px" }}>
                  {notifications.length === 0 ? (
                    <Dropdown.ItemText>{t("no_notifications")}</Dropdown.ItemText>
                  ) : (
                    notifications.map((n, idx) => (
                      <Dropdown.Item key={idx}>
                        <strong>{n.title}</strong>
                        <div className="small text-muted">{n.message}</div>
                      </Dropdown.Item>
                    ))
                  )}
                </Dropdown.Menu>
              </Dropdown>
            )}

            {user ? (
              <>
                <Nav.Link
                  as={Link}
                  to="/profile/dashboard"
                  className="d-flex align-items-center gap-2"
                >
                  <FaUserCircle size={24} />
                  <span className={isScrolled ? "text-dark" : "text-white"}>
                    {user.firstName}
                  </span>
                </Nav.Link>
                <Button
                  variant="danger"
                  onClick={handleLogout}
                  className="rounded-pill px-4"
                >
                  {t("logout")}
                </Button>
              </>
            ) : (
              <>
                <Nav.Link
                  as={Link}
                  to="/login"
                  className={isScrolled ? "text-dark" : "text-white"}
                >
                  {t("sign_in")}
                </Nav.Link>

                <Button
                  as="div"
                  variant={isScrolled ? "dark" : "light"}
                  className="rounded-pill px-4"
                >
                  <Link
                    to="/register"
                    className={`text-decoration-none d-block px-3 py-1 ${
                      isScrolled ? "text-white" : "text-dark"
                    }`}
                  >
                    {t("join")}
                  </Link>
                </Button>
              </>
            )}
          </div>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Header;
