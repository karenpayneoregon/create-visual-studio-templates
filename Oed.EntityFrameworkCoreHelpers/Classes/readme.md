# About

This folder contains methods to connect to a database with options to log information from Entity Framework Core for desktop applications.

All connections are read from an `appsettings.json` file as per below.

```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=True"
  }
}
```

**What above different environments?**

```json
{
  "ConnectionsConfiguration": {
    "ActiveEnvironment": "Production",
    "Development": "Dev connection string goes here",
    "Stage": "Stage connection string goes here",
    "Production": "Prod connection string goes here"
  }
}
```

**See also**

[Working with multiple environments for connection strings](https://github.com/karenpayneoregon/configuration-helpers)

**What about web applications?**

An environment is set on each server e.g. development, staging, production for `ASPNETCORE_ENVIRONMENT` or our own variable as per below for each server.

![image](../../assets/serverEnvironment.png)

In ASP.NET Core [the configuration](https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings#aspnet-core) system is very flexible, and the connection string could be stored in appsettings.json, an environment variable, the user secret store, or another configuration source

Base appsettings.json

```json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "DbConnectionConfig": {
    "DatabaseName": "OCS_MESSAGES",
    "UserName": "TODO",
    "Password": "TODO",
    "ServerName": "TODO"
  }
}

```