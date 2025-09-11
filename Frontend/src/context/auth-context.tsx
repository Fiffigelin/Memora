import { createContext, ReactNode, useState, useCallback } from "react";
import { AuthResponseDto, Client, LoginRequestDto } from "../api/client";
import { ConfigurationProvider } from "../api/client-base";
import {
  getFromCacheOrUseCallback,
  setCacheIfPossible,
  removeCacheIfPossible,
  getFromCache,
} from "./cache-helper";

const baseUrl = import.meta.env.VITE_API_BASE_URL;

type AuthContextType = {
  user: AuthResponseDto | null;
  login: (credentials: LoginRequestDto) => Promise<void>;
  logout: () => void;
};

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<AuthResponseDto | null>(() => {
    return getFromCache<AuthResponseDto>("authToken", true);
  });

  const login = useCallback(async (credentials: LoginRequestDto) => {
    const client = new Client(new ConfigurationProvider(undefined, baseUrl));

    const response = await getFromCacheOrUseCallback<AuthResponseDto>(
      "authToken",
      async () => {
        const loginResponse = await client.login(credentials);
        if (!loginResponse?.token) throw new Error("Login failed");
        return loginResponse;
      },
      true,
      true
    );

    setCacheIfPossible("authToken", response, true);
    setUser(response);
  }, []);

  const logout = useCallback(() => {
    removeCacheIfPossible("authToken", true);
    setUser(null);
  }, []);

  return <AuthContext.Provider value={{ user, login, logout }}>{children}</AuthContext.Provider>;
}
