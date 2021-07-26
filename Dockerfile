ARG APP_ENTRYPOINT=tests

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.sln .
COPY src/Sher.Api/*.csproj ./src/Sher.Api/
COPY src/Sher.Application/*.csproj ./src/Sher.Application/
COPY src/Sher.Core/*.csproj ./src/Sher.Core/
COPY src/Sher.Infrastructure/*.csproj ./src/Sher.Infrastructure/
COPY src/Sher.SharedKernel/*.csproj ./src/Sher.SharedKernel/

COPY tests/Sher.UnitTests/*.csproj ./tests/Sher.UnitTests/
COPY tests/Sher.IntegrationTests/*.csproj ./tests/Sher.IntegrationTests/

RUN dotnet restore

COPY src ./src/
COPY tests ./tests/

WORKDIR /source/src/Sher.Api
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS tests
WORKDIR /app
COPY --from=build /source ./
ENTRYPOINT ["dotnet", "test", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=opencover", "/p:CoverletOutput=/shared/"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS api
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Sher.Api.dll"]

FROM ${APP_ENTRYPOINT}