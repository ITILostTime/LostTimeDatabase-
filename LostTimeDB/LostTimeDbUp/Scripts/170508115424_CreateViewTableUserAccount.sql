/* Migration Script */

CREATE VIEW ViewUserAccount
AS
	SELECT
		UserID = s.UserID,
		UserFirstname = s.UserFirstname,
		UserLastname = s.UserLastname,
		UserEmail = s.UserEmail,
		UserPassword = s.UserPassword
	FROM UserAccount s;