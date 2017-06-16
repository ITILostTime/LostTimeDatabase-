/* Migration Script */

CREATE TABLE UserGoogleIDToken
(
	UserGoogleID nvarchar(64) PRIMARY KEY,
	UserGoogleToken NVARCHAR(64),
)