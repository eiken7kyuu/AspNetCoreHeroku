FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HerokuDeploySample.csproj", "HerokuDeploySample/"]
RUN dotnet restore "HerokuDeploySample/HerokuDeploySample.csproj"
WORKDIR "/src/HerokuDeploySample"
COPY . .

RUN dotnet build "HerokuDeploySample.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "HerokuDeploySample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HerokuDeploySample.dll