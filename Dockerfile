#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
RUN dir
COPY ["BarNone.API/BarNone.API.csproj", "BarNone.API/"]
COPY ["BarNone.BusinessLogic/BarNone.BusinessLogic.csproj", "BarNone.BusinessLogic/"]
COPY ["BarNone.DataLayer/BarNone.DataLayer.csproj", "BarNone.DataLayer/"]
RUN dotnet restore "BarNone.API/BarNone.API.csproj"
COPY . .
WORKDIR "/src"
RUN dir
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarNone.API.dll"]