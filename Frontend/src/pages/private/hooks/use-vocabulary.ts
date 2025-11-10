import { useState, useCallback, useMemo } from "react";
import { Client, VocabularyListDto, VocabularyListDtoApiResponse } from "../../../api/client";
import { ConfigurationProvider } from "../../../api/client-base";
import { useAuthContext } from "../../../context/auth-context";

const baseUrl = import.meta.env.VITE_API_BASE_URL;

export function useVocabulary() {
  const { user } = useAuthContext();
  const [lists, setLists] = useState<VocabularyListDto[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const client = useMemo(() => {
    return new Client(new ConfigurationProvider(user?.token, baseUrl));
  }, [user?.token]);

  const refetch = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await client.getAllListsByUser();
      if (!response.success) throw new Error(response.message);
      setLists(response.data ?? []);
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        throw err;
      }
    } finally {
      setIsLoading(false);
    }
  }, [client]);

  // CREATE
  // ================= EJ TESTAD FUNKAR BARA TEORETISKT XD om ens det ======================
  // const createList = useCallback(
  //   async (data: VocabularyListDto) => {
  //     setIsLoading(true);
  //     setError(null);
  //     try {
  //       const response = await client.createVocabularylist(data);
  //       if (!response.success) throw new Error(response.message);
  //       await refetch(); // uppdatera listor direkt efter skapande
  //       return response.data;
  //     } catch (err: any) {
  //       setError(err.message);
  //       throw err;
  //     } finally {
  //       setIsLoading(false);
  //     }
  //   },
  //   [refetch]
  // );

  // UPDATE
  // ================= EJ TESTAD FUNKAR BARA TEORETISKT XD om ens det ======================
  // const updateList = useCallback(
  //   async (id: string, data: VocabularyListDto) => {
  //     setIsLoading(true);
  //     setError(null);
  //     try {
  //       const response = await client.update(id, data);
  //       if (!response.success) throw new Error(response.message);
  //       await refetch(); // uppdatera listor efter uppdatering
  //       return response.data;
  //     } catch (err: any) {
  //       setError(err.message);
  //       throw err;
  //     } finally {
  //       setIsLoading(false);
  //     }
  //   },
  //   [refetch]
  // );

  const fetchListById = useCallback(
    async (listId: string) => {
      setIsLoading(true);
      setError(null);
      try {
        const response: VocabularyListDtoApiResponse = await client.getListByListId(listId);
        if (!response.success) throw new Error(response.message);
        return response.data;
      } catch (err: unknown) {
        if (err instanceof Error) {
          setError(err.message ?? "Unknown error");
        } else {
          throw err;
        }
      } finally {
        setIsLoading(false);
      }
    },
    [client]
  );

  const deleteList = useCallback(
    async (listId: string) => {
      setIsLoading(true);
      setError(null);
      try {
        const response: VocabularyListDtoApiResponse = await client.deleteListById(listId);
        if (!response.success) throw new Error(response.message);
        await refetch(); // uppdatera listor efter radering
        return response.data;
      } catch (err: unknown) {
        if (err instanceof Error) {
          setError(err.message ?? "Unknown error");
        } else {
          throw err;
        }
      } finally {
        setIsLoading(false);
      }
    },
    [client, refetch]
  );

  return {
    lists,
    isLoading,
    error,
    refetch,
    fetchListById,
    deleteList,
  };
}
