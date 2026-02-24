# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY FinanceTracker.Api/*.csproj ./FinanceTracker.Api/
COPY FinanceTracker.Application/*.csproj ./FinanceTracker.Application/
COPY FinanceTracker.Domain/*.csproj ./FinanceTracker.Domain/
COPY FinanceTracker.Infrastructure/*.csproj ./FinanceTracker.Infrastructure/

RUN dotnet restore

COPY . .
RUN dotnet publish FinanceTracker.Api -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "FinanceTracker.Api.dll"]