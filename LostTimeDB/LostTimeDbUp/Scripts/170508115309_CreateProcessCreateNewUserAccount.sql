/* Migration Script */

CREATE PROCEDURE CreateNewUserAccount
(
	@UserID NVARCHAR(32),
	@UserFirstname NVARCHAR(32),
	@UserLastname NVARCHAR(32),
	@UserEmail NVARCHAR(64),
	@UserPassword NVARCHAR(32)
)
AS
BEGIN
	INSERT INTO UserAccount(UserID, UserFirstname, UserLastname, UserEmail, UserPassword) 
	VALUES (@UserID, @UserFirstname, @UserLastname, @UserEmail, @UserPassword);
END
    