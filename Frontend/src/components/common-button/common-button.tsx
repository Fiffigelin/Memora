import "./common-button.scss";

export type CommonButtonProps = {
  title: string;
  onClick: () => void;
  variant: "default" | "inactive" | "warning";
  disabled?: boolean;
  loading?: boolean;
};

export default function CommonButton({
  title,
  onClick,
  variant,
  disabled,
  loading,
}: CommonButtonProps) {
  return (
    <button
      type="button"
      className={`btn-container ${variant} ${disabled ? "disabled" : ""} ${loading ? "loading" : ""}`}
      onClick={onClick}
      disabled={disabled}
    >
      <span className="btn-font">{title}</span>
    </button>
  );
}
