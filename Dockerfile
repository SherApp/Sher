FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.sln .
COPY src/Sher.Api/*.csproj ./src/Sher.Api/
COPY src/Sher.Application/*.csproj ./src/Sher.Application/
COPY src/Sher.Core/*.csproj ./src/Sher.Core/
COPY src/Sher.Infrastructure/*.csproj ./src/Sher.Infrastructure/
COPY src/Sher.Infrastructure.FileProcessing/*.csproj ./src/Sher.Infrastructure.FileProcessing/
COPY src/Sher.SharedKernel/*.csproj ./src/Sher.SharedKernel/
RUN dotnet restore ./src/Sher.Api

COPY src ./src/
WORKDIR /source/src/Sher.Api
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Sher.Api.dll"]