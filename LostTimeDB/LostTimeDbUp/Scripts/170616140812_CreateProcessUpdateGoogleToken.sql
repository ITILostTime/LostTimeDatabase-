/* Migration Script */

CREATE PROCEDURE UpdateUserGoogleToken
(
	@UserGoogleID nvarchar(64),
	@UserGoogleToken NVARCHAR(64)
)
AS
BEGIN
	UPDATE UserAccount
	SET
		UserGoogleToken = @UserGoogleToken
	WHERE UserGoogleID = @UserGoogleID
END