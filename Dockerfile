FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .

RUN dotnet restore "./src/Membership.Notifier/Membership.Notifier.csproj" --disable-parallel 
RUN dotnet publish "./src/Membership.Notifier/Membership.Notifier.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS http://*:5001
ENV ASPNETCORE_ENVIRONMENT docker

ENTRYPOINT ["dotnet", "Membership.Notifier.dll"]