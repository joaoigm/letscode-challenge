FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /build
COPY Resistence.sln ./
COPY src/ ./src
RUN dotnet restore
RUN dotnet build --no-restore
RUN dotnet publish --no-restore --no-build -o /publish ./src/Resistence.Api

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS app
WORKDIR /app
COPY --from=build /publish ./
ENTRYPOINT [ "dotnet", "/app/Resistence.Api.dll" ]