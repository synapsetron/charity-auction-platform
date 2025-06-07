import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Header from "./components/layout/Header";
import Footer from "./components/layout/Footer";
import PaymentSuccessPage from "./pages/payment/PaymentSuccessPage";
import PaymentFailPage from "./pages/payment/PaymentFailPage";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import AuctionsListPage from "./pages/AuctionsListPage";
import AuctionDetailsPage from "./pages/AuctionDetailsPage";
import BlogPage from "./pages/BlogPage";
import ServicesPage from "./pages/ServicesPage";
import ContactPage from "./pages/ContactPage";
import ProfilePage from "./pages/ProfilePage";
import AboutPage from "pages/AboutPage";
import Dashboard from "./components/profile/Dashboard";
import ProfileLayout from "./components/profile/ProfileLayout";
import MyProducts from "./components/profile/MyProducts";
import AdminPanel from "./components/profile/AdminPanel";
import AuctionForm from "components/auction/AuctionForm";
import { AuthProvider } from "./context/AuthContext";
import { GoogleOAuthProvider } from "@react-oauth/google";
import MyBidsPage from "./components/profile/MyBidsPage";
import { NotificationContainer } from "./components/common/NotificationContainer";
import { NotificationProvider } from "./context/NotificationContext";
import "./i18n";

const App = () => {
  return (
    <GoogleOAuthProvider clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID!}>
      <AuthProvider>
        <NotificationProvider>
          <Router>
            <div
              style={{
                minHeight: "100vh",
                display: "flex",
                flexDirection: "column",
              }}
            >
              <Header />
              <main style={{ flex: 1 }}>
                <Routes>
                  {/* Public pages */}
                  <Route path="/" element={<HomePage />} />
                  <Route path="/home" element={<HomePage />} />
                  <Route path="/auctions" element={<AuctionsListPage />} />
                  <Route path="/auction/:id" element={<AuctionDetailsPage />} />
                  <Route path="/blog" element={<BlogPage />} />
                  <Route path="/services" element={<ServicesPage />} />
                  <Route path="/contact" element={<ContactPage />} />
                  <Route path="/login" element={<LoginPage />} />
                  <Route path="/register" element={<RegisterPage />} />
                  <Route path="/about" element={<AboutPage />} />
                  <Route path="/payment/success" element={<PaymentSuccessPage />} />
                  <Route path="/payment/fail" element={<PaymentFailPage />} />
                  {/* Profile */}
                  <Route path="/profile" element={<ProfileLayout />}>
                    <Route index element={<Navigate to="dashboard" replace />} />
                    <Route path="dashboard" element={<Dashboard />} />
                    <Route path="favorites" element={<div>Favorites Page</div>} />
                    <Route path="personal" element={<ProfilePage />} />
                    <Route path="/profile/admin-panel" element={<AdminPanel />} />
                    <Route path="my-products" element={<MyProducts />} />
                    <Route path="add-product" element={<AuctionForm />} />
                    <Route path="/profile/winning-bids" element={<MyBidsPage />} />
                  </Route>
                  <Route path="*" element={<div>404 Not Found</div>} />
                </Routes>
              </main>
              <Footer />
              <NotificationContainer />
            </div>
          </Router>
        </NotificationProvider>
      </AuthProvider>
    </GoogleOAuthProvider>
  );
};

export default App;

