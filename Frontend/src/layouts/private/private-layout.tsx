import { Outlet } from "react-router-dom";
import "./private-layout.scss";

export default function PrivateLayout() {
  return (
    <div className="private-container">
      <div className="split left">
        <div className="center-container">
          <div className="logo-placeholder" />
        </div>
      </div>

      <div className="split right">
        <Outlet />
      </div>
    </div>
  );
}
