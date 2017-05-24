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

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsGateway"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public NewsGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="newsID">The news identifier.</param>
        /// <returns></returns>
        public News FindByID(int newsID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select n.NewsDate, n.NewsTitle, n.NewsContent
                    from ViewNews n
                    where n.NewsID = @NewsID;",
                    new {NewsID = newsID})
                    .FirstOrDefault;
            }
        }

        /// <summary>
        /// Creates the news.
        /// </summary>
        /// <param name="newsID">The news identifier.</param>
        /// <param name="newsDate">The news date.</param>
        /// <param name="newsTitle">The news title.</param>
        /// <param name="newsContent">Content of the news.</param>
        /// <param name="authorID">The author identifier.</param>
        /// <param name="upVote">Up vote.</param>
        /// <param name="downVote">Down vote.</param>
        public void CreateNews(int newsID, DateTime newsDate, string newsTitle, string newsContent, int authorID, int upVote, int downVote)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNews",
                    new
                    {
                        NewsID = newsID,
                        NewsDate = newsDate,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        AuthorID = authorID,
                        UpVote = upVote,
                        DownVote = downVote
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Updates the news.
        /// </summary>
        /// <param name="newsDate">The news date.</param>
        /// <param name="newsTitle">The news title.</param>
        /// <param name="newsContent">Content of the news.</param>
        public void UpdateNews(DateTime newsDate, string newsTitle, string newsContent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateNews",
                    new
                    {
                        NewsDate = newsDate,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Deletes the news.
        /// </summary>
        /// <param name="newsID">The news identifier.</param>
        public void DeleteNews(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteNews",
                    new
                    {
                        NewsID = newsID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Finds the by dates.
        /// </summary>
        /// <param name="newsDate">The news date.</param>
        /// <returns></returns>
        public News FindByDates(DateTime newsDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select n.NewsTitle, n.NewsContent
                    from ViewsNews n 
                    where n.NewsDate = @NewsDate;",
                    new { NewsDate = newsDate })
                    .FirstOrDefault;
            }
        }

        /*public News GetByPopularity()
        {

        }*/

        //FindByAuthorID
    }
}
