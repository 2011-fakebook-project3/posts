FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app/src

# --------------------------------
COPY Fakebook.Posts.Domain/*.csproj Fakebook.Posts.Domain/
COPY Fakebook.Posts.DataAccess/*.csproj Fakebook.Posts.DataAccess/
COPY Fakebook.Posts.RestApi/*.csproj Fakebook.Posts.RestApi/
COPY Fakebook.Posts.UnitTests/*.csproj Fakebook.Posts.UnitTests/
COPY *.sln ./
RUN dotnet restore
# ---------------------------------
COPY .config ./
RUN dotnet tool restore
# ---------------------------------

COPY . ./

# generate SQL script from migrations
RUN dotnet ef migrations script -p Fakebook.Posts.DataAccess -s Fakebook.Posts.RestApi -o ../init-db.sql -i

FROM postgres:13.1-alpine AS runtime

WORKDIR /docker-entrypoint-initdb.d

ENV POSTGRES_PASSWORD Pass@word

COPY --from=build /app/init-db.sql .
