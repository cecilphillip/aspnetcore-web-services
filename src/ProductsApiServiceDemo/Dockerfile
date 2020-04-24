FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish ProductsApiServiceDemo.csproj -c Debug -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
WORKDIR /app 
ENV ASPNETCORE_ENVIRONMENT=Development \
    ASPNETCORE_URLS=https://+:443;http://+:80 \ 
    ASPNETCORE_Kestrel__Certificates__Default__Password=1111 \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/https/productserviceapi.pfx
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "ProductsApiServiceDemo.dll"]