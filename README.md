# AspNet.Docker.Integration

[![Build status](https://ci.appveyor.com/api/projects/status/csr4l6bm2nq9afsg?svg=true)](https://ci.appveyor.com/project/shengLin-alex/aspnet-docker-integration)

> This project is built using [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.1&tabs=visual-studio).  
> The target is learning ASP.NET core with Dependency injection([Autofac](https://autofac.org/)) using repository pattern and technique of Docker.

> Welcome technical exchange, if this project has mistake of code or concept of programming, let me know, thanks!

## Build Setup

Clone this project by
``` bash
git clone https://github.com/shengLin-alex/AspNet.Docker.Integration.git
```

Go to web project folder
``` bash
cd ./AspNet.Docker.Integration/AspNet.Docker.Integration
```

Then docker-compose up

``` bash
docker-compose up
```

Rebuild image
``` bash
sudo rm -rf ./data
docker-compose up --build
```

## .env example
```
APP_HOST_PORT=8001

POSTGRES_HOST_DIR=./data/postgres_data
POSTGRES_DB=develop
POSTGRES_USER=dockeradmin
POSTGRES_PASSWORD=dockeradmin
POSTGRES_PORT=5432

SQLSERVER_HOST_DIR=./data/sqlserver_data
SQLSERVER_PORT=1433
SQLSERVER_PASSWORD=dockERadmin_1
```


## Simple Test with RESTful API

POST /api/test/add/user, with body:
```json
{
  "name": "John Doe"
}
```

GET /api/test/get/users, the response will show:
```json
[
  {
    "id": 1,
    "name": "John Doe"
  }
]
```