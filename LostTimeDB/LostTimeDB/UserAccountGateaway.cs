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

        public UserAccount FindByName(string userPseudonym)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserPseudonym,
                                s.UserEmail,
                                s.UserPassword,
                                s.UserAccountCreationDate,
                                s.UserLastConnectionDate
                    from ViewUserAccountGetByPseudonym s
                    where s.UserPseudonym = @UserPseudonym ;",
                    new { UserPseudonym = userPseudonym })
                    .FirstOrDefault();
            }
        }

        public UserAccount FindByID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserPseudonym,
                                s.UserEmail,
                                s.UserPassword,
                                s.UserAccountCreationDate,
                                s.UserLastConnectionDate
                    from ViewUserAccountGetByID s
                    where s.UserID = @UserID ;",
                    new { UserID = userID })
                    .FirstOrDefault();
            }
        }

        public void CreateNewUserAccount(string userPseudonym, string userEmail, string userPassword, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNewUserAccount",
                    new
                    {
                        UserPseudonym = userPseudonym,
                        UserEmail = userEmail,
                        UserPassword = userPassword,
                        UserAccountCreationDate = date,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUserAccount(string userPseudonym)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteUserAccount",
                    new
                    {
                        UserPseudonym = userPseudonym
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateUserAccount(int userID, string userPseudonym, string userEmail, string userPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateUserAccount",
                    new
                    {
                        UserID = userID,
                        UserPseudonym = userPseudonym,
                        UserEmail = userEmail,
                        UserPassword = userPassword
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
