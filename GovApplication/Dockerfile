#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GovApplication.csproj", "./"]
RUN dotnet restore "GovApplication.csproj"
COPY . .

WORKDIR "/src"
RUN dotnet build "GovApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GovApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ConnStr="Server=db;User ID=root;Password=root;Database=MyDB;"
ENTRYPOINT ["dotnet", "GovApplication.dll"]


