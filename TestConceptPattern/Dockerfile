#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /appfromme
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestConceptPattern/TestConceptPattern.csproj","TestConceptPattern/"]
COPY ["Services/Services.csproj","Services/"]
RUN dotnet restore "TestConceptPattern/TestConceptPattern.csproj"
COPY . .
WORKDIR "/src/TestConceptPattern"
RUN dotnet build "TestConceptPattern.csproj" -o /appfromme/build
#RUN dotnet build "TestConceptPattern.csproj" -c Release -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "TestConceptPattern.csproj" -o /appfromme/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /appfromme
COPY --from=publish /appfromme/publish .
ENTRYPOINT ["dotnet", "TestConceptPattern.dll"]