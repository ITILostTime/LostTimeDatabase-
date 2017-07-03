/* Migration Script */

CREATE PROCEDURE UpdateQuest
(
	@QuestID INT,
	@QuestTitle NVARCHAR(64),
	@QuestData NVARCHAR(MAX),
	@QuestLastEdit datetime2,
	@AuthorID int
)
AS
BEGIN
	UPDATE Quest
	SET
		QuestTitle = @QuestTitle,
		QuestData = @QuestData,
		QuestLastEdit = @QuestLastEdit,
		AuthorID = @AuthorID
	WHERE QuestID = @QuestID
END