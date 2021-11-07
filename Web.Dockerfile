FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app
COPY . .

RUN dotnet restore Web

RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

RUN npm install
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
EXPOSE 80
EXPOSE 443
ENTRYPOINT dotnet watch run --project Web
