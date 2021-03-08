#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MyVotingApp/MyVotingApp.csproj", "MyVotingApp/"]
COPY ["TokenService/TokenService.csproj", "TokenService/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "MyVotingApp/MyVotingApp.csproj"
COPY . .
WORKDIR "/src/MyVotingApp"
RUN dotnet build "MyVotingApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVotingApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyVotingApp.dll"]