/* Migration Script */

CREATE PROCEDURE DeleteUserAccount
(
	@UserID NVARCHAR(32)
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserID = @UserID;
END