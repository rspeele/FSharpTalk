open Rezoom
open Rezoom.Execution
open Rezoom.SQL.Mapping
open Rezoom.SQL.Mapping.Migrations
open Rezoom.SQL.Provider
open System.Data.SQLite
open System.Linq
open System.IO

type Migrations = SQLModel<".">

let migrate() =
    let dbname = "test.db"
    if not <| File.Exists(dbname) then
        SQLiteConnection.CreateFile(dbname)
    use conn = new SQLiteConnection("data source=" + dbname)
    conn.Open()
    let config =
        {   AllowMigrationsFromOlderMajorVersions = false
            LogMigrationRan = fun m -> printfn "Running migration: %s" m.FileName
        }
    Migrations.Migrate(config, conn)

type AddUser = SQL<"""
    insert into Users(Email, Name)
    values (@email, @name);
    select last_insert_rowid() as id;
""">

type AddToDo = SQL<"""
    insert into ToDos(ParentId, CreatedById, Heading, Paragraph, Completed)
    values (null, @byUserId, @heading, @paragraph, false);
""">

type ListUsersAndToDos = SQL<"""
    select u.Id, u.Email, u.Name, t.Heading as ToDoHeading, t.Paragraph as ToDoParagraph from Users u
    left join ToDos t on t.CreatedById = u.Id
""">

let sampleCode =
    plan {
        let! robert = AddUser.Command(email = "robert.peele@iticentral.com", name = "Robert Peele").ExecutePlan()
        let robertId = int (robert.First().id) // comes through as int64, have to explicitly convert to int
        for toDo in ["Prepare talk"; "Do actual work"; "Eat lunch"] do
            do! AddToDo.Command(byUserId = robertId, heading = toDo, paragraph = None).ExecutePlan()
        let! results = ListUsersAndToDos.Command().ExecutePlan()
        for result in results do
            printfn "%A %A %A %A" result.Id result.Email result.Name result.ToDoHeading
    }

[<EntryPoint>]
let main argv =
    migrate()

    let task = Execution.execute ExecutionConfig.Default sampleCode
    task.Wait()

    printfn "done"
    0 // return an integer exit code
