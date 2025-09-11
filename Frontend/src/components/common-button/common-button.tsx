import "./common-button.scss";

export type CommonButtonProps = {
  title: string;
  onClick: () => void;
  variant: "default" | "inactive" | "warning";
  disabled?: boolean;
};

export default function CommonButton({ title, onClick, variant, disabled }: CommonButtonProps) {
  return (
    <button
      type="button"
      className={`btn-container ${variant} ${disabled ? "disabled" : ""}`}
      onClick={onClick}
      disabled={disabled}
    >
      <span className="btn-font">{title}</span>
    </button>
  );
}
