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
    }
}
