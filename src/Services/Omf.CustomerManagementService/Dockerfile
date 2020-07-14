FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /code

COPY . .

RUN dotnet restore

RUN dotnet publish --output /out/ --configuration Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

COPY --from=build /out /app/

WORKDIR /app

ENTRYPOINT ["dotnet", "Omf.CustomerManagementService.dll"]