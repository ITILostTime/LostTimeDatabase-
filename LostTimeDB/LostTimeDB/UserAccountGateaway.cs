using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace LostTimeDB
{
    public class UserAccountGateaway
    {
        readonly string _connectionString;

        public UserAccountGateaway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserAccount FindByName(string userFirstname, string userLastname)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserFirstname,
                                s.UserLastname,
                                s.UserEmail,
                                s.UserPassword
                    from ViewUserAccount s
                    where s.userFirstName = @UserFirstname and s.userLastname = @UserLastname;",
                    new { UserFirstname = userFirstname, UserLastname = userLastname })
                    .FirstOrDefault();
            }
        }

        public void CreateNewUserAccount(string userID, string userFirstname, string userLastname, string userEmail, string userPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNewUserAccount",
                    new
                    {
                        UserID = userID,
                        UserFirstname = userFirstname,
                        UserLastname = userLastname,
                        UserEmail = userEmail,
                        UserPassword = userPassword
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUserAccount(string userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteUserAccount",
                    new
                    {
                        UserID = userID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateUserAccount(string userID, string userFirstname, string userLastname, string userEmail, string userPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateUserAccount",
                    new
                    {
                        UserID = userID,
                        UserFirstname = userFirstname,
                        UserLastname = userLastname,
                        UserEmail = userEmail,
                        UserPassword = userPassword
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
