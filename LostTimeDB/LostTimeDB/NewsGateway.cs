using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostTimeDB
{
    public class NewsGateway
    {
        readonly string _connectionString;

        public NewsGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public News GetByID(int newsID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select n.Date, n.Title, n.Content
                    from ViewNews n
                    where n.NewsID = @NewsID;",
                    new {NewsID = newsID})
                    .FirstOrDefault;
            }
        }

        public void CreateNews(int newsID, DateTime date, string title, string content, int authorID, int upVote, int downVote)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNews",
                    new
                    {
                        NewsID = newsID,
                        Date = date,
                        Title = title,
                        Content = content,
                        AuthorID = authorID,
                        UpVote = upVote,
                        DownVote = downVote
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateNews(DateTime date, string title, string content)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateNews",
                    new
                    {
                        Date = date,
                        Title = title,
                        Content = content
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteNews(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteNews",
                    new
                    {
                        NewsID = newsID,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public News GetByDates(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select n.Title, n.Content
                    from ViewsNews n 
                    where n.Date = @Date;",
                    new { Date = date })
                    .FirstOrDefault;
            }
        }

        public News GetByPopularity()
        {

        }
    }
}
