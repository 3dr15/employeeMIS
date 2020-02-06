FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# COPY /PracticeAPI/*.csproj ./
# COPY /PracticeAPI.BLL/*.csproj ./
# COPY /PracticeAPI.DLL/*.csproj ./

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PracticeAPI.dll"]