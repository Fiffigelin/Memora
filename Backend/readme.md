## Kör projektet lokalt med HTTPS

Detta projekt kan köras både med HTTP och HTTPS lokalt.

***För att kunna köra frontenden med requests krävs https!*** 

### HTTPS-profil
1. Säkerställ att du har ett betrott self-signed certifikat:
```bash
dotnet dev-certs https --trust
```

2. Kör detta kommando i terminalen:
```bash
dotnet run --launch-profile "https"
```

