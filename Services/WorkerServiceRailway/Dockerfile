FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /dotnetapp
COPY bin/Release/netcoreapp3.1/publish/docker .
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet WorkerServiceRailway.dll


