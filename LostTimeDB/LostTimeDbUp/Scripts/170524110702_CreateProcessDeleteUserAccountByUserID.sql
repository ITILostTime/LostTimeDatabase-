/* Migration Script */

CREATE PROCEDURE DeleteUserAccountByUserID
(
	@UserID INT
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserID = @UserID;
END