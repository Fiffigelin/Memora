export async function getFromCacheOrUseCallback<T>(
  key: string,
  asyncCallback: () => Promise<T>,
  useLocal?: boolean,
  alwaysRunCallback?: boolean
): Promise<T> {
  try {
    const cachedValue = useLocal ? localStorage.getItem(key) : sessionStorage.getItem(key);
    if (cachedValue === null || alwaysRunCallback) return await asyncCallback();
    const parsedJson = JSON.parse(cachedValue);
    return parsedJson as T;
  } catch {
    return await asyncCallback();
  }
}

const setCacheWithJsonStringIfPossible = (key: string, value: string, useLocal?: boolean): void => {
  try {
    if (useLocal) localStorage.setItem(key, value);
    else sessionStorage.setItem(key, value);
  } catch (_) {
    // eslint-disable-next-line no-empty
  }
};

// har
export function setCacheIfPossible<T>(key: string, value: T, useLocal?: boolean): void {
  try {
    const jsonPayload: string = value ? JSON.stringify(value) : "";
    setCacheWithJsonStringIfPossible(key, jsonPayload, useLocal);
  } catch (_) {
    // eslint-disable-next-line no-empty
  }
}

// har
export const removeCacheIfPossible = (key: string, useLocal?: boolean): void => {
  try {
    if (useLocal) localStorage.removeItem(key);
    else sessionStorage.removeItem(key);
  } catch (_) {
    // eslint-disable-next-line no-empty
  }
};

// har
export const getFromCache = <T>(key: string, useLocal?: boolean): T | null => {
  try {
    const cachedValue = useLocal ? localStorage.getItem(key) : sessionStorage.getItem(key);
    if (cachedValue === null) return null;
    const parsedJson = JSON.parse(cachedValue);
    return parsedJson;
  } catch {
    return null;
  }
};
