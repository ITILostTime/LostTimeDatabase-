/* Migration Script */

CREATE PROCEDURE BadVoteForNews
(
	@NewsID INT
)
AS
BEGIN
	UPDATE News
	SET
		NewsBadVote = NewsBadVote - 1
	WHERE NewsID = @NewsID
END