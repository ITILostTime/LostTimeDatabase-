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
    public class GoogleAccountGateaway
    {
        readonly string _connectionString;

        public GoogleAccountGateaway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create( int userGoogleID, string userGoogleToken)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNewUserGoogleIDToken",
                    new
                    {
                        UserGoogleID = userGoogleID,
                        UserGoogleToken = userGoogleToken,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AssignGoogleIDToUserID(int userGoogleID, string userGoogleToken, int userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "CreateNewProcessAssignUserGoogleIDToUserID",
                    new
                    {
                        UserGoogleID = userGoogleID,
                        UserGoogleToken = userGoogleToken,
                        UserID = userID,
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteGoogleAccountByGoogleID(int googleID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DeleteGoogleAccountByGoogleID",
                    new
                    {
                        UserGoogleID = googleID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
