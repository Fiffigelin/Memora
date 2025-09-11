// auth-context.tsx
import { createContext, ReactNode, useState, useCallback } from "react";
import { AuthResponseDto, Client, LoginRequestDto } from "../api/client";
import { ConfigurationProvider } from "../api/client-base";

type AuthContextType = {
  user: AuthResponseDto | null;
  login: (credentials: LoginRequestDto) => Promise<void>;
  logout: () => void;
};

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<AuthResponseDto | null>(null);
  const baseUrl = "https://localhost:7156";

  const login = useCallback(async (credentials: LoginRequestDto) => {
    const client = new Client(new ConfigurationProvider(undefined, baseUrl));
    const response = await client.login(credentials);

    if (response?.token) localStorage.setItem("authToken", response.token);
    setUser(response);
  }, []);

  const logout = useCallback(() => {
    localStorage.removeItem("authToken");
    setUser(null);
  }, []);

  return <AuthContext.Provider value={{ user, login, logout }}>{children}</AuthContext.Provider>;
}
