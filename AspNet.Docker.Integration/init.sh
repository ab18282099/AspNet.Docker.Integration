#!/usr/bin/env bash
# 下面是原本的作法，但是因為 ef database update 只有第一次會成功，第一次建立db完成之後，再次update 會產生衝突
# 所以最後棄用 ef migration
# cd /app/AspNet.Docker.Integration.Repository &&\

# add migrations
# dotnet ef migrations add InitialCreatePostgres -c DockerPostgresDbContext &&\
# dotnet ef migrations add InitialCreateSqlServer -c DockerSqlServerDbContext &&\

# update db
# dotnet ef database update -c DockerPostgresDbContext &&\
# dotnet ef database update -c DockerSqlServerDbContext &&\

#cd /app/AspNet.Docker.Integration

# run host
#dotnet run