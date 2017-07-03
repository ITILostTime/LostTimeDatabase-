/* Migration Script */

CREATE PROCEDURE DeleteQuestByQuestID
(
	@QuestID int
)
AS
BEGIN
	DELETE FROM Quest WHERE QuestID = @QuestID;
END