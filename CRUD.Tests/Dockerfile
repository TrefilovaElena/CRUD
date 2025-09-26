FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
WORKDIR CRUD.Tests



RUN dotnet restore

RUN dotnet build -c Release

CMD sleep 40 && dotnet test --configuration Release