/* Migration Script */

create view GetAllQuest
AS
	SELECT
		QuestID = s.QuestID,
		QuestTitle = s.QuestTitle,
		QuestData = s.QuestData,
		QuestLastEdit = s.QuestLastEdit,
		AuthorID = s.AuthorID
	FROM Quest s
	where s.QuestID <> 0;

