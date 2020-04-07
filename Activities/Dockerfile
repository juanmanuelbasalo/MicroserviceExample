FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5050

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Activities/Activities.csproj", "Activities/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Activities/Activities.csproj"
COPY . .
WORKDIR "/src/Activities"
RUN dotnet build "Activities.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Activities.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Activities.dll"]
