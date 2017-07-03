﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace LostTimeDB
{
    public class QuestGateway
    {
        readonly string _connectionString;

        public QuestGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateQuest(string questTitle, string questData, int authorID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNewQuest",
                    new
                    {
                        QuestTitle = questTitle,
                        QuestData = questData,
                        QuestLastEdit = DateTime.Now,
                        AuthorID = authorID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteQuestByQuestID(int questID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteQuestByQuestID",
                    new
                    {
                        QuestID = questID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Quest FindQuestBYQuestID(int questID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Quest>(
                    @"select    s.QuestID,
                                s.QuestTitle,
                                s.QuestData,
                                s.QuestLastEdit,
                                s.AuthorID
                        from FindQuestByQuestID s
                        where s.QuestID = @QuestID ;",
                        new { QuestID = questID })
                        .FirstOrDefault();
            }
        }

        public IEnumerable<Quest> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Quest>(
                    @"select    s.QuestID,
                                s.QuestTitle,
                                s.QuestData,
                                s.QuestLastEdit,
                                s.AuthorID
                      from GetAllQuest s;");
            }
        }

        public void UpdateQuest(int questID, string questTitle, string questData, int authorID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateQuest",
                    new
                    {
                        QuestID = questID,
                        QuestTitle = questTitle,
                        QuestData = questData,
                        QuestLastEdit = DateTime.Now,
                        AuthorID = authorID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }


    }
}