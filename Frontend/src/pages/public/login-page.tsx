import ValidationInput from "../../components/validation-input/validation-input";
import CommonButton from "../../components/common-button/common-button";
import { useCallback, useState } from "react";
import { LoginRequestDto } from "../../api/client";
import { isEmail } from "../../utils/validations";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./hooks/use-login";
import "./login-page.scss";

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
    <section className="login-page">
      <div className="login-container">
        <div className="card">
          <div className="card-title">
            <h1>Login</h1>
            <div className="card-text">
              <p>Doesn't have a account yet?</p>
              <p className="uri">Sign up</p>
            </div>
          </div>
          <div className="card-content">
            <div className="card-input">
              <ValidationInput
                label="Email"
                validationmsg={"Inkorrekt email address"}
                placeholder="Skriv din email"
                value={user?.email ?? ""}
                onChange={(value) => {
                  updateLoginEmail(value);
                }}
                isValid={validLoginEmail}
              />
              <ValidationInput
                label="Lösenord"
                validationmsg={"Inkorrekt email address"}
                placeholder="Skriv ditt lösenord"
                value={user?.password ?? ""}
                onChange={(value) => {
                  updatePassword(value);
                }}
                isValid={true}
                type={"password"}
              />
            </div>
            <div className="card-button">
              <CommonButton
                onClick={validateLogin}
                title="LOGIN"
                variant="default"
                disabled={false}
              />
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
