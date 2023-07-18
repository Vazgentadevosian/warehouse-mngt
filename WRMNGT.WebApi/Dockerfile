FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WRMNGT.WebApi/WRMNGT.WebApi.csproj", "WRMNGT.WebApi/"]
RUN dotnet restore "WRMNGT.WebApi/WRMNGT.WebApi.csproj"
COPY . .
WORKDIR "/src/WRMNGT.WebApi"
RUN dotnet build "WRMNGT.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WRMNGT.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WRMNGT.WebApi.dll"]
