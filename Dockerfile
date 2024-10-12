#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.UI/Feijuca.Auth.UI.csproj", "Feijuca.Auth.UI/"]
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.Infra.CrossCutting/Feijuca.Auth.Infra.CrossCutting.csproj", "Feijuca.Auth.Infra.CrossCutting.csproj/"]
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.Application/Feijuca.Auth.Application.csproj", "Feijuca.Auth.Application.csproj/"]
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.Infra.Data/Feijuca.Auth.Infra.Data.csproj", "Feijuca.Auth.Infra.Data/"]
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.Domain/Feijuca.Auth.Domain.csproj", "Feijuca.Auth.Domain/"]
COPY ["src/Feijuca.Auth.Api/Feijuca.Auth.Common/Feijuca.Auth.Common.csproj", "Feijuca.Auth.Common/"]
RUN dotnet restore "Feijuca.Auth.UI/Feijuca.Auth.UI.csproj"
COPY . .

RUN dotnet build "src/Feijuca.Auth.Api/Feijuca.Auth.UI/Feijuca.Auth.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Feijuca.Auth.Api/Feijuca.Auth.UI/Feijuca.Auth.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feijuca.Auth.UI"]
