#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TG.Configs.Api/TG.Configs.Api.csproj", "TG.Configs.Api/"]
COPY nuget.config .
RUN dotnet restore "TG.Configs.Api/TG.Configs.Api.csproj"
COPY . .
WORKDIR "/src/TG.Configs.Api"
RUN dotnet build "TG.Configs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TG.Configs.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TG.Configs.Api.dll"]