# Create HTTPS certificates for Docker

re [Hosting ASP.NET Core images with Docker Compose over HTTPS](https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https)

```shell
dotnet dev-certs https -ep \${HOME}/.aspnet/https/productserviceapi.pfx -p 1111
dotnet dev-certs https --trust
```

```yaml
services:
  environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ASPNETCORE_URLS=https://+:443;http://+:80
    - ASPNETCORE_Kestrel__Certificates__Default__Password=1111
    - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/productserviceapi.pfx
  volumes:
    - ~/.aspnet/https:/https:ro
```

```shell
docker run --name <your-name> -p 80:80 -p 443:443 -v ${HOME}/.aspnet/https:/https/ <your-image>
```
