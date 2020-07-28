# configuring database
Configure SQL server using docker 
```
docker pull mcr.microsoft.com/mssql/server
```

# commands to execute into database
```
CREATE LOGIN commander
WITH PASSWORD = 'Hco123456789';

CREATE USER [commander]
FROM LOGIN [commander]
WITH DEFAULT_SCHEMA=dbo;

ALTER ROLE db_owner ADD MEMBER commander;

USE master;
GRANT CREATE ANY DATABASE TO commander;
```

# To execute migrations into database
```
dotnet ef database update
```

# To check for errors
```
dotnet build
```

# To start project
```
dotnet run
```
