#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine3.14-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.14-amd64 AS build
WORKDIR /src
COPY ["Employees.api/Employees.api.csproj", "Employees.api/"]
COPY ["Employees.Models/Employees.Models.csproj", "Employees.Models/"]
COPY ["Employees.Business/Employees.Business.csproj", "Employees.Business/"]
COPY ["Employees.Data/Employees.Data.csproj", "Employees.Data/"]
COPY ["Employees.Dtos/Employees.Dtos.csproj", "Employees.Dtos/"]
RUN dotnet restore "Employees.api/Employees.api.csproj"
COPY . .
WORKDIR "/src/Employees.api"
RUN dotnet build "Employees.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employees.api.csproj" -p:PublishSingleFile=true -r linux-musl-x64 --self-contained true -p:PublishTrimmed=True -p:TrimMode=Link -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Employees.Api"]