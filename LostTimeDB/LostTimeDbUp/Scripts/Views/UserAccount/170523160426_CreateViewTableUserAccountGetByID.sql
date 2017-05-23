/* Migration Script */

CREATE VIEW ViewUserAccountGetByID
AS
	SELECT
		UserID = s.UserID,
		UserPseudonym = s.UserPseudonym,
		UserEmail = s.UserEmail,
		UserPassword = s.UserPassword,
		UserAccountCreationDate = s.UserAccountCreationDate,
		UserLastConnectionDate  = s.UserLastConnectionDate
	FROM UserAccount s;