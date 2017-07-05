/* Migration Script */

CREATE VIEW FindQuestByQuestTitle
AS
	SELECT
		QuestID = s.QuestID,
		QuestTitle = s.QuestTitle,
		QuestData = s.QuestData,
		QuestLastEdit = s.QuestLastEdit,
		AuthorID = s.AuthorID
	FROM Quest s;