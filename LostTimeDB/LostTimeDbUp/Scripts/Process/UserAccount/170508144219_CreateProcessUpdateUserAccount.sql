/* Migration Script */

CREATE PROCEDURE UpdateUserAccount
(
	@UserID INT,
	@UserPseudonym NVARCHAR(64),
	@UserEmail NVARCHAR(64),
	@UserPassword NVARCHAR(64)
)
AS
BEGIN
	UPDATE UserAccount
	SET
		UserPseudonym = @UserPseudonym,
		UserEmail = @UserEmail,
		UserPassword = @UserPassword
	WHERE UserID = @UserID
END