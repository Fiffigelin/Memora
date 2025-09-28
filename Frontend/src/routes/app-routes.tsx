// src/routes/AppRoutes.tsx
import { BrowserRouter, Routes, Route } from "react-router-dom";
// import PublicLayout from "../layouts/PublicLayout";
// import PrivateLayout from "../layouts/PrivateLayout";

// import LoginPage from "../pages/LoginPage";
// import RegisterPage from "../pages/RegisterPage";
// import Dashboard from "../pages/Dashboard";
// import VocabularyLists from "../pages/VocabularyLists";
// import Profile from "../pages/Profile";
import PublicLayout from "../layouts/public/public-layout";
import LoginPage from "../pages/public/login-page";
import RegisterPage from "../pages/public/register-page";
import ProtectedRoute from "./protected-route";
import { useAuth } from "../pages/public/hooks/use-login";
import PrivateLayout from "../layouts/private/private-layout";
import DashboardHome from "../pages/private/dashboard-home/dashboard-home-page";
import VocabularyHome from "../pages/private/vocabulary-home/vocabulary-home-page";

export default function AppRoutes() {
  const { user } = useAuth();
  const isAuthenticated = !!user;

  return (
    <BrowserRouter>
      <Routes>
        <Route element={<PublicLayout />}>
          <Route path="/" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Route>

        <Route
          element={
            <ProtectedRoute isAuthenticated={isAuthenticated}>
              <PrivateLayout />
            </ProtectedRoute>
          }
        >
          <Route path="/dashboard" element={<DashboardHome />} />
          <Route path="/vocabulary" element={<VocabularyHome />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
