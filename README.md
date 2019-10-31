# Developing APIs with ASP.NET Core and EF Core and
# Developing APIs with ASP.NET Core and Dapper

Project for courses 1974 and 1976 from balta.io.

## Built With

- [.NET CORE](https://docs.microsoft.com/pt-br/dotnet/core/)

## Instructions

- This project uses docker to run SQL Server, so to setup your enviroment, follow Microsoft [guide](https://docs.microsoft.com/pt-br/sql/linux/quickstart-install-connect-docker?view=sql-server-2017&pivots=cs1-powershell) to install and run
SQL Server 2017 for Linux with Docker.

- Remember to change connection string on appsettings.json.

- Install ef core tools with you does not have it yet:
```
    dotnet tool install --global dotnet-ef
```

- Restore project, just for safe:
```
    dotnet restore
```

- And finally, run the above command to create the database:
```
    dotnet ef database update
```

## Authors

- **Vinícius Avansini** - [GitHub](https://github.com/viavn)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
