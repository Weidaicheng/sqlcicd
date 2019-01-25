# sqlcicd
A CI/CD tool for SQL Database



#### Basic Usage

- Integrate(Sql syntax check)

```powershell
dotnet.exe sqlcicd.dll --integrate <repo path>
```



- Delivery(Sql execute)

```powershell
dotnet.exe sqlcicd.dll --delivery <repo path>
```



- Get help

```powershell
dotnet.exe sqlcicd.dll --help
```



There should be a `.sqlcide` file under the root of repo path, and that file contains the content below.

```
DbType:Mssql
RepositoryType:Git
Server:localhost
Database:Demo
UserId:sa
Password:your password
```

DbType: the database server you're using, for now, only `Mssql` is supported.

RepositoryType: the version control tool you're using, for now, only `Git` is supported.

And optional, you can add a `.sqlignore` file under the root of repo path, and add some lines like this to ignore the exclude the sql files which you don't want to execute.

```
noneed.sql
exclude.sql
```

Also, you can add a `.sqlorder` file under the root of repo path, and add some lines like this to specify the order of sql files execution.

```
table_Author.sql
table_Author_AddAge.sql
table_Blog.sql
sp_CreateAuthor.sql
tables/table_Comment.sql
tables/table_Comment_AddIsDeleted.sql
```



