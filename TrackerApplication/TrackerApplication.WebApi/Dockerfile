#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrackerApplication.WebApi/TrackerApplication.WebApi.csproj", "TrackerApplication.WebApi/"]
RUN dotnet restore "TrackerApplication.WebApi/TrackerApplication.WebApi.csproj"
COPY . .
WORKDIR "/src/TrackerApplication.WebApi"
RUN dotnet build "TrackerApplication.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrackerApplication.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackerApplication.WebApi.dll"]