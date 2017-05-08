/* Migration Script */

CREATE TABLE UserAccount
(
	UserID NVARCHAR(32) NOT NULL,
	UserFirstname NVARCHAR(32) NOT NULL,
	UserLastname NVARCHAR(32) NOT NULL,
	UserEmail NVARCHAR(64) NOT NULL,
	UserPassword NVARCHAR(32) NOT NULL

	CONSTRAINT UniqueNvarchar_UserID UNIQUE(UserID),
	CONSTRAINT UniqueNvarchar_UserEmail UNIQUE(UserEmail),
);