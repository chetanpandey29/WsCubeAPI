#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["WS_CubeAPI.csproj", ""]
RUN dotnet restore "WS_CubeAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "WS_CubeAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WS_CubeAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WS_CubeAPI.dll"]