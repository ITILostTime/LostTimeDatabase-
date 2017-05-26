/* Migration Script */

CREATE PROCEDURE DeleteGoogleAccountByGoogleID
(
	@UserGoogleID INT
)
AS
BEGIN
	DELETE FROM UserGoogleIDToken WHERE UserGoogleID = @UserGoogleID;
END