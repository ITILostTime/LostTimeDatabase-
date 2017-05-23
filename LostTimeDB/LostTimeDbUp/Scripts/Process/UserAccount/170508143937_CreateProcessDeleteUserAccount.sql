/* Migration Script */

CREATE PROCEDURE DeleteUserAccount
(
	@UserPseudonym NVARCHAR(64)
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserPseudonym = @UserPseudonym;
END