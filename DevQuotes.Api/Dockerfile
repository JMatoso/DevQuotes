#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../DevQuotes.Api/DevQuotes.Api.csproj", "DevQuotes.Api/"]
COPY ["../DevQuotes.Application/DevQuotes.Application.csproj", "DevQuotes.Application/"]
COPY ["../DevQuotes.Communication/DevQuotes.Communication.csproj", "DevQuotes.Communication/"]
COPY ["../DevQuotes.Shared/DevQuotes.Shared.csproj", "DevQuotes.Shared/"]
COPY ["../DevQuotes.Domain/DevQuotes.Domain.csproj", "DevQuotes.Domain/"]
COPY ["../DevQuotes.Exceptions/DevQuotes.Exceptions.csproj", "DevQuotes.Exceptions/"]
COPY ["../DevQuotes.Infrastructure/DevQuotes.Infrastructure.csproj", "DevQuotes.Infrastructure/"]
RUN dotnet restore "./DevQuotes.Api/DevQuotes.Api.csproj"
COPY . .
WORKDIR "/src/DevQuotes.Api"
RUN dotnet build "./DevQuotes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevQuotes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevQuotes.Api.dll"]