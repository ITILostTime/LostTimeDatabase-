/* Migration Script */

CREATE PROCEDURE UpdateNewsDetails
(
	@NewsID INT,
	@NewsTitle NVARCHAR(64),
	@NewsContent nvarchar(max),
	@NewsLastUpdate Datetime2,
	@NewsAuthorID int
)
AS
BEGIN
	UPDATE News
	SET
		NewsTitle = @NewsTitle,
		NewsContent = @NewsContent,
		NewsLastUpdate = @NewsLastUpdate,
		NewsEditionNb = NewsEditionNb + 1,
		NewsAuthorID = @NewsAuthorID
	WHERE NewsID = @NewsID
END