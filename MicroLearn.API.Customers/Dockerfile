#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1803 AS build
WORKDIR /src
COPY ["MicroLearn.API.Customers/MicroLearn.API.Customers.csproj", "MicroLearn.API.Customers/"]
RUN dotnet restore "MicroLearn.API.Customers/MicroLearn.API.Customers.csproj"
COPY . .
WORKDIR "/src/MicroLearn.API.Customers"
RUN dotnet build "MicroLearn.API.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroLearn.API.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroLearn.API.Customers.dll"]