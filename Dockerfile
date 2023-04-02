#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GetStartedDigitalOceanDotNET001/GetStartedDigitalOceanDotNET001.csproj", "GetStartedDigitalOceanDotNET001/"]
RUN dotnet restore "GetStartedDigitalOceanDotNET001/GetStartedDigitalOceanDotNET001.csproj"
COPY . .
WORKDIR "/src/GetStartedDigitalOceanDotNET001"
RUN dotnet build "GetStartedDigitalOceanDotNET001.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GetStartedDigitalOceanDotNET001.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetStartedDigitalOceanDotNET001.dll"]