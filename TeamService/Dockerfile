FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TeamService.Core/TeamService.Core.csproj", "TeamService.Core/"]
COPY ["TeamService.Domain/TeamService.Domain.csproj", "TeamService.Domain/"]
COPY ["TeamService.Application/TeamService.Application.csproj", "TeamService.Application/"]
COPY ["TeamService.Infrastructure/TeamService.Infrastructure.csproj", "TeamService.Infrastructure/"]
COPY ["TeamService/Team.TeamService.csproj", "TeamService.TeamService/"]

RUN dotnet restore "TeamService/TeamService.csproj"

COPY . .

RUN dotnet publish "TeamService/TeamService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TeamService.dll"]