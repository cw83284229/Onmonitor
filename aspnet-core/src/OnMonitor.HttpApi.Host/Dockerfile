#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["src/OnMonitor.HttpApi.Host/OnMonitor.HttpApi.Host.csproj", "src/OnMonitor.HttpApi.Host/"]
COPY ["src/OnMonitor.HttpApi/OnMonitor.HttpApi.csproj", "src/OnMonitor.HttpApi/"]
COPY ["src/OnMonitor.Application.Contracts/OnMonitor.Application.Contracts.csproj", "src/OnMonitor.Application.Contracts/"]
COPY ["src/OnMonitor.Domain.Shared/OnMonitor.Domain.Shared.csproj", "src/OnMonitor.Domain.Shared/"]
COPY ["src/OnMonitor.Application/OnMonitor.Application.csproj", "src/OnMonitor.Application/"]
COPY ["src/OnMonitor.Domain/OnMonitor.Domain.csproj", "src/OnMonitor.Domain/"]
COPY ["src/Utility/Utility.csproj", "src/Utility/"]
COPY ["src/OnMonitor.EntityFrameworkCore.DbMigrations/OnMonitor.EntityFrameworkCore.DbMigrations.csproj", "src/OnMonitor.EntityFrameworkCore.DbMigrations/"]
COPY ["src/OnMonitor.EntityFrameworkCore/OnMonitor.EntityFrameworkCore.csproj", "src/OnMonitor.EntityFrameworkCore/"]
RUN dotnet restore "src/OnMonitor.HttpApi.Host/OnMonitor.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/OnMonitor.HttpApi.Host"
RUN dotnet build "OnMonitor.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnMonitor.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnMonitor.HttpApi.Host.dll"]