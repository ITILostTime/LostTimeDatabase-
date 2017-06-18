/* Migration Script */

CREATE VIEW ViewUserAccountFindByEmail
AS
	SELECT
			UserID = s.UserID,
			UserPseudonym = s.UserPseudonym,
			UserEmail = s.UserEmail,
			UserPassword = s.UserPassword,
			UserPermission = s.UserPermission,
			UserAccountCreationDate = s.UserAccountCreationDate,
			UserLastConnectionDate  = s.UserLastConnectionDate,
			UserGoogleToken = s.UserGoogleToken,
			UserGoogleID = s.UserGoogleID
	FROM UserAccount s;