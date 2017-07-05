/* Migration Script */

CREATE PROCEDURE CreateNewQuest
(
	@QuestTitle NVARCHAR(64),
	@QuestData nvarchar(max),
	@QuestLastEdit datetime2,
	@AuthorID int
)
AS
BEGIN
	INSERT INTO Quest(QuestTitle, QuestData, QuestLastEdit, AuthorID)
	VALUES(@QuestTitle, @QuestData, @QuestLastEdit, @AuthorID)
END