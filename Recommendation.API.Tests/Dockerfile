#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Recommendation.API.Tests/Recommendation.API.Tests.csproj", "Recommendation.API.Tests/"]
COPY ["Recommendation.API/Recommendation.API.csproj", "Recommendation.API/"]
RUN dotnet restore "Recommendation.API.Tests/Recommendation.API.Tests.csproj"
COPY . .
WORKDIR "/src/Recommendation.API.Tests"
RUN dotnet build "Recommendation.API.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recommendation.API.Tests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recommendation.API.Tests.dll"]