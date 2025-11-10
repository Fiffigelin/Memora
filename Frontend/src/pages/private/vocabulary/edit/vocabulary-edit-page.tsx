import { useParams, useNavigate, useLocation } from "react-router-dom";

import "./vocabulary-edit-page.scss";
import { useState } from "react";
import { VocabularyListDto } from "../../../../api/client";
import { useVocabulary } from "../../hooks/use-vocabulary";
import ValidationInput from "../../../../components/validation-input/validation-input";

export default function VocabularyEdit() {
  const { id } = useParams<{ id?: string }>();
  const { isLoading } = useVocabulary();
  const location = useLocation();
  const navigate = useNavigate();

  const state = location.state as { list?: VocabularyListDto };
  const emptyList: VocabularyListDto = {
    id: "",
    title: "",
    vocabularies: [],
    createdAt: new Date(),
    language: "",
  };

  const [list] = useState<VocabularyListDto | undefined>(state.list ?? emptyList);
  const isNew = !id;

  // export interface UpdateVocabularyDto {
  //   id?: string | undefined;
  //   word?: string | undefined;
  //   translation?: string | undefined;
  // }

  // export interface UpdateVocabularyListDto {
  //   id: string;
  //   title?: string | undefined;
  //   vocabularies: UpdateVocabularyDto[] | undefined;
  // }

  // export interface CreateVocabularyListDto {
  //   title: string | undefined;
  //   language: string | undefined;
  //   vocabularies?: CreateVocabularyDto[] | undefined;
  // }

  const handleSave = () => {
    if (isNew) {
      console.log("YAY EN NY LISTA SKAPAS");
    } else {
      console.log("YAY LISTAN UPPDATERAS");
    }
    navigate("/vocabulary");
  };

  return (
    <div className="vocabulary-container">
      <h1>{isNew ? "Create Vocabulary" : `Edit Vocabulary ${id}`}</h1>
      <form className="vocabulary-form">
        <ValidationInput
          id={"title"}
          label={"Title"}
          validationmsg={"Title must contain atleast 3 chars"}
          isValid={false}
          placeholder={"Title"}
          value={list?.title || ""}
          onChange={(value: string) => {
            console.log(value);
          }}
        />
      </form>
      {isLoading && isNew && <p>Loading...</p>}

      <button onClick={handleSave}>Save</button>
    </div>
  );
}
