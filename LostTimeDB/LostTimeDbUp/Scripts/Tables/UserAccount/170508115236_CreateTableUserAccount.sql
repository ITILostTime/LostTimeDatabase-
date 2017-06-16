/* Migration Script */

CREATE TABLE UserAccount
(
	UserID INT IDENTITY(0, 1) PRIMARY KEY,
	UserPseudonym NVARCHAR(64) NOT NULL,
	UserEmail NVARCHAR(64) NOT NULL,
	[UserPassword] varbinary(64) NOT NULL,
	UserAccountCreationDate DATETIME2 NOT NULL,
	UserLastConnectionDate DATETIME2,
	UserGoogleID nvarchar(64),
	UserGoogleToken NVARCHAR(64),

	CONSTRAINT FK_UserGoogleID FOREIGN KEY (UserGoogleID) REFERENCES UserGoogleIDToken (UserGoogleID),
);