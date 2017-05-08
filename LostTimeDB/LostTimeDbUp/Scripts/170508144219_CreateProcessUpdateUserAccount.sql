/* Migration Script */

CREATE PROCEDURE UpdateUserAccount
(
	@UserID NVARCHAR(32),
	@UserFirstname NVARCHAR(32),
	@UserLastname NVARCHAR(32),
	@UserEmail NVARCHAR(64),
	@UserPassword NVARCHAR(32)
)
AS
BEGIN
	UPDATE UserAccount
	SET 
		UserFirstname = @UserFirstname,
		UserLastname = @UserLastname,
		UserEmail = @UserEmail,
		UserPassword = @UserPassword
	WHERE UserID = @UserID
END