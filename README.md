# ReadingList

# Configuration
## 1. Startup project
Right click on the `ReadingList` project in Solution Explorer and select `Set as Startup Projects`
    ![Set ReadingList as startup project](setAsStartupProject.png)

## 2. Database
1. Connection String
    - Change your connection string in [appsettings.json](ReadingList/appsettings.json)
```json
"ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=ReadingList;Trusted_Connection=True;"
  }
```

2. Initiate database
    - Choose `ReadingList` as Default project in Package Manager Console.
        ![ReadingList default project](setDefaultProject.png)
    - Run `update-database` command.
> Sample data would be inserted into database automatically by using the [seeder]()

### You can start the API now

## 3. React Client
The way I open client project is to open `ReadingList.Client` in Visual Studio Code.
- From there run those two commands in terminal.
    - `npm install`
    - `npm start`
