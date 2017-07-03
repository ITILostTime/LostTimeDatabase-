/* Migration Script */

create view GetAllNews
AS
	SELECT
		NewsID = s.NewsID,
		NewsTitle = s.NewsTitle,
		NewsContent = s.NewsContent,
		NewsCreationDate = s.NewsCreationDate,
		NewsLastUpdate = s.NewsLastUpdate,
		NewsGoodVote  = s.NewsGoodVote,
		NewsBadVote = s.NewsBadVote,
		NewsEditionNb = s.NewsEditionNb,
		NewsAuthorID = s.NewsAuthorID
	FROM News s
	where s.NewsID <> 0;