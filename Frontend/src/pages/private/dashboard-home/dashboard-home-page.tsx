import { TiChevronRight } from "react-icons/ti";
import "./dashboard-home-page.scss";

export default function DashboardHome() {
  return (
    <nav className="sideboard">
      <header>
        <div className="image-text">
          <span className="image">
            <div></div>
          </span>

          <div className="text header-text">
            <span className="name">Memora</span>
            <span className="profession">GLOSOR!</span>
          </div>
        </div>

        <TiChevronRight className="toggle" />
      </header>
    </nav>
  );
}
