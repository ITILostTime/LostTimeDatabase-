/* Migration Script */

CREATE VIEW FindQuestByQuestID
AS
	SELECT
		QuestID = s.QuestID,
		QuestTitle = s.QuestTitle,
		QuestData = s.QuestData,
		QuestLastEdit = s.QuestLastEdit,
		AuthorID = s.AuthorID
	FROM Quest s;