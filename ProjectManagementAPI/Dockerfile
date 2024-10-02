
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
USER app
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectManagementAPI/ProjectManagementAPI.csproj", "ProjectManagementAPI/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["BusinessObjects/BusinessObjects.csproj", "BusinessObjects/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["ControllerTests/ControllerTests.csproj", "ControllerTests/"]
RUN dotnet restore "./ProjectManagementAPI/ProjectManagementAPI.csproj"
COPY . .
WORKDIR "/src/ProjectManagementAPI"
RUN dotnet build "./ProjectManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagementAPI.dll"]
