/* Migration Script */

CREATE PROCEDURE DeleteNewsByNewsID
(
	@NewsID INT
)
AS
BEGIN
	DELETE FROM News WHERE NewsID = @NewsID;
END