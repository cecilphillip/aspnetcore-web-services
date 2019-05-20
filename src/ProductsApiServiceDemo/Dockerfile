FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.5 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2.204 AS build
WORKDIR /src
COPY ProductsApiServiceDemo.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish ProductsApiServiceDemo.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductsApiServiceDemo.dll"]