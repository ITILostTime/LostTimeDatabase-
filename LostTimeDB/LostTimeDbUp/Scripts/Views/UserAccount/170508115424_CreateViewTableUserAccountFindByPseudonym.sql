/* Migration Script */

CREATE VIEW ViewUserAccountFindByPseudonym
AS
	SELECT
		UserID = s.UserID,
		UserPseudonym = s.UserPseudonym,
		UserEmail = s.UserEmail,
		UserPassword = s.UserPassword,
		UserAccountCreationDate = s.UserAccountCreationDate,
		UserLastConnectionDate  = s.UserLastConnectionDate,
		UserGoogleToken = s.UserGoogleToken,
		UserGoogleID = s.UserGoogleID
	FROM UserAccount s;