import { useState } from "react";
import "./validation-input.scss";

export type ValidationInputProps = {
  label: string;
  validationmsg: string;
  isValid: boolean;
  placeholder: string;
  value: string;
  onChange: (value: string) => void;
  type?: "text" | "email" | "password";
};

export default function ValidationInput({
  label,
  validationmsg,
  isValid,
  placeholder,
  value,
  onChange,
  type = "text",
}: ValidationInputProps) {
  const [touched, setTouched] = useState(false);

  return (
    <div className="input-container">
      <p className="title">{label}</p>
      <input
        className={`input-field ${!isValid && touched ? "input-error" : ""}`}
        placeholder={placeholder}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        onBlur={() => setTouched(true)}
        type={type}
      />
      {!isValid && touched && <p className="error-msg">{validationmsg}</p>}
    </div>
  );
}
