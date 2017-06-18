using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostTimeDBTest
{
    [TestFixture]
    public class NewsGatewayTest
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";

        [Test]
        public void Create_Find_Delete_NewsTest()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news;
            LostTimeDB.News news2;

            NewsGtw.CreateNews("TitleTest", "ContentTest", DateTime.Now, 0125631);
            news = NewsGtw.FindByTitle("TitleTest");
            Assert.That(news.NewsTitle, Is.EqualTo("TitleTest"));
            Assert.That(news.NewsContent, Is.EqualTo("ContentTest"));
            Assert.That(news.NewsAuthorID, Is.EqualTo(0125631));

            news2 = NewsGtw.FindByID(news.NewsID);
            Assert.That(news2.NewsID, Is.EqualTo(news.NewsID));
            Assert.That(news2.NewsTitle, Is.EqualTo("TitleTest"));
            Assert.That(news2.NewsContent, Is.EqualTo("ContentTest"));
            Assert.That(news.NewsAuthorID, Is.EqualTo(0125631));

            NewsGtw.DeleteNews(news2.NewsID);
            news2 = NewsGtw.FindByID(news2.NewsID);
            news = NewsGtw.FindByTitle("TitleTest");
            Assert.That(news2, Is.Null);
            Assert.That(news, Is.Null);
        }

        [Test]
        public void GetAllNewsTest()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news2;

            NewsGtw.CreateNews("NewsTitleA", "NewsContentA", DateTime.Now, 01);
            NewsGtw.CreateNews("NewsTitleB", "NewsContentB", DateTime.Now, 02);
            NewsGtw.CreateNews("NewsTitleC", "NewsContentC", DateTime.Now, 03);
            NewsGtw.CreateNews("NewsTitleD", "NewsContentD", DateTime.Now, 04);

            IEnumerable<LostTimeDB.News> news = NewsGtw.GetAll();

            foreach(LostTimeDB.News NewsItem in news)
            {
                if(NewsItem.NewsAuthorID == 01)
                {
                    Assert.That(NewsItem.NewsTitle, Is.EqualTo("NewsTitleA"));
                    Assert.That(NewsItem.NewsContent, Is.EqualTo("NewsContentA"));
                    Assert.That(NewsItem.NewsEditionNb, Is.EqualTo(1));
                    Assert.That(NewsItem.NewsGoodVote, Is.EqualTo(0));
                    Assert.That(NewsItem.NewsBadVote, Is.EqualTo(0));
                }
                if(NewsItem.NewsAuthorID == 02)
                {
                    Assert.That(NewsItem.NewsTitle, Is.EqualTo("NewsTitleB"));
                    Assert.That(NewsItem.NewsContent, Is.EqualTo("NewsContentB"));
                    Assert.That(NewsItem.NewsEditionNb, Is.EqualTo(1));
                    Assert.That(NewsItem.NewsGoodVote, Is.EqualTo(0));
                    Assert.That(NewsItem.NewsBadVote, Is.EqualTo(0));
                }
                if (NewsItem.NewsAuthorID == 03)
                {
                    Assert.That(NewsItem.NewsTitle, Is.EqualTo("NewsTitleC"));
                    Assert.That(NewsItem.NewsContent, Is.EqualTo("NewsContentC"));
                    Assert.That(NewsItem.NewsEditionNb, Is.EqualTo(1));
                    Assert.That(NewsItem.NewsGoodVote, Is.EqualTo(0));
                    Assert.That(NewsItem.NewsBadVote, Is.EqualTo(0));
                }
                if (NewsItem.NewsAuthorID == 04)
                {
                    Assert.That(NewsItem.NewsTitle, Is.EqualTo("NewsTitleD"));
                    Assert.That(NewsItem.NewsContent, Is.EqualTo("NewsContentD"));
                    Assert.That(NewsItem.NewsEditionNb, Is.EqualTo(1));
                    Assert.That(NewsItem.NewsGoodVote, Is.EqualTo(0));
                    Assert.That(NewsItem.NewsBadVote, Is.EqualTo(0));
                }
                NewsGtw.DeleteNews(NewsItem.NewsID);
                news2 = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news2, Is.Null);
            }

        }

        [Test]
        public void UpdateNewsPopularityTest()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news;

            NewsGtw.CreateNews("Title", "Content", DateTime.Now, 01);
            news = NewsGtw.FindByTitle("Title");
            Assert.That(news.NewsGoodVote, Is.EqualTo(0));
            Assert.That(news.NewsBadVote, Is.EqualTo(0));

            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            news = NewsGtw.FindByTitle("Title");
            Assert.That(news.NewsGoodVote, Is.EqualTo(1));

            NewsGtw.UpdateNewsBadPopularity(news.NewsID);
            news = NewsGtw.FindByTitle("Title");
            Assert.That(news.NewsBadVote, Is.EqualTo(-1));

            NewsGtw.DeleteNews(news.NewsID);
            news = NewsGtw.FindByTitle("Title");
            Assert.That(news, Is.Null);

        }

        [Test]
        public void Create_UpdateNews()
        {
            LostTimeDB.NewsGateway newsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news;

            newsGtw.CreateNews("Title", "Content", DateTime.Now, 01);
            news = newsGtw.FindByTitle("Title");
            Assert.That(news.NewsTitle, Is.EqualTo("Title"));
            Assert.That(news.NewsAuthorID, Is.EqualTo(01));

            newsGtw.UpdateArticle(news.NewsID, "updateTitle", "updateContent", DateTime.Now, 02);
            news = newsGtw.FindByTitle("updateTitle");
            Assert.That(news.NewsTitle, Is.EqualTo("updateTitle"));
            Assert.That(news.NewsAuthorID, Is.EqualTo(02));

            newsGtw.DeleteNews(news.NewsID);
            news = newsGtw.FindByTitle("updateTitle");
            Assert.That(news, Is.Null);

        }

        [Test]
        public void GetAllByAuthorIDTest()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news2;

            NewsGtw.CreateNews("TitleA", "ContentA", DateTime.Now, 10);
            NewsGtw.CreateNews("TitleB", "ContentB", DateTime.Now, 11);
            NewsGtw.CreateNews("TitleC", "ContentC", DateTime.Now, 10);
            NewsGtw.CreateNews("TitleD", "ContentD", DateTime.Now, 10);
            NewsGtw.CreateNews("TitleE", "ContentE", DateTime.Now, 13);
            NewsGtw.CreateNews("TitleF", "ContentF", DateTime.Now, 14);
            NewsGtw.CreateNews("TitleG", "ContentG", DateTime.Now, 14);
            NewsGtw.CreateNews("TitleH", "ContentH", DateTime.Now, 15);

            IEnumerable<LostTimeDB.News> news = NewsGtw.GetAllByAuthorID(10);

            foreach (LostTimeDB.News NewsItem in news)
            {
                Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(10));
                NewsGtw.DeleteNews(NewsItem.NewsID);
                news2 = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news2, Is.Null);
            }
        }

        [Test]
        public void GetAllByRatingTest()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news;

            //  =>  -3
            NewsGtw.CreateNews("TitleA", "ContentA", DateTime.Now, 20);
            news = NewsGtw.FindByTitle("TitleA");
            Assert.That(news.NewsContent, Is.EqualTo("ContentA"));
            NewsGtw.UpdateNewsBadPopularity(news.NewsID);
            NewsGtw.UpdateNewsBadPopularity(news.NewsID);
            NewsGtw.UpdateNewsBadPopularity(news.NewsID);

            // => 2
            NewsGtw.CreateNews("TitleB", "ContentB", DateTime.Now, 21);
            news = NewsGtw.FindByTitle("TitleB");
            Assert.That(news.NewsContent, Is.EqualTo("ContentB"));
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsBadPopularity(news.NewsID);

            // =>   7
            NewsGtw.CreateNews("TitleC", "ContentC", DateTime.Now, 22);
            news = NewsGtw.FindByTitle("TitleC");
            Assert.That(news.NewsContent, Is.EqualTo("ContentC"));
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);
            NewsGtw.UpdateNewsGoodPopularity(news.NewsID);

            //  => 0
            NewsGtw.CreateNews("TitleD", "ContentD", DateTime.Now, 23);
            news = NewsGtw.FindByTitle("TitleD");
            Assert.That(news.NewsContent, Is.EqualTo("ContentD"));

            IEnumerable<LostTimeDB.News> IEnews = NewsGtw.GetAllByRating();
            int count = 0;

            foreach (LostTimeDB.News NewsItem in IEnews)
            {
                if(count == 0)
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(22));
                    count++;
                }
                if (count == 1)
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(21));
                    count++;
                }
                if (count == 2)
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(23));
                    count++;
                }
                if (count == 3)
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(20));
                    count++;
                }

                NewsGtw.DeleteNews(NewsItem.NewsID);
                news = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news, Is.Null);
            }
        }

        [Test]
        public void GetNewsBeforeCurrentNewsID()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news0;
            LostTimeDB.News news1;
            LostTimeDB.News news2;
            LostTimeDB.News news3;
            LostTimeDB.News news4;
            LostTimeDB.News news5;
            LostTimeDB.News news;


            NewsGtw.CreateNews("TitleA", "ContentA", DateTime.Now, 50);
            news0 = NewsGtw.FindByTitle("TitleA");
            Assert.That(news0.NewsContent, Is.EqualTo("ContentA"));

            NewsGtw.CreateNews("TitleB", "ContentB", DateTime.Now, 51);
            news1 = NewsGtw.FindByTitle("TitleB");
            Assert.That(news1.NewsContent, Is.EqualTo("ContentB"));

            NewsGtw.CreateNews("TitleC", "ContentC", DateTime.Now, 52);
            news2 = NewsGtw.FindByTitle("TitleC");
            Assert.That(news2.NewsContent, Is.EqualTo("ContentC"));

            NewsGtw.CreateNews("TitleD", "ContentD", DateTime.Now, 53);
            news3 = NewsGtw.FindByTitle("TitleD");
            Assert.That(news3.NewsContent, Is.EqualTo("ContentD"));

            NewsGtw.CreateNews("TitleE", "ContentE", DateTime.Now, 54);
            news4 = NewsGtw.FindByTitle("TitleE");
            Assert.That(news4.NewsContent, Is.EqualTo("ContentE"));

            NewsGtw.CreateNews("TitleF", "ContentF", DateTime.Now, 55);
            news5 = NewsGtw.FindByTitle("TitleF");
            Assert.That(news5.NewsContent, Is.EqualTo("ContentF"));

            IEnumerable<LostTimeDB.News> IEnews = NewsGtw.GetAllBeforeNewsID(news3.NewsID);

            foreach (LostTimeDB.News NewsItem in IEnews)
            {
                if (NewsItem.NewsTitle == "TitleA")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(50));
                }
                if (NewsItem.NewsTitle == "TitleB")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(51));
                }
                if (NewsItem.NewsTitle == "TitleC")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(52));
                }

                NewsGtw.DeleteNews(NewsItem.NewsID);
                news = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news, Is.Null);
            }
        }

        [Test]
        public void GetNewsAfterCurrentNewsID()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news0;
            LostTimeDB.News news1;
            LostTimeDB.News news2;
            LostTimeDB.News news3;
            LostTimeDB.News news4;
            LostTimeDB.News news5;
            LostTimeDB.News news;


            NewsGtw.CreateNews("TitleA", "ContentA", DateTime.Now, 50);
            news0 = NewsGtw.FindByTitle("TitleA");
            Assert.That(news0.NewsContent, Is.EqualTo("ContentA"));

            NewsGtw.CreateNews("TitleB", "ContentB", DateTime.Now, 51);
            news1 = NewsGtw.FindByTitle("TitleB");
            Assert.That(news1.NewsContent, Is.EqualTo("ContentB"));

            NewsGtw.CreateNews("TitleC", "ContentC", DateTime.Now, 52);
            news2 = NewsGtw.FindByTitle("TitleC");
            Assert.That(news2.NewsContent, Is.EqualTo("ContentC"));

            NewsGtw.CreateNews("TitleD", "ContentD", DateTime.Now, 53);
            news3 = NewsGtw.FindByTitle("TitleD");
            Assert.That(news3.NewsContent, Is.EqualTo("ContentD"));

            NewsGtw.CreateNews("TitleE", "ContentE", DateTime.Now, 54);
            news4 = NewsGtw.FindByTitle("TitleE");
            Assert.That(news4.NewsContent, Is.EqualTo("ContentE"));

            NewsGtw.CreateNews("TitleF", "ContentF", DateTime.Now, 55);
            news5 = NewsGtw.FindByTitle("TitleF");
            Assert.That(news5.NewsContent, Is.EqualTo("ContentF"));

            IEnumerable<LostTimeDB.News> IEnews = NewsGtw.GetAllAfterNewsID(news3.NewsID);

            foreach (LostTimeDB.News NewsItem in IEnews)
            {
                if (NewsItem.NewsTitle == "TitleE")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(54));
                }
                if (NewsItem.NewsTitle == "TitleF")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(55));
                }

                NewsGtw.DeleteNews(NewsItem.NewsID);
                news = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news, Is.Null);
            }
        }

        [Test]
        public void GetNewsBetweenTwoDates()
        {
            LostTimeDB.NewsGateway NewsGtw = new LostTimeDB.NewsGateway(_connectionstring);
            LostTimeDB.News news0;
            LostTimeDB.News news1;
            LostTimeDB.News news2;
            LostTimeDB.News news3;
            LostTimeDB.News news4;
            LostTimeDB.News news5;
            LostTimeDB.News news;


            NewsGtw.CreateNews("TitleA", "ContentA", DateTime.Now, 50);
            news0 = NewsGtw.FindByTitle("TitleA");
            Assert.That(news0.NewsContent, Is.EqualTo("ContentA"));

            NewsGtw.CreateNews("TitleB", "ContentB", DateTime.Now, 51);
            news1 = NewsGtw.FindByTitle("TitleB");
            Assert.That(news1.NewsContent, Is.EqualTo("ContentB"));

            NewsGtw.CreateNews("TitleC", "ContentC", DateTime.Now, 52);
            news2 = NewsGtw.FindByTitle("TitleC");
            Assert.That(news2.NewsContent, Is.EqualTo("ContentC"));

            NewsGtw.CreateNews("TitleD", "ContentD", DateTime.Now, 53);
            news3 = NewsGtw.FindByTitle("TitleD");
            Assert.That(news3.NewsContent, Is.EqualTo("ContentD"));

            NewsGtw.CreateNews("TitleE", "ContentE", DateTime.Now, 54);
            news4 = NewsGtw.FindByTitle("TitleE");
            Assert.That(news4.NewsContent, Is.EqualTo("ContentE"));

            NewsGtw.CreateNews("TitleF", "ContentF", DateTime.Now, 55);
            news5 = NewsGtw.FindByTitle("TitleF");
            Assert.That(news5.NewsContent, Is.EqualTo("ContentF"));

            IEnumerable<LostTimeDB.News> IEnews = NewsGtw.GetArticleByDateInterval(news4.NewsLastUpdate, news1.NewsLastUpdate);

            foreach (LostTimeDB.News NewsItem in IEnews)
            {
                if (NewsItem.NewsTitle == "TitleB")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(51));
                }
                if (NewsItem.NewsTitle == "TitleC")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(52));
                }
                if (NewsItem.NewsTitle == "TitleD")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(53));
                }
                if (NewsItem.NewsTitle == "TitleE")
                {
                    Assert.That(NewsItem.NewsAuthorID, Is.EqualTo(54));
                }

                NewsGtw.DeleteNews(NewsItem.NewsID);
                news = NewsGtw.FindByID(NewsItem.NewsID);
                Assert.That(news, Is.Null);
            }
        }
    }
}
