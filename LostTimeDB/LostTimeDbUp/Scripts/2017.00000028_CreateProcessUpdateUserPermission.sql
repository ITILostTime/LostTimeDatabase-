/* Migration Script */

CREATE PROCEDURE UpdateUserPermission
(
	@UserID int,
	@UserPermission varchar(5)
)
AS
BEGIN
	UPDATE UserAccount
	SET
		[UserPermission] = @UserPermission
	WHERE UserID = @UserID
END