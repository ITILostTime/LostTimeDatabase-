/* Migration Script */

create view GetAllUserAccount
AS
	SELECT
		UserID = s.UserID,
		UserPseudonym = s.UserPseudonym,
		UserEmail = s.UserEmail,
		UserPassword = s.UserPassword,
		UserPermission = s.UserPermission,
		UserAccountCreationDate  = s.UserAccountCreationDate,
		UserLastConnectionDate = s.UserLastConnectionDate,
		UserGoogleID = s.UserGoogleID,
		UserGoogleToken = s.UserGoogleToken
	FROM UserAccount s
	where s.UserID <> 0;

