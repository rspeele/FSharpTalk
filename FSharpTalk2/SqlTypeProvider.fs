module SqlTypeProvider
open Rezoom.SQL
open Rezoom.SQL.Synchronous
open System
open System.IO

type Model = SQLModel<".">

type InsertSQL = SQL<"""
    insert into Users(Email, Name)
    values (@email, @name);

    select last_insert_rowid() as inserted;
""">

type InsertCommentSQL = SQL<"""
    insert into Comments(AuthorId, Comment)
    values (@authorId, @comment);
""">

type QueryCommentsSQL = SQL<"select * from Comments">

[<EntryPoint>]
let main argv =
    if File.Exists("rzsql.db") then
        File.Delete("rzsql.db") // clean up DB from last run
    Model.Migrate(Migrations.MigrationConfig.Default)

    use context = new ConnectionContext()
    let userId = InsertSQL.Command(email = "humbobst@gmail.com", name = "Robert").ExecuteScalar(context)
    for comment in [ "Isn't"; "this"; "neat" ] do
        InsertCommentSQL.Command(authorId = userId, comment = comment).Execute(context)

    let comments = QueryCommentsSQL.Command().Execute(context)
    for comment in comments do
        Console.WriteLine(comment.Id.ToString() + " " + comment.Comment)

    0 // exit code
