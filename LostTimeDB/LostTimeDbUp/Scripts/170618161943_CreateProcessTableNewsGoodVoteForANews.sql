/* Migration Script */

CREATE PROCEDURE GoodVoteForNews
(
	@NewsID INT
)
AS
BEGIN
	UPDATE News
	SET
		NewsGoodVote = NewsGoodVote + 1
	WHERE NewsID = @NewsID
END