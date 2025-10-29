import { formatDate } from "../../utils/date-utils";
import { VocabularyListDto } from "../../api/client";
import IconButton from "../icon-button/icon-button";

import "./vocab-card.scss";

export type VocabCardProps = {
  vocabList: VocabularyListDto;
  onDelete: (id: string) => void;
};

export default function VocabCard({ vocabList, onDelete }: VocabCardProps) {
  const amountOfWords = vocabList.vocabularies?.length;

  return (
    <div className="vocab-card" id={vocabList.id}>
      <div className="vocab-header">
        <h2>{vocabList.title}</h2>
      </div>
      <div className="vocab-body">
        <img src="/1-698a9e66.png" alt="Bild" className="language-img" />
        {vocabList.createdAt && (
          <h3 style={{ color: " #000" }}>{formatDate(vocabList.createdAt, "DAY_MONTH_YEAR")}</h3>
        )}
        {amountOfWords && <h4>{`Antal ord i listan: ${amountOfWords}`}</h4>}
      </div>
      <div className="vocab-buttons">
        <IconButton type={"edit"} onHandleClick={() => {}} />
        <IconButton type={"delete"} onHandleClick={() => onDelete(vocabList.id || "")} />
      </div>
    </div>
  );
}
