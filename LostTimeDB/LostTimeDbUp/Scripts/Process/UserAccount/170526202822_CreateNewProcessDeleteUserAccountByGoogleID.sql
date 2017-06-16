/* Migration Script */

CREATE PROCEDURE DeleteUserAccountByGoogleID
(
	@UserGoogleID NVARCHAR(64)
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserGoogleID = @UserGoogleID;
END