# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LookingPromos.Worker/LookingPromos.Worker.csproj", "LookingPromos.Worker/"]
COPY ["LookingPromos.SharedKernel.Persistence/LookingPromos.SharedKernel.Persistence.csproj", "LookingPromos.SharedKernel.Persistence/"]
COPY ["LookingPromos.SharedKernel.Domain/LookingPromos.SharedKernel.Domain.csproj", "LookingPromos.SharedKernel.Domain/"]
COPY ["LookingPromos.SharedKernel.Contracts/LookingPromos.SharedKernel.Contracts.csproj", "LookingPromos.SharedKernel.Contracts/"]
COPY ["LookingPromos.SharedKernel/LookingPromos.SharedKernel.csproj", "LookingPromos.SharedKernel/"]
COPY ["LookingPromos.Worker.Application/LookingPromos.Worker.Application.csproj", "LookingPromos.Worker.Application/"]
COPY ["LookingPromos.Worker.Infrastructure/LookingPromos.Worker.Infrastructure.csproj", "LookingPromos.Worker.Infrastructure/"]
COPY ["LookingPromos.SharedKernel.Infrastructure/LookingPromos.SharedKernel.Infrastructure.csproj", "LookingPromos.SharedKernel.Infrastructure/"]
RUN dotnet restore "./LookingPromos.Worker/LookingPromos.Worker.csproj"
COPY . .
WORKDIR "/src/LookingPromos.Worker"
RUN dotnet build "./LookingPromos.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LookingPromos.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LookingPromos.Worker.dll"]