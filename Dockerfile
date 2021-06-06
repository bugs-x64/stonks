# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./src/StonksCore/StonksCore.csproj ./StonksCore.csproj
COPY ./src/StonksWebApi/StonksWebApi.csproj ./StonksWebApi.csproj
RUN dotnet restore StonksWebApi.csproj
RUN dotnet restore StonksCore.csproj

# Copy everything else and build
COPY ./src ./
RUN dotnet publish ./StonksApi.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_ENVIRONMENT="Docker-Development"
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "StonksWebApi.dll"]