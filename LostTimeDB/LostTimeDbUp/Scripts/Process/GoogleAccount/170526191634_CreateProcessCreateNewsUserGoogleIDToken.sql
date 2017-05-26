/* Migration Script */

CREATE PROCEDURE CreateNewUserGoogleIDToken
(
	@UserGoogleID INT,
	@UserGoogleToken NVARCHAR(64)
)
AS
BEGIN
	INSERT INTO UserGoogleIDToken(UserGoogleID, UserGoogleToken)
	VALUES(@UserGoogleID, @UserGoogleToken)
END