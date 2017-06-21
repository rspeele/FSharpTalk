using System;
using System.Data.Common;
using Dapper;

namespace CSharpCode
{
    public static class DapperSqlExample
    {
        public class CommentRow
        {
            public long Id { get; set; }
            public long AuthorId { get; set; }
            public string Comment { get; set; }
        }

        public static void Example(DbConnection connection)
        {
            var userId = connection.ExecuteScalar<long>(@"
                insert into Users(Email, Name)
                values (@email, @name);

                select last_insert_rowid() as inserted;
                ", new { name = "Robert", emali = "humbobst@gmail.com" });
            
            foreach (var comment in new[] { "Kinda", "Error", "Prone" })
            {
                connection.Execute(@"
                    insert into Comments(AuthorId, Comment)
                    values (@authorId, @comment);
                ", new { authorld = userId, comment = comment });
            }

            foreach (var comment in connection.Query<CommentRow>("select * from Comment"))
            {
                Console.WriteLine(comment.Id + " " + comment.Comment);
            }
        }
    }
}
