# AspNet.Docker.Integration

> This project is built using [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.1&tabs=visual-studio).  
> The target is learning ASP.NET core with Dependency injection([Autofac](https://autofac.org/)) using repository pattern and technique of Docker.

> Welcome technical exchange, if this project has mistake of code or concept of programming, let me know, thanks!

## Environment

Ubuntu 18.04

Docker 17.05.0-ce

dotnet SDK 2.1

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

## Simple Test with URL

Connect to database that run in docker and insert some data, then GET /Test/GetUsers and GET /Test/GetOrders.