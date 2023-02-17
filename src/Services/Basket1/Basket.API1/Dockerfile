#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket1/Basket.API1/Basket.API1.csproj", "Services/Basket1/Basket.API1/"]
RUN dotnet restore "Services/Basket1/Basket.API1/Basket.API1.csproj"
COPY . .
WORKDIR "/src/Services/Basket1/Basket.API1"
RUN dotnet build "Basket.API1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API1.dll"]