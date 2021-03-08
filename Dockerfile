FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim as build-image

WORKDIR /home/app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . .

RUN dotnet publish ./MyVotingApp/MyVotingApp.csproj -o /publish/


FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
ENV DOTNET_VERSION=5.0.0
ENV ASPNET_VERSION=5.0.0
RUN dotnet --list-sdks
WORKDIR /publish

COPY --from=build-image /publish .

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"


ENTRYPOINT ["dotnet", "MyVotingApp.dll"]