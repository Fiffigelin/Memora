// src/routes/AppRoutes.tsx
import { BrowserRouter, Routes, Route } from "react-router-dom";
// import PublicLayout from "../layouts/PublicLayout";
// import PrivateLayout from "../layouts/PrivateLayout";

// import LoginPage from "../pages/LoginPage";
// import RegisterPage from "../pages/RegisterPage";
// import Dashboard from "../pages/Dashboard";
// import VocabularyLists from "../pages/VocabularyLists";
// import Profile from "../pages/Profile";
import PublicLayout from "../layouts/public-layout";
import LoginPage from "../pages/public/login-page";
import RegisterPage from "../pages/public/register-page";
import ProtectedRoute from "./protected-route";
import App from "../App";
import { useAuth } from "../pages/public/hooks/use-login";

export default function AppRoutes() {
  const { user } = useAuth();
  const isAuthenticated = !!user;

  return (
    <BrowserRouter>
      <Routes>
        {/* Public layout */}
        <Route element={<PublicLayout />}>
          <Route path="/" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Route>

        <Route
          path="/dashboard"
          element={
            <ProtectedRoute isAuthenticated={isAuthenticated}>
              <App />
            </ProtectedRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}
