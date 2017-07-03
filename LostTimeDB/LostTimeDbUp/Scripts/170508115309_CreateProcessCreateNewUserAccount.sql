/* Migration Script */

CREATE PROCEDURE CreateNewUserAccount
(
	@UserPseudonym NVARCHAR(64),
	@UserEmail NVARCHAR(64),
	@UserPassword varbinary(64),
	@UserAccountCreationDate DATETIME2,
	@UserLastConnectionDate DATETIME2,
	@UserPermission varchar(5)
)
AS
BEGIN
	INSERT INTO UserAccount(Userpseudonym, UserEmail, [UserPassword], UserAccountCreationDate, UserLastConnectionDate, [UserPermission])
	VALUES(@UserPseudonym, @UserEmail, @UserPassword, @UserAccountCreationDate, @UserLastConnectionDate, @UserPermission)
END