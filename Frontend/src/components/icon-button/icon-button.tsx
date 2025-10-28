import { RiAddCircleFill, RiAddCircleLine } from "react-icons/ri";
import { MdModeEditOutline, MdOutlineEdit, MdDeleteOutline, MdDelete } from "react-icons/md";

import "./icon-button.scss";
import { JSX, useState } from "react";

export type IconButtonProps = {
  type: "edit" | "delete" | "add";
};

export default function IconButton({ type }: IconButtonProps) {
  const [isActive, setActive] = useState(false);

  const typeIcon = (type: string): JSX.Element | null => {
    switch (type) {
      case "edit":
        return isActive ? <MdModeEditOutline /> : <MdOutlineEdit />;
      case "delete":
        return isActive ? <MdDelete /> : <MdDeleteOutline />;
      case "add":
        return isActive ? <RiAddCircleFill /> : <RiAddCircleLine />;
      default:
        return null;
    }
  };

  const handleClick = () => {
    setActive((prev) => !prev);
    // logik för att byta sida
  };

  return (
    <div className={`icon-button ${type}`} onClick={handleClick}>
      {typeIcon(type)}
    </div>
  );
}
