FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln .
COPY ["src/Sher.Api/*.csproj", "./src/Sher.Api/"]
COPY ["src/Sher.Application/*.csproj", "./src/Sher.Application/"]
COPY ["src/Sher.Core/*.csproj", "./src/Sher.Core/"]
COPY ["src/Sher.Infrastructure/*.csproj", "./src/Sher.Infrastructure/"]

COPY ["tests/Sher.UnitTests/*.csproj", "./tests/Sher.UnitTests/"]
COPY ["tests/Sher.IntegrationTests/*.csproj", "./tests/Sher.IntegrationTests/"]

RUN dotnet restore
COPY . .
ENTRYPOINT ["dotnet", "test", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=opencover", "/p:CoverletOutput=/shared/"]
