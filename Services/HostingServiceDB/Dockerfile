FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /dotnetapp
COPY bin/Release/netcoreapp3.1/publish/docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet HostingServiceDB.dll