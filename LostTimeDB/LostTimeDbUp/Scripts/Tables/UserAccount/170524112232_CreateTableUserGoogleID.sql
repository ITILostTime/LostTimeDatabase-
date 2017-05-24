/* Migration Script */

CREATE TABLE UserGoogleIDToken
(
	UserGoogleID INT PRIMARY KEY,
	UserGoogleToken NVARCHAR(64),
);