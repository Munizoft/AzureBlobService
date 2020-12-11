#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Munizoft.Azure.BlogService.WebAPI/Munizoft.Azure.BlogService.WebAPI.csproj", "Munizoft.Azure.BlogService.WebAPI/"]
RUN dotnet restore "Munizoft.Azure.BlogService.WebAPI/Munizoft.Azure.BlogService.WebAPI.csproj"
COPY . .
WORKDIR "/src/Munizoft.Azure.BlogService.WebAPI"
RUN dotnet build "Munizoft.Azure.BlogService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Munizoft.Azure.BlogService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Munizoft.Azure.BlogService.WebAPI.dll"]