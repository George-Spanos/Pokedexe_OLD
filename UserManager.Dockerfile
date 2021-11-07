# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


# Copy everything else and build
COPY ./SharedKernel ./SharedKernel
COPY ./Model ./Model
COPY ./UserManager ./UserManager

# Copy everything else and build
COPY ./UserManager ./UserManager
RUN dotnet publish UserManager -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "UserManager.dll"]
EXPOSE 443