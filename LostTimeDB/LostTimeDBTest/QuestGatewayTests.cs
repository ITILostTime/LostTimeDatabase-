using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public class QuestGatewayTests
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";

        [Test]
        public void Create_FindByQuestID_Delete_GetAll_Quest()
        {
            LostTimeDB.QuestGateway gtw = new LostTimeDB.QuestGateway(_connectionstring);
            LostTimeDB.Quest quest;
            IEnumerable<LostTimeDB.Quest> IEQuest;

            int i = 0;

            for (i = 0; i < 5; i++)
            {
                gtw.CreateQuest("TitleTest" + i, "{QuestTitle" + i + ":Title" + i + ", QuestID" + i + ":" + i + ", QuestActif" + i + ": true}", i);
            }

            IEQuest = gtw.GetAll();

            i = 0;

            foreach (LostTimeDB.Quest n in IEQuest)
            {
                Assert.That(n.QuestTitle, Is.EqualTo("TitleTest" + i));
                Assert.That(n.QuestData, Is.EqualTo("{QuestTitle" + i + ":Title" + i + ", QuestID" + i + ":" + i + ", QuestActif" + i + ": true}"));
                i++;
            }

            i = 0;

            foreach (LostTimeDB.Quest n in IEQuest)
            {
                quest = gtw.FindQuestBYQuestID(n.QuestID);

                Assert.That(quest.QuestTitle, Is.EqualTo("TitleTest" + i));

                gtw.DeleteQuestByQuestID(n.QuestID);
                quest = gtw.FindQuestBYQuestID(quest.QuestID);
                Assert.That(quest, Is.Null);

                i++;
            }

        }

        [Test]
        public void Update_Quest()
        {
            LostTimeDB.QuestGateway gtw = new LostTimeDB.QuestGateway(_connectionstring);
            LostTimeDB.Quest quest = new LostTimeDB.Quest();

            gtw.CreateQuest("questTitle", "{QuestTitle:Title, QuestID: 1 , QuestActif : true}", 1);

            IEnumerable<LostTimeDB.Quest> Qst = gtw.GetAll();

            foreach(LostTimeDB.Quest n in Qst)
            {
                quest = gtw.FindQuestBYQuestID(n.QuestID);
            }

            Assert.That(quest.QuestTitle, Is.EqualTo("questTitle"));

            gtw.DeleteQuestByQuestID(quest.QuestID);
            quest = gtw.FindQuestBYQuestID(quest.QuestID);
            Assert.That(quest, Is.Null);
        }

        [Test]
        public void FindQuestByQuestTitleTest()
        {
            LostTimeDB.QuestGateway gtw = new LostTimeDB.QuestGateway(_connectionstring);
            LostTimeDB.Quest quest = new LostTimeDB.Quest();

            gtw.CreateQuest("title", "test", 1);
            quest = gtw.FindQuestBYQuestTitle("title");

            Assert.That(quest.QuestData, Is.EqualTo("test"));
            Assert.That(quest.AuthorID, Is.EqualTo(1));

            gtw.DeleteQuestByQuestID(quest.QuestID);
            quest = gtw.FindQuestBYQuestTitle("title");
            Assert.That(quest, Is.Null);

        }

        [Test]
        public void GetALLBYAuthorIDTest()
        {
            LostTimeDB.QuestGateway gtw = new LostTimeDB.QuestGateway(_connectionstring);
            LostTimeDB.Quest quest = new LostTimeDB.Quest();

            for(int x = 0; x < 5; x++)
            {
                gtw.CreateQuest("title" + x, "QuestData" + x, x);
                gtw.CreateQuest("title" + x, "QuestData" + x, 5);
            }
            IEnumerable<LostTimeDB.Quest> qt = gtw.GetAll();
            IEnumerable<LostTimeDB.Quest> Qst = gtw.GetAllByAuthorID(5);

            foreach(LostTimeDB.Quest n in Qst)
            {
                Assert.That(n.AuthorID, Is.EqualTo(5));
            }

            foreach(LostTimeDB.Quest n in qt)
            {
                gtw.DeleteQuestByQuestID(n.QuestID);
            }
        }
    }
}
