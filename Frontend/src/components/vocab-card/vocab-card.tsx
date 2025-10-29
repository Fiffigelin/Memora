import { formatDate } from "../../utils/date-utils";
import { VocabularyListDto } from "../../api/client";
import IconButton from "../icon-button/icon-button";

import "./vocab-card.scss";

export type VocabCardProps = {
  vocabList: VocabularyListDto;
  // id: string;
  // createdAt: Date | undefined;
  // language: string;
  // title: string;
};

export default function VocabCard({ vocabList }: VocabCardProps) {
  const amountOfWords = vocabList.vocabularies?.length;

  return (
    <div className="vocab-card" id={vocabList.id}>
      <div className="vocab-header">
        <img src="../../../../public/1-698a9e66.png" alt="Bild" />
        <h2>{vocabList.title}</h2>
      </div>
      <div className="vocab-body">
        <div className="vocab-content">
          {vocabList.createdAt && (
            <p style={{ color: " #000" }}>{formatDate(vocabList.createdAt, "DAY_MONTH_YEAR")}</p>
          )}
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
