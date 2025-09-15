import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import AppRoutes from "./routes/app-routes.tsx";
import { AuthProvider } from "./context/auth-context.tsx";

import "./index.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <AppRoutes />
    </AuthProvider>
  </StrictMode>
);
