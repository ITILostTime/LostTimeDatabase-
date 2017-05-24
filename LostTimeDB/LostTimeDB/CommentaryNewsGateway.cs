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
                    "CreateNewsComment", 
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
                    "UpdateNewsComment",
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
                    "DeleteNewsComment",
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
                    @"select nc.NewsCommentDate, nc.NewsCommentContent
                    from ViewNewsComment nc
                    where nc.NewsCommentID = @NewsCommentID;",
                    new { CommentaryNewsID = commentaryNewsID })
                    .FirstOrDefault;
            }
        }

        /// <summary>
        /// Finds the by date.
        /// </summary>
        /// <param name="newsCommentDate">The news comment date.</param>
        /// <returns></returns>
        public CommentaryNews FindByDate(DateTime newsCommentDate)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<CommentaryNews>(
                    @"select nc.NewsCommentID, nc.NewsCommentContent
                    from ViewNewsComment nc
                    where nc.NewsCommentDate = @NewsCommentDate;",
                    new { NewsCommentDate = newsCommentDate })
                    .FirstOrDefault;
            }
        }

        //CreateByAuthorID
    }
}
