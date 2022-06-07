#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieConsumer/MovieConsumer.csproj", "MovieConsumer/"]
RUN dotnet restore "MovieConsumer/MovieConsumer.csproj"
COPY . .
WORKDIR "/src/MovieConsumer"
RUN dotnet build "MovieConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieConsumer.dll"]