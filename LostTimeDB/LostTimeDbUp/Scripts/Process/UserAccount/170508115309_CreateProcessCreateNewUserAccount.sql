/* Migration Script */

CREATE PROCEDURE CreateNewUserAccount
(
	@UserPseudonym NVARCHAR(64),
	@UserEmail NVARCHAR(64),
	@UserPassword NVARCHAR(64),
	@UserAccountCreationDate DATETIME2,
	@UserLastConnectionDate DATETIME2
)
AS
BEGIN
	INSERT INTO UserAccount(Userpseudonym, UserEmail, UserPassword, UserAccountCreationDate, UserLastConnectionDate)
	VALUES(@UserPseudonym, @UserEmail, @UserPassword, @UserAccountCreationDate, @UserLastConnectionDate)
END