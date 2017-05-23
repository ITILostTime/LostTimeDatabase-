/* Migration Script */

CREATE PROCEDURE DeleteUserAccountByGoogleID
(
	@UserGoogleID INT
)
AS
BEGIN
	DELETE FROM UserAccount WHERE UserGoogleID = @UserGoogleID;
END