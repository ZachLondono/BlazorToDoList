FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToDoApp.csproj", "ToDoApp/"]
RUN dotnet restore "ToDoApp/ToDoApp.csproj"
COPY . .
WORKDIR "/src/ToDoApp"
RUN dotnet build "ToDoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.dll"]
