/* Migration Script */

CREATE PROCEDURE CreateNewUserAccountWithGoogleInformation
(
	@UserPseudonym NVARCHAR(64),
	@UserEmail NVARCHAR(64),
	@UserPassword NVARCHAR(64),
	@UserAccountCreationDate DATETIME2,
	@UserLastConnectionDate DATETIME2,
	@UserGoogleToken NVARCHAR(64),
	@UserGoogleID Int
)
AS
BEGIN
	INSERT INTO UserAccount(UserPseudonym, UserEmail, UserPassword, UserAccountCreationDate, UserLastConnectionDate, UserGoogleToken, UserGoogleID)
	VALUES(@UserPseudonym, @UserEmail, @UserPassword, @UserAccountCreationDate, @UserLastConnectionDate, @UserGoogleToken, @UserGoogleID)
END