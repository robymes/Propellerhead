FROM microsoft/aspnetcore-build:2.0.6-2.1.104 AS build-stage
LABEL maintainer Roberto Messora <robymes@gmail.com>
COPY src src
WORKDIR src
RUN dotnet restore RobyMes.Propellerhead.sln
RUN dotnet build RobyMes.Propellerhead.sln
WORKDIR RobyMes.Propellerhead.Web
RUN dotnet publish --output /build --configuration Release

FROM microsoft/aspnetcore:2.0.6 AS final-stage
WORKDIR app
COPY --from=build-stage /build .
EXPOSE 80
ENTRYPOINT ["dotnet", "RobyMes.Propellerhead.Web.dll"]