FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication1/OngTDE.BackEnd.csproj", "WebApplication1/"]

RUN dotnet restore "WebApplication1/OngTDE.BackEnd.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "OngTDE.BackEnd.csproj" --c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OngTDE.BackEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OngTDE.BackEnd.dll"]