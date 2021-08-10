FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln .
COPY ["src/Sher.Api/*.csproj", "./Sher.Api/"]
COPY ["src/Sher.Application/*.csproj", "./Sher.Application/"]
COPY ["src/Sher.Core/*.csproj", "./Sher.Core/"]
COPY ["src/Sher.Infrastructure/*.csproj", "./Sher.Infrastructure/"]

RUN dotnet restore "/src/Sher.Api/Sher.Api.csproj"
COPY src .

WORKDIR "/src/Sher.Api"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sher.Api.dll"]
