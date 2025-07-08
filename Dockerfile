# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore and publish
RUN dotnet restore ./LoanCalculator/LoanCalculator.csproj
RUN dotnet publish ./LoanCalculator/LoanCalculator.csproj -c Release -o /app/publish

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "LoanCalculator.dll"]
