import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/layout/Header';
import Footer from './components/layout/Footer';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import AuctionsListPage from './pages/AuctionsListPage'; // Переименовал тут!
import AuctionDetailsPage from './pages/AuctionDetailsPage'; // <-- ДЕТАЛИ АУКЦИОНА
import BlogPage from './pages/BlogPage';
import ServicesPage from './pages/ServicesPage';
import ContactPage from './pages/ContactPage';
import ProfilePage from './pages/ProfilePage';
import Dashboard from './components/profile/Dashboard';
import ProfileLayout from './components/profile/ProfileLayout';
import AdminPanel from './components/profile/AdminPanel';
import { AuthProvider } from './context/AuthContext';
import { GoogleOAuthProvider } from '@react-oauth/google';

const App = () => {
  return (
    <GoogleOAuthProvider clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID!}>
      <AuthProvider>
        <Router>
          <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
            <Header />
            <main style={{ flex: 1 }}>
              <Routes>
                {/* Публичные страницы */}
                <Route path="/" element={<HomePage />} />
                <Route path="/home" element={<HomePage />} />
                <Route path="/auctions" element={<AuctionsListPage />} /> {/* <-- Страница всех аукционов */}
                <Route path="/auction/:id" element={<AuctionDetailsPage />} /> {/* <-- Страница деталей аукциона */}
                <Route path="/blog" element={<BlogPage />} />
                <Route path="/services" element={<ServicesPage />} />
                <Route path="/contact" element={<ContactPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />

                {/* Профиль */}
                <Route path="/profile" element={<ProfileLayout />}>
                  <Route index element={<Navigate to="dashboard" replace />} />
                  <Route path="dashboard" element={<Dashboard />} />
                  <Route path="winning-bids" element={<div>Winning Bids Page</div>} />
                  <Route path="favorites" element={<div>Favorites Page</div>} />
                  <Route path="personal" element={<ProfilePage />} />
                  <Route path="/profile/admin-panel" element={<AdminPanel />} />
                </Route>
                <Route path="*" element={<div>404 Not Found</div>} />
              </Routes>
            </main>
            <Footer />
          </div>
        </Router>
      </AuthProvider>
    </GoogleOAuthProvider>
  );
};

export default App;
