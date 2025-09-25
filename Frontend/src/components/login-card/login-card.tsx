import { useCallback, useState } from "react";
import { LoginRequestDto } from "../../api/client";
import { isEmail } from "../../utils/validations";
import ValidationInput from "../validation-input/validation-input";
import CommonButton from "../common-button/common-button";

import "./login-card.scss";

type LoginCardProps = {
  onLogin: (user: LoginRequestDto) => Promise<void>;
  loading: boolean;
};

export default function LoginCard({ onLogin, loading }: LoginCardProps) {
  const [flipped, setFlipped] = useState(false);
  const [user, setUser] = useState<LoginRequestDto>();
  const [validLoginEmail, setLoginEmailValid] = useState<boolean>(true);

  const isFormValid = !!user?.email && validLoginEmail && !!user?.password;

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

  const handleSubmit = () => {
    if (user && isFormValid) {
      onLogin(user);
    }
  };

  return (
    <div className="card">
      <div className={`card-inner ${flipped ? "is-flipped" : ""}`}>
        <div className="card-face card-face-front">
          <div className="card-wrapper">
            <div className="card-text">
              <h1>Login</h1>
              <div className="login-text">
                <p>Har inget konto?</p>
                <p className="uri-reg" onClick={() => setFlipped(!flipped)}>
                  Registrering
                </p>
              </div>
            </div>

            <div className="card-content">
              <div className="card-input">
                <ValidationInput
                  id="email"
                  label="Email"
                  validationmsg={"Inkorrekt email address"}
                  placeholder="Skriv din email"
                  value={user?.email ?? ""}
                  onChange={(value) => {
                    updateLoginEmail(value);
                  }}
                  isValid={user?.email?.trim() !== "" && validLoginEmail}
                />
                <ValidationInput
                  id="password"
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
              <div className="card-btn">
                <CommonButton
                  onClick={handleSubmit}
                  title="LOGIN"
                  variant="default"
                  disabled={!isFormValid}
                  loading={loading}
                />
              </div>
            </div>
          </div>
        </div>

        <div className="card-face card-face-back">
          <div className="card-content">
            <div className="card-header">
              <h2 onClick={() => setFlipped(!flipped)}>Header</h2>
            </div>
            <div className="card-body">
              <h3>Text</h3>
              <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
                incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
                pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                deserunt mollit anim id est laborum.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
