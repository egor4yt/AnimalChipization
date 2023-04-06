FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AnimalChipization.Api/AnimalChipization.Api.csproj", "AnimalChipization.Api/"]
COPY ["AnimalChipization.Data/AnimalChipization.Data.csproj", "AnimalChipization.Data/"]
COPY ["AnimalChipization.Core/AnimalChipization.Core.csproj", "AnimalChipization.Core/"]
COPY ["AnimalChipization.Api.Contracts/AnimalChipization.Api.Contracts.csproj", "AnimalChipization.Api.Contracts/"]
COPY ["AnimalChipization.Services/AnimalChipization.Services.csproj", "AnimalChipization.Services/"]
COPY ["AnimalChipization.Mappers/AnimalChipization.Mappers.csproj", "AnimalChipization.Mappers/"]
RUN dotnet restore "AnimalChipization.Api/AnimalChipization.Api.csproj"
COPY . .
WORKDIR "/src/AnimalChipization.Api"
RUN dotnet build "AnimalChipization.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimalChipization.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get -y update; apt-get -y install curl 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalChipization.Api.dll"]
