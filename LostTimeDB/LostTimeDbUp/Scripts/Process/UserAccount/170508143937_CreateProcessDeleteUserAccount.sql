/* Migration Script */

CREATE PROCEDURE DeleteUserAccountByName
(
	@UserPseudonym NVARCHAR(64)
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserPseudonym = @UserPseudonym;
END