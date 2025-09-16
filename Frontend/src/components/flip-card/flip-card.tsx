import { useState } from "react";
import "./flip-card.scss";

export default function FlipCard() {
  const [flipped, setFlipped] = useState(false);

  return (
    <div className="card">
      <div
        className={`card-inner ${flipped ? "is-flipped" : ""}`}
        onClick={() => setFlipped(!flipped)}
      >
        <div className="card-face card-face-front">
          <h2>Developer Card</h2>
        </div>

        <div className="card-face card-face-back">
          <div className="card-content">
            <div className="card-header">
              <h2>Header</h2>
            </div>
            <div className="card-body">
              <h3>Text</h3>
              <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
                incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
                pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                deserunt mollit anim id est laborum.
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
