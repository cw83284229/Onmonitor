#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#使用asp.net core 3.1作为基础镜像，起一个别名为base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app #设置容器的工作目录为/app
EXPOSE 80  #暴露80端口
EXPOSE 443

#使用.net core sdk 3.1作为基础镜像，起一个别名为build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["host/OnMonitor.HttpApi.Host/OnMonitor.HttpApi.Host.csproj", "host/OnMonitor.HttpApi.Host/"]
COPY ["src/OnMonitor.HttpApi/OnMonitor.HttpApi.csproj", "src/OnMonitor.HttpApi/"]
COPY ["src/OnMonitor.Application.Contracts/OnMonitor.Application.Contracts.csproj", "src/OnMonitor.Application.Contracts/"]
COPY ["src/OnMonitor.Domain.Shared/OnMonitor.Domain.Shared.csproj", "src/OnMonitor.Domain.Shared/"]
COPY ["src/Utility/Utility.csproj", "src/Utility/"]
COPY ["src/OnMonitor.Application/OnMonitor.Application.csproj", "src/OnMonitor.Application/"]
COPY ["src/OnMonitor.Domain/OnMonitor.Domain.csproj", "src/OnMonitor.Domain/"]
COPY ["host/OnMonitor.Host.Shared/OnMonitor.Host.Shared.csproj", "host/OnMonitor.Host.Shared/"]
COPY ["src/OnMonitor.EntityFrameworkCore/OnMonitor.EntityFrameworkCore.csproj", "src/OnMonitor.EntityFrameworkCore/"]
RUN dotnet restore "host/OnMonitor.HttpApi.Host/OnMonitor.HttpApi.Host.csproj" #执行dotnet restore命令，相当于平时用vs还原nuget包
COPY . .#拷贝当前目录的文件到到容器的/src目录
WORKDIR "/src/host/OnMonitor.HttpApi.Host"
#执行dotnet build命令，相当于平时用vs生成项目。以Release模式生成到容器的/app/build目录
RUN dotnet build "OnMonitor.HttpApi.Host.csproj" -c Release -o /app/build
#将上面的build(.net core sdk 3.1)作为基础镜像，又重命名为publish
FROM build AS publish
#执行dotnet publish命令，相当于平时用vs发布项目。以Release模式发布到容器的/app/publish目录
RUN dotnet publish "OnMonitor.HttpApi.Host.csproj" -c Release -o /app/publish
#将上面的base(asp.net core 3.1)作为基础镜像，又重命名为final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#指定容器入口命令，容器启动时会运行dotnet WebApplication1.dll
ENTRYPOINT ["dotnet", "OnMonitor.HttpApi.Host.dll"]