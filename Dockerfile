FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY TaskHub.MVC/*.csproj TaskHub.MVC/
COPY TaskHub.Application/*.csproj TaskHub.Application/
COPY TaskHub.Infrastructure/*.csproj TaskHub.Infrastructure/
COPY TaskHub.Core/*.csproj TaskHub.Core/

RUN dotnet restore TaskHub.MVC/TaskHub.MVC.csproj

COPY . .
RUN dotnet publish TaskHub.MVC/TaskHub.MVC.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

COPY --from=build /app /app	

WORKDIR /app

CMD ["dotnet", "TaskHub.MVC.dll"]