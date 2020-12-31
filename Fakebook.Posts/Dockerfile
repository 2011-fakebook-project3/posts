#################- Build and Publish -#####################

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app/src

# -------------------------------- 
# if the csproj file hasn't changed, we can cache up to this point
COPY Fakebook.Posts.Domain/*.csproj Fakebook.Posts.Domain/
COPY Fakebook.Posts.DataAccess/*.csproj Fakebook.Posts.DataAccess/
COPY Fakebook.Posts.RestApi/*.csproj Fakebook.Posts.RestApi/
COPY Fakebook.Posts.UnitTests/*.csproj Fakebook.Posts.UnitTests/
COPY *.sln ./
RUN dotnet restore
# --------------------------------- 

COPY . ./

RUN dotnet publish -o ../publish



#################- Package Assemblies -###################

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

CMD dotnet Fakebook.Posts.RestApi.dll