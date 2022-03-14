#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Identity/MyStore.Services.Identity.API/MyStore.Services.Identity.API.csproj", "Services/Identity/MyStore.Services.Identity.API/"]
COPY ["Services/Identity/MyStore.Services.Identity.Repository/MyStore.Services.Identity.Repository.csproj", "Services/Identity/MyStore.Services.Identity.Repository/"]
COPY ["Services/Identity/MyStore.Services.Identity.Domain/MyStore.Services.Identity.Domain.csproj", "Services/Identity/MyStore.Services.Identity.Domain/"]
COPY ["Core/MyStore.Core.EntityFrameworkCore.SqlServer/MyStore.Core.EntityFrameworkCore.SqlServer.csproj", "Core/MyStore.Core.EntityFrameworkCore.SqlServer/"]
COPY ["Core/MyStore.Core.EntityFrameworkCore/MyStore.Core.EntityFrameworkCore.csproj", "Core/MyStore.Core.EntityFrameworkCore/"]
RUN dotnet restore "Services/Identity/MyStore.Services.Identity.API/MyStore.Services.Identity.API.csproj"
COPY . .

WORKDIR /src/Services/Identity/MyStore.Services.Identity.API
RUN dotnet build "MyStore.Services.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyStore.Services.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyStore.Services.Identity.API.dll"]