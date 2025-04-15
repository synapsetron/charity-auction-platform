import { BrowserRouter as Router, Routes, Route, Navigate  } from 'react-router-dom';
import Header from './components/layout/Header';
import Footer from './components/layout/Footer';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ProductPage from './pages/AuctionsListPage';
import BlogPage from './pages/BlogPage';
import ServicesPage from './pages/ServicesPage';
import ContactPage from './pages/ContactPage';
import ProfilePage from './pages/ProfilePage';
import Dashboard from './components/profile/Dashboard';
import ProfileLayout from './components/profile/ProfileLayout'; 
import { AuthProvider } from './context/AuthContext';

const App = () => {
  return (
    <AuthProvider>
    <Router>
      <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
        <Header />
        <main style={{ flex: 1 }}>
          <Routes>
            {/* Публичные страницы */}
            <Route path="/" element={<HomePage />} />
            <Route path="/home" element={<HomePage />} />
            <Route path="/product" element={<ProductPage />} />
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
            </Route>
          </Routes>
        </main>
        <Footer />
      </div>
    </Router>
    </AuthProvider>
  );
};

export default App;
