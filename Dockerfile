FROM microsoft/dotnet
WORKDIR /app
COPY *.sln .
COPY AspNet.Docker.Integration/. ./AspNet.Docker.Integration/
COPY AspNet.Docker.Integration.Helper/. ./AspNet.Docker.Integration.Helper/
COPY AspNet.Docker.Integration.Repository/. ./AspNet.Docker.Integration.Repository/
COPY AspNet.Docker.Integration.UnitTest/. ./AspNet.Docker.Integration.UnitTest/
RUN dotnet restore --disable-parallel && dotnet build && dotnet test ./AspNet.Docker.Integration.UnitTest