#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankCards.ApiOrm/BankCards.ApiOrm.csproj", "BankCards.ApiOrm/"]
COPY ["BankCards.Interfaces/BankCards.Interfaces.csproj", "BankCards.Interfaces/"]
COPY ["BankCards.Domain/BankCards.Domain.csproj", "BankCards.Domain/"]
COPY ["BankCards.Exceptions/BankCards.Exceptions.csproj", "BankCards.Exceptions/"]
COPY ["BankCards.Services/BankCards.Services.csproj", "BankCards.Services/"]
COPY ["BankCards.DAL/BankCards.DAL.csproj", "BankCards.DAL/"]
COPY ["BankCards.Security/BankCards.Security.csproj", "BankCards.Security/"]
RUN dotnet restore "BankCards.ApiOrm/BankCards.ApiOrm.csproj"
COPY . .
WORKDIR "/src/BankCards.ApiOrm"
RUN dotnet build "BankCards.ApiOrm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankCards.ApiOrm.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankCards.ApiOrm.dll"]