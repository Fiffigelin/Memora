import { createContext, useContext } from "react";
import { AuthResponseDto, LoginRequestDto } from "../api/client";

export type AuthContextType = {
  user: AuthResponseDto;
  login: (credentials: LoginRequestDto) => Promise<void>;
  logout: () => void;
  loading: boolean;
};

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuthContext = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuthContext must be used within AuthProvider");
  return context;
};
