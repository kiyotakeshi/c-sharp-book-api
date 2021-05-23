```shell
create LOGIN BookApi with PASSWORD = '1qazxsw2!';

ALTER SERVER ROLE sysadmin ADD MEMBER BookApi; 

dotnet ef migrations add InitialDatabaseCreation
```
