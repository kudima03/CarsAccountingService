#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cars.API/Cars.API.csproj", "Cars.API/"]
RUN dotnet restore "Cars.API/Cars.API.csproj"
COPY . .
WORKDIR "/src/Cars.API"
RUN dotnet build "Cars.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cars.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cars.API.dll"]