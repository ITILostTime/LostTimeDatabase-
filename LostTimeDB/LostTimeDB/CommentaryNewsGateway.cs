using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LostTimeDB
{
    public class CommentaryNewsGateway
    {
        readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentaryNewsGateway"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public CommentaryNewsGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates the news comment.
        /// </summary>
        /// <param name="newsCommentID">The news comment identifier.</param>
        /// <param name="newsCommentDate">The news comment date.</param>
        /// <param name="newsID">The news identifier.</param>
        /// <param name="newsCommentContent">Content of the news comment.</param>
        /// <param name="authorID">The author identifier.</param>
        public void CreateNewsComment(int commentaryNewsID, DateTime commentaryNewsDate, int newsID, string commentaryNewsContent, int commentaryNewsAuthorID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "CreateCommentaryNews", 
                    new
                    {
                        CommentaryNewsID = commentaryNewsID,
                        CommentaryNewsDate = commentaryNewsDate,
                        NewsID = newsID,
                        CommentaryNewsContent = commentaryNewsContent,
                        CommentaryNewsAuthorID = commentaryNewsAuthorID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Updates the news comment.
        /// </summary>
        /// <param name="newsCommentID">The news comment identifier.</param>
        /// <param name="newsCommentDate">The news comment date.</param>
        /// <param name="newsID">The news identifier.</param>
        /// <param name="newsCommentContent">Content of the news comment.</param>
        /// <param name="authorID">The author identifier.</param>
        public void UpdateNewsComment(int commentaryNewsID, DateTime commentaryNewsDate, int newsID, string commentaryNewsContent, int commentaryNewsAuthorID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "UpdateCommentaryNews",
                    new
                    {
                        CommentaryNewsID = commentaryNewsID,
                        CommentaryNewsDate = commentaryNewsDate,
                        NewsID = newsID,
                        CommentaryNewsContent = commentaryNewsContent,
                        CommentaryNewsAuthorID = commentaryNewsAuthorID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Deletes the news comment.
        /// </summary>
        /// <param name="newsCommentID">The news comment identifier.</param>
        public void DeleteNewsComment(int commentaryNewsID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "DeleteCommentaryNews",
                    new
                    {
                        CommentaryNewsID = commentaryNewsID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Finds the by news.
        /// </summary>
        /// <param name="newsCommentID">The news comment identifier.</param>
        /// <returns></returns>
        public CommentaryNews FindByNews(int commentaryNewsID) //a voir si on vise le newsID ou le newsCommentID
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CommentaryNews>(
                    @"select nc.CommentaryNewsDate, nc.CommentaryNewsContent
                    from ViewCommentaryNews nc
                    where nc.CommentaryNewsID = @CommentaryNewsID;",
                    new { CommentaryNewsID = commentaryNewsID })
                    .FirstOrDefault;
            }
        }

        /// <summary>
        /// Finds the by date.
        /// </summary>
        /// <param name="newsCommentDate">The news comment date.</param>
        /// <returns></returns>
        public CommentaryNews FindByDate(DateTime commentaryNewsDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CommentaryNews>(
                    @"select nc.CommentaryNewsID, nc.CommentaryNewsContent
                    from ViewCommentaryNews nc
                    where nc.CommentaryNewsDate = @CommentaryNewsDate;",
                    new { CommentaryNewsDate = commentaryNewsDate })
                    .FirstOrDefault;
            }
        }

        public CommentaryNews CreateByAuthorID(int commentaryNewsAuthorID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CommentaryNews>(
                    @"select nc.CommentaryNewsID, nc.CommentaryNewsDate, nc.NewsID, nc.CommentaryNewsContent
                    from ViewCommentaryNews nc
                    where nc.CommentaryNewsAuthorID = @CommentaryNewsAuthorID;",
                    new { CommentaryNewsAuthorID = commentaryNewsAuthorID })
                    .FirstOrDefault;
            }
        }
    }
}
