interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string;
  readonly VITE_OTHER_CONFIG?: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
