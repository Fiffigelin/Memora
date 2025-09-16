import ValidationInput from "../../components/validation-input/validation-input";
import CommonButton from "../../components/common-button/common-button";
import { useCallback, useState } from "react";
import { LoginRequestDto } from "../../api/client";
import { isEmail } from "../../utils/validations";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./hooks/use-login";
import "./login-page.scss";
import LoginCard from "../../components/login-card/login-card";
import FlipCard from "../../components/flip-card/flip-card";

export default function LoginPage() {
  const navigate = useNavigate();

  const [user, setUser] = useState<LoginRequestDto>();
  const [validLoginEmail, setLoginEmailValid] = useState<boolean>(true);

  const { login } = useAuth();

  const updateLoginUser = useCallback(
    (property: keyof LoginRequestDto, value: string | undefined) => {
      setUser((prevState) => {
        return {
          ...prevState,
          [property]: value,
        };
      });
    },
    []
  );

  const updateLoginEmail = useCallback(
    (value: string) => {
      updateLoginUser("email", value);
      setLoginEmailValid(isEmail(value));
    },
    [updateLoginUser]
  );

  const updatePassword = useCallback(
    (value: string) => {
      updateLoginUser("password", value);
    },
    [updateLoginUser]
  );

  async function validateLogin() {
    if (user && validLoginEmail) {
      try {
        await login(user);
        console.log("User inloggad");
        // navigera till en annan sida
        navigate("/dashboard");
      } catch (err) {
        console.error("något gick väldigt fel");
      }
    }
  }

  return (
    <section className="login-container">
      <LoginCard />
    </section>
  );
}
