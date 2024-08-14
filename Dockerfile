FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Projeto API Power BI.csproj", "./"]
RUN dotnet restore "Projeto API Power BI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Projeto API Power BI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projeto API Power BI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projeto API Power BI.dll"]
