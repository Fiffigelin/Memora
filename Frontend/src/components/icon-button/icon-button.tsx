import { JSX } from "react";
import { MdOutlineEdit, MdDeleteOutline } from "react-icons/md";

import "./icon-button.scss";

export type IconButtonProps = {
  type: "edit" | "delete";
  onHandleClick: () => void;
};

export default function IconButton({ type }: IconButtonProps) {
  const typeIcon = (type: string): JSX.Element | null => {
    switch (type) {
      case "edit":
        return <MdOutlineEdit />;
      case "delete":
        return <MdDeleteOutline />;
      default:
        return null;
    }
  };

  const typeText = (type: string): string => {
    switch (type) {
      case "edit":
        return "Redigera";
      case "delete":
        return "Radera";
      default:
        return "";
    }
  };

  const handleClick = () => {
    console.log("DELETE");
    // onHandleClick();
    // logik för att byta sida
  };

  return (
    <div className={`icon-button ${type}`} onClick={handleClick}>
      {typeIcon(type)}
      <p>{typeText(type)}</p>
    </div>
  );
}
