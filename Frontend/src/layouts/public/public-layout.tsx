import { Outlet } from "react-router-dom";
import "./public-layout.scss";

export default function PublicLayout() {
  const imgPath = "/colorful-speech-bubble-background-vector-21126512-removebg-preview.png";

  return (
    <div className="public-container">
      <div className="split left">
        <img className="img" src={imgPath} />
      </div>

      <div className="split right">
        <Outlet />
      </div>
    </div>
  );
}
