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
    public class UserAccountGateway
    {
        readonly string _connectionString;

        public UserAccountGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<UserAccount> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<UserAccount>(
                    @"select s.UserID,
                             s.UserPseudonym,
                             s.UserEmail,
                             s.UserPassword,
                             s.UserPermission,
                             s.UserAccountCreationDate,
                             s.UserLastConnectionDate,
                             s.UserGoogleID,
                             s.UserGoogleToken
                      from GetAllUserAccount s;");
            }
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
                                s.UserPermission,
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
                                s.UserPermission,
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
        public UserAccount FindByGoogleID(string userGoogleID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserPseudonym,
                                s.UserEmail,
                                s.UserPassword,
                                s.UserPermission,
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

        public UserAccount FindByUserEmail(string userEmail)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<UserAccount>(
                    @"select    s.UserID,
                                s.UserPseudonym,
                                s.UserEmail,
                                s.UserPassword,
                                s.UserPermission,
                                s.UserAccountCreationDate,
                                s.UserLastConnectionDate,
                                s.UserGoogleID,
                                s.UserGoogleToken
                        from ViewUserAccountFindByEmail s
                        where s.UserEmail = @UserEmail;",
                        new { UserEmail = userEmail })
                        .FirstOrDefault();
            }
        }

        public void CreateNewUserAccount(string userPseudonym, string userEmail, byte[] userPassword, DateTime date)
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
                        UserPermission = "USER"
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

        public void DeleteUserAccountByGoogleID(string googleID)
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

        public void UpdateUserAccount(int userID, string userPseudonym, string userEmail, byte[] userPassword)
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

        public void UpdatePassword(string userEmail, byte[] userPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdatePassword",
                    new
                    {
                        UserEmail = userEmail,
                        UserPassword = userPassword
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateUserPermission(int userID, string userPermission)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UpdateUserPermission",
                    new
                    {
                        UserID = userID,
                        UserPermission = userPermission
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
