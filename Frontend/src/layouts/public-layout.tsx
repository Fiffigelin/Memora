import { Outlet } from "react-router-dom";
import "./public-layout.scss";

export default function PublicLayout() {
  return (
    <div className="layout-container">
      <div className="split left">
        <div className="center-container">
          <div className="logo-placeholder" />
        </div>
      </div>

      <div className="split right">
        {/* <div className="center-containter"> */}
        <Outlet />
        {/* </div> */}
      </div>
    </div>
  );
}
