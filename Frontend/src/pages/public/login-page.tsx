import "./login-page.scss";
import LoginCard from "../../components/login-card/login-card";
import { LoginRequestDto } from "../../api/client";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./hooks/use-login";

export default function LoginPage() {
  const { login, loading } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async (user: LoginRequestDto) => {
    try {
      await login(user);
      navigate("/dashboard");
    } catch (err) {
      // fixa felhantering
      console.error("Login failed", err);
    }
  };

  return (
    <section className="login-container">
      <LoginCard onLogin={handleLogin} loading={loading} />
    </section>
  );
}
