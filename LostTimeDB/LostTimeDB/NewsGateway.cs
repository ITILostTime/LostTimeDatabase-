using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace LostTimeDB
{
    public class NewsGateway
    {
        readonly string _connectionString;

        public NewsGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateNews(string newsTitle, string newsContent, DateTime date, int newsAuthorID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNews",
                    new
                    {
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        NewsCreationDate = date,
                        NewsLastUpdate = date,
                        NewsGoodVote = 0,
                        NewsBadVote = 0,
                        NewsEditionNb = 1,
                        NewsAuthorID = newsAuthorID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteNews(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteNewsByNewsID",
                    new
                    {
                        NewsID = newsID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public News FindByID(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<News>(
                    @"select    s.NewsID,
                                s.NewsTitle,
                                s.NewsContent,
                                s.NewsCreationDate,
                                s.NewsLastUpdate,
                                s.NewsGoodVote,
                                s.NewsBadVote,
                                s.NewsEditionNb,
                                s.NewsAuthorID
                    from ViewNewsTableByNewsID s
                    where s.NewsID = @NewsID ;",
                    new { NewsID = newsID })
                    .FirstOrDefault();
            }
        }

        public News FindByTitle(string newsTitle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<News>(
                    @"select    s.NewsID,
                                s.NewsTitle,
                                s.NewsContent,
                                s.NewsCreationDate,
                                s.NewsLastUpdate,
                                s.NewsGoodVote,
                                s.NewsBadVote,
                                s.NewsEditionNb,
                                s.NewsAuthorID
                    from ViewNewsTableByNewsTitle s
                    where s.NewsTitle = @NewsTitle ;",
                    new { NewsTitle = newsTitle })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<News> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select s.NewsID,
                             s.NewsTitle,
                             s.NewsContent,
                             s.NewsCreationDate,
                             s.NewsLastUpdate,
                             s.NewsGoodVote,
                             s.NewsBadVote,
                             s.NewsEditionNb,
                             s.NewsAuthorID
                      from GetAllNews s;");
            }
        }
        public IEnumerable<News> GetAllByAuthorID(int newsAuthorID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<News>(
                    @"select    s.NewsID,
                                s.NewsTitle,
                                s.NewsContent,
                                s.NewsCreationDate,
                                s.NewsLastUpdate,
                                s.NewsGoodVote,
                                s.NewsBadVote,
                                s.NewsEditionNb,
                                s.NewsAuthorID
                    from GetAllByAuthorID s
                    where s.NewsAuthorID = @NewsAuthorID ;",
                    new { NewsAuthorID = newsAuthorID });
            }
        }

        public IEnumerable<News> GetAllByRating()
        {
            IEnumerable<News> Item = GetAll();

            List<News> NewsArticles = Item.ToList();
            int Count = NewsArticles.Count();
            List<News> NewsArticlesByRating = new List<News>();
            News tempNews = new News();
            bool findBestRating = false;
            bool firstTurn = false;

            while(Count != 0)
            {
                foreach (News n in NewsArticles)
                {
                    if (firstTurn == false)
                    {
                        tempNews = n;
                        firstTurn = true;
                    }
                    else
                    {
                        if ((n.NewsGoodVote - n.NewsBadVote) > (tempNews.NewsGoodVote - tempNews.NewsBadVote) && Count > 0)
                        {
                            tempNews = n;
                        }
                        else if ((n.NewsGoodVote - n.NewsBadVote) > (tempNews.NewsGoodVote - tempNews.NewsBadVote) && Count == 0)
                        {
                            NewsArticlesByRating.Add(n);
                            NewsArticles.Remove(n);
                            findBestRating = true;
                        }
                    }
                    if (findBestRating == true)
                    {
                        Count = NewsArticles.Count();
                        findBestRating = false;
                        firstTurn = false;
                    }
                    else
                    {
                        Count--;
                    }
                }
            }

            Item = NewsArticlesByRating.AsEnumerable<News>();
            return Item;
        }

        public IEnumerable<News> GetAllAfterNewsID(int newsAuthorID)
        {
            IEnumerable<News> Item = GetAll();

            List<News> NewsArticles = Item.ToList();
            List<News> NewsArticlesAfterCurrentNewsID = new List<News>();

            foreach(News n in NewsArticles)
            {
                if(n.NewsID > newsAuthorID)
                {
                    NewsArticlesAfterCurrentNewsID.Add(n);
                }
            }

            Item = NewsArticlesAfterCurrentNewsID.AsEnumerable<News>();
            return Item;
        }

        public IEnumerable<News> GetAllBeforeNewsID(int newsAuthorID)
        {
            IEnumerable<News> Item = GetAll();

            List<News> NewsArticles = Item.ToList();
            List<News> NewsArticlesAfterCurrentNewsID = new List<News>();

            foreach (News n in NewsArticles)
            {
                if (n.NewsID < newsAuthorID)
                {
                    NewsArticlesAfterCurrentNewsID.Add(n);
                }
            }

            Item = NewsArticlesAfterCurrentNewsID.AsEnumerable<News>();
            return Item;
        }

        public IEnumerable<News> GetArticleByDateInterval(DateTime AfterFirstdate, DateTime BeforeSecondDate)
        {
            IEnumerable<News> Item = GetAll();

            List<News> NewsArticles = Item.ToList();
            List<News> NewsArticlesAfterCurrentNewsID = new List<News>();

            foreach (News n in NewsArticles)
            {
                if (n.NewsLastUpdate >= AfterFirstdate && n.NewsLastUpdate <= BeforeSecondDate)
                {
                    NewsArticlesAfterCurrentNewsID.Add(n);
                }
            }

            Item = NewsArticlesAfterCurrentNewsID.AsEnumerable<News>();
            return Item;
        }

        public void UpdateNewsGoodPopularity(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "GoodVoteForNews",
                    new
                    {
                        NewsID = newsID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateNewsBadPopularity(int newsID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "BadVoteForNews",
                    new
                    {
                        NewsID = newsID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateArticle(int newsID, string newsTitle, string newsContent, DateTime date, int newsAuthorID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateNewsDetails",
                    new
                    {
                        NewsID = newsID,
                        NewsTitle = newsTitle,
                        NewsContent = newsContent,
                        NewsLastUpdate = date,
                        NewsAuthorID = newsAuthorID

                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
