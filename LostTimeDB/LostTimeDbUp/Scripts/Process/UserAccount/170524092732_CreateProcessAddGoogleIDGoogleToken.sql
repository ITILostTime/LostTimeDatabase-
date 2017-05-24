/* Migration Script */

CREATE PROCEDURE AddGoogleIDGoogleToken
(
	@UserID INT,
	@UserGoogleToken NVARCHAR(64),
	@UserGoogleID Int
)
AS
BEGIN
	UPDATE UserAccount
	SET
		UserGoogleToken = @UserGoogleToken,
		UserGoogleID = @UserGoogleID
	WHERE UserID = @UserID
END