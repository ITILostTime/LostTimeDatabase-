/* Migration Script */

CREATE PROCEDURE DeleteGoogleAccountByGoogleID
(
	@UserGoogleID NVARCHAR(64)
)
AS
BEGIN
	DELETE FROM UserGoogleIDToken WHERE UserGoogleID = @UserGoogleID;
END