using DbUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LostTimeDbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault()
                ?? "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";


            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.WriteLine("Fail");
                Console.ReadLine();
#endif
                return -1;
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            //CreateUserID(connectionString);

            return 0;
        }

        public static int CreateUserID(string connectionstring)
        {
            string userID;
            string userFirstname;
            string userLastname;
            string userEmail;
            string userPassword01;
            string userPassword02;

            Console.WriteLine("Identifiant");
            userID = Console.ReadLine();

            Console.WriteLine("Firstname");
            userFirstname = Console.ReadLine();

            Console.WriteLine("Lastname");
            userLastname = Console.ReadLine();

            Console.WriteLine("Email");
            userEmail = Console.ReadLine();

            Console.WriteLine("Password01");
            userPassword01 = Console.ReadLine();

            Console.WriteLine("Password02");
            userPassword02 = Console.ReadLine();
            
            LostTimeDB.UserAccountGateaway user = new LostTimeDB.UserAccountGateaway(connectionstring);

            if (userPassword01 == userPassword02)
            {
                LostTimeDB.UserAccount userAccount = new LostTimeDB.UserAccount();

                user.CreateNewUserAccount(userID, userFirstname, userLastname, userEmail, userPassword01);

                userAccount = user.FindByName(userFirstname, userLastname);
                
                Console.WriteLine(userAccount.UserID);
                Console.WriteLine(userAccount.UserFirstname);
                Console.WriteLine(userAccount.UserLastname);
                Console.WriteLine(userAccount.UserEmail);
                Console.WriteLine(userAccount.UserPassword);

                //user.DeleteUserAccount("julie");

                //user.UpdateUserAccount(userAccount.UserID, "aa", "aa", "aa", "aa");
;
            }
            else
            {
                Console.WriteLine("Vos mots de passe sont différents");
            }

            Console.ReadLine();

            return 0;

        }
    }
}
