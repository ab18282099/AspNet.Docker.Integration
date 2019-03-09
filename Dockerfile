FROM microsoft/dotnet
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY AspNet.Docker.Integration/*.csproj ./AspNet.Docker.Integration/
COPY AspNet.Docker.Integration.Helper/*.csproj ./AspNet.Docker.Integration.Helper/
COPY AspNet.Docker.Integration.Repository/*.csproj ./AspNet.Docker.Integration.Repository/
COPY AspNet.Docker.Integration.UnitTest/*.csproj ./AspNet.Docker.Integration.UnitTest/

# 必須取消並行下載，不然會出錯
RUN dotnet restore --disable-parallel

# copy everything else and build app
COPY AspNet.Docker.Integration/. ./AspNet.Docker.Integration/
COPY AspNet.Docker.Integration.Helper/. ./AspNet.Docker.Integration.Helper/
COPY AspNet.Docker.Integration.Repository/. ./AspNet.Docker.Integration.Repository/
COPY AspNet.Docker.Integration.UnitTest/. ./AspNet.Docker.Integration.UnitTest/
WORKDIR /app
RUN dotnet build

# run unit test
WORKDIR /app/AspNet.Docker.Integration.UnitTest
RUN dotnet test