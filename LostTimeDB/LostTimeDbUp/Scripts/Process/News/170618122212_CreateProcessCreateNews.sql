/* Migration Script */

CREATE PROCEDURE CreateNews
(
	@NewsTitle nvarchar(64),
	@NewsContent nvarchar(max),
	@NewsCreationDate Datetime2,
	@NewsLastUpdate Datetime2,
	@NewsGoodVote int,
	@NewsBadVote int,
	@NewsEditionNb int,
	@NewsAuthorID int

)
AS
BEGIN
	INSERT INTO News(NewsTitle, NewsContent, NewsCreationDate, NewsLastUpdate, NewsGoodVote, NewsBadVote, NewsEditionNb, NewsAuthorID)
	VALUES(@NewsTitle, @NewsContent, @NewsCreationDate, @NewsLastUpdate, @NewsGoodVote, @NewsBadVote, @NewsEditionNb, @NewsAuthorID)
END