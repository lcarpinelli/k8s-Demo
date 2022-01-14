#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["K8s-Frontend/K8s-Frontend.csproj", "K8s-Frontend/"]
RUN dotnet restore "K8s-Frontend/K8s-Frontend.csproj"
COPY . .
WORKDIR "/src/K8s-Frontend"
RUN dotnet build "K8s-Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K8s-Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K8s-Frontend.dll"]