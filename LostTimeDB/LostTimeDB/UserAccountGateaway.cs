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
                                s.UserLastConnectionDate,
                                s.UserGoogleToken,
                                s.UserGoogleID
                    from ViewUserAccountFindByPseudonym s
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
                                s.UserLastConnectionDate,
                                s.UserGoogleToken,
                                s.UserGoogleID
                    from ViewUserAccountFindByID s
                    where s.UserID = @UserID ;",
                    new { UserID = userID })
                    .FirstOrDefault();
            }
        }
        public UserAccount FindByGoogleID(int userGoogleID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserPseudonym,
                                s.UserEmail,
                                s.UserPassword,
                                s.UserAccountCreationDate,
                                s.UserLastConnectionDate,
                                s.UserGoogleID,
                                s.UserGoogleToken
                        from ViewUserAccountFindByGoogleID s
                        where s.UserGoogleID = @UserGoogleID;",
                        new { UserGoogleID = userGoogleID })
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
                        UserLastConnectionDate = date,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUserAccountByName(string userPseudonym)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteUserAccountByName",
                    new
                    {
                        UserPseudonym = userPseudonym
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUserAccountByUserID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteUserAccountByUserID",
                    new
                    {
                        UserID = userID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUserAccountByGoogleID(int googleID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteUserAccountByGoogleID",
                    new
                    {
                        UserGoogleID = googleID
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
