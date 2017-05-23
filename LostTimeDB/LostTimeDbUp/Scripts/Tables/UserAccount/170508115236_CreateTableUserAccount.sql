/* Migration Script */

CREATE TABLE UserAccount
(
	UserID INT IDENTITY(0, 1) PRIMARY KEY,
	UserPseudonym NVARCHAR(64) NOT NULL,
	UserEmail NVARCHAR(64) NOT NULL,
	UserPassword NVARCHAR(64) NOT NULL,
	UserAccountCreationDate DATETIME2 NOT NULL,
	UserLastConnectionDate DATETIME2,
	UserGoogleToken NVARCHAR(64),
	UserGoogleID Int,
		
	/*
	UserGroupID int,
	ActiveQuest json
	*/
);