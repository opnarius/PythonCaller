FROM gcr.io/xpo-corp-ea-dev2/xea-dotnet-aspnetcore-python:2.2.7-1 AS base


WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src

COPY . .

WORKDIR /src/PythonCaller

RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "PythonCaller.dll"]