/* Migration Script */
create table Quest
(
	QuestID INT IDENTITY(1, 1) PRIMARY KEY,
	QuestTitle NVARCHAR(64) NOT NULL,
	QuestData nvarchar(max) not null,
	QuestLastEdit datetime2 not null,
	AuthorID int not null,
);