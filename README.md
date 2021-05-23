- mssql server を起動

```shell
docker-compose up -d

docker-compose ps
```

- はじめに接続用のユーザを作っておく
    - 作成後は、 [appsettions.json](./appsettings.json) に設定を記載

```sql
create LOGIN BookApi with PASSWORD = '1qazxsw2!';

ALTER SERVER ROLE sysadmin ADD MEMBER BookApi; 
```

- migration の実行

```shell
# .gitignore に追加し忘れたため
rm -rf migrations

dotnet ef migrations add InitialDatabaseCreation

dotnet ef database update
```

- migration がうまくいかず、一度 DB を削除したい場合

```sql
-- DB に繋いでるプロセスの確認
select * from sys.sysprocesses where dbid = DB_ID('Book');

-- 強制的に削除する
USE master;
ALTER DATABASE Book SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE Book;

-- migration ディレクトリを削除し、再度 migration を実行すると DB から再作成される
````

- 起動
    - シードデータを流す場合は [Startup.cs](./Startup.cs) のコメントアウトを解除

```shell
dotnet run
```
