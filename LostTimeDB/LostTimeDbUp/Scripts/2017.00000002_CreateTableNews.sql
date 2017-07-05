/* Migration Script */

CREATE TABLE News
(
	NewsID int identity(0,1) Primary key,
	NewsTitle nvarchar(64) not null,
	NewsContent nvarchar(max) not null,
	NewsCreationDate Datetime2 not null,
	NewsLastUpdate Datetime2 not null,
	NewsGoodVote int,
	NewsBadVote int,
	NewsEditionNb int,
	NewsAuthorID int,
);