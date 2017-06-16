/* Migration Script */

CREATE PROCEDURE CreateNewUserGoogleIDToken
(
	@UserGoogleID NVARCHAR(64),
	@UserGoogleToken NVARCHAR(64)
)
AS
BEGIN
	INSERT INTO UserGoogleIDToken(UserGoogleID, UserGoogleToken)
	VALUES(@UserGoogleID, @UserGoogleToken)
END