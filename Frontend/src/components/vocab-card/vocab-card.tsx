import { formatDate } from "../../utils/date-utils";

import "./vocab-card.scss";
import IconButton from "../icon-button/icon-button";

export type VocabCardProps = {
  id: string;
  createdAt: Date | undefined;
  language: string;
  title: string;
  amountOfWords: number;
};

export default function VocabCard({
  id,
  createdAt,
  language,
  title,
  amountOfWords,
}: VocabCardProps) {
  console.log(language);

  return (
    <div className="vocab-card" id={id}>
      <div className="vocab-header">
        <img src="../../../../public/1-698a9e66.png" alt="Bild" />
        <h2>{title}</h2>
      </div>
      <div className="vocab-body">
        <div className="vocab-content">
          {createdAt && <p style={{ color: " #000" }}>{formatDate(createdAt, "DAY_MONTH_YEAR")}</p>}
          {amountOfWords && <p>{`Antal ord i listan: ${amountOfWords}`}</p>}
        </div>
        <div className="vocab-buttons">
          <IconButton type={"add"} />
          <IconButton type={"edit"} />
          <IconButton type={"delete"} />
        </div>
      </div>
    </div>
  );
}
