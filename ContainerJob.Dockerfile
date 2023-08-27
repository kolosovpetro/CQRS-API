FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MoviesAPI.Core/MoviesAPI.Core.csproj", "MoviesAPI.Core/"]
COPY ["MoviesAPI.Data/MoviesAPI.Data.csproj", "MoviesAPI.Data/"]
COPY ["MoviesAPI.Models/MoviesAPI.Models.csproj", "MoviesAPI.Models/"]
COPY ["MoviesAPI.Requests/MoviesAPI.Requests.csproj", "MoviesAPI.Requests/"]
RUN dotnet restore "MoviesAPI.Core/MoviesAPI.Core.csproj"
COPY . .
WORKDIR "/src/MoviesAPI.Core"
RUN dotnet build "MoviesAPI.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoviesAPI.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false