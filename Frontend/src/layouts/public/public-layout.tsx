import { Outlet } from "react-router-dom";
import "./public-layout.scss";

export default function PublicLayout() {
  const imgPath = "/colorful-speech-bubble-background-vector-21126512-removebg-preview.png";

  return (
    <div className="public-container">
      <div className="public-wrapper">
        <div className="left">
          <div className="img-wrapper">
            <img className="img" src={imgPath} />
            <p className="slackey-regular">memora</p>
          </div>
        </div>

        <div className="right">
          <Outlet />
        </div>
      </div>
    </div>
  );
}
