export default class ClientBase {
  private configurationProvider: ConfigurationProvider | undefined;

  constructor(configurationProvider: ConfigurationProvider | undefined) {
    this.configurationProvider = configurationProvider;
  }

  getBaseUrl(generatedEndpoint: string, baseUrl?: string): string {
    return this.configurationProvider?.url ?? "";
  }

  transformOptions(options: RequestInit): Promise<RequestInit> {
    return new Promise<RequestInit>((resolve) => {
      const headersDictionary: Record<string, string> = options.headers as Record<string, string>;

      headersDictionary["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
      headersDictionary["X-Frame-Options"] = "SAMEORIGIN";

      if (this.configurationProvider?.accessToken) {
        headersDictionary["Authorization"] = `Bearer ${this.configurationProvider.accessToken}`;
      }

      options.headers = headersDictionary;
      options.credentials = "same-origin";
      return resolve(options);
    });
  }
}

export class ConfigurationProvider {
  public accessToken?: string;
  public url?: string;

  constructor(accessToken?: string, url?: string) {
    this.accessToken = accessToken;
    this.url = url;
  }
}
