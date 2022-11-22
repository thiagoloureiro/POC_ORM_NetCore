#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/POC_ORM_NetCore/POC_ORM_NetCore.csproj", "src/POC_ORM_NetCore/"]
COPY ["src/Model/Model.csproj", "src/Model/"]
COPY ["src/Data.EntityFramework/Data.EntityFramework.csproj", "src/Data.EntityFramework/"]
COPY ["src/Data.Base/Data.Base.csproj", "src/Data.Base/"]
COPY ["src/Data.ADO/Data.ADO.csproj", "src/Data.ADO/"]
COPY ["src/Data.NHibernate/Data.NHibernate.csproj", "src/Data.NHibernate/"]
COPY ["src/Data.Dapper/Data.Dapper.csproj", "src/Data.Dapper/"]
RUN dotnet restore "src/POC_ORM_NetCore/POC_ORM_NetCore.csproj"
COPY . .
COPY wait-for-it.sh wait-for-it.sh 
RUN chmod +x wait-for-it.sh
WORKDIR "/src/src/POC_ORM_NetCore"
RUN dotnet build "POC_ORM_NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC_ORM_NetCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for-it.sh /wait-for-it.sh 
RUN chmod +x /wait-for-it.sh
ENTRYPOINT ["dotnet", "POC_ORM_NetCore.dll"]