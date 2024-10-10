#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Feijuca.Auth.Api/Api.csproj", "Api/"]
COPY ["src/Feijuca.Auth.Api.Infra.CrossCutting/CrossCutting.csproj", "Infra.CrossCutting/"]
COPY ["src/Feijuca.Auth.Api.Application/Application.csproj", "Application/"]
COPY ["src/Feijuca.Auth.Api.Infra.Data/Infra.Data.csproj", "Infra.Data/"]
COPY ["src/Feijuca.Auth.Api.Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Feijuca.Auth.Api/Api.csproj"
COPY . .

RUN dotnet build "src/Feijuca.Auth.Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Feijuca.Auth.Api/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feijuca.Auth.Api.dll"]