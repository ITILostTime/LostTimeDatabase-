/* Migration Script */

CREATE PROCEDURE CreateNewProcessAssignUserGoogleIDToUserID
(
	@UserGoogleID NVARCHAR(64),
	@UserGoogleToken NVARCHAR(64),
	@UserID INT
)
AS
BEGIN
	UPDATE UserAccount
	SET 
		UserGoogleID = @UserGoogleID,
		UserGoogleToken = @UserGoogleToken
	WHERE UserID = @UserID
END