/* Migration Script */

CREATE PROCEDURE UpdatePassword
(
	@UserEmail NVARCHAR(64),
	@UserPassword varbinary(64)
)
AS
BEGIN
	UPDATE UserAccount
	SET
		[UserPassword] = @UserPassword
	WHERE UserEmail = @UserEmail
END