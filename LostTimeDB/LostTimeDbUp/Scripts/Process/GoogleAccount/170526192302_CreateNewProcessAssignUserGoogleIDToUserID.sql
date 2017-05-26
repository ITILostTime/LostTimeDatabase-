/* Migration Script */

CREATE PROCEDURE CreateNewProcessAssignUserGoogleIDToUserID
(
	@UserGoogleID INT,
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