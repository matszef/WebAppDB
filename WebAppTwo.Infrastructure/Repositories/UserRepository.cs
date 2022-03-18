using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WebAppTwo.Infrastructure.Models;

namespace WebAppTwo.Infrastructure
{
    public class UserRepository
    {
        public void DatabaseConnection()
        {
            var sql = "select * from Users";
            var users = new List<User>();

            string connString = "Server=DESKTOP-AGBAN3N\\TEW_SQLEXPRESS;Database=UsersDatabase;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                        };
                        users.Add(user);
                    }
                }
            }
        }

        public void DatabaseInsert()
        {
            string connString = "Server=DESKTOP-AGBAN3N\\TEW_SQLEXPRESS;Database=UsersDatabase;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var sqlStatement = @"INSERT INTO Users
                    (UserId, FirstName, LastName, EmailAddress, City)
                    VALUES (1, 'Mateusz', 'Szefler', 'SSS@SSS.COM', 'Ciechanow')";
                connection.Execute(sqlStatement, connection);
            }
        }

        public void DatabaseDelete(int id)
        {
            string connString = "Server=DESKTOP-AGBAN3N\\TEW_SQLEXPRESS;Database=UsersDatabase;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var sqlStatement = "DELETE Users WHERE UserId = @Id";
                connection.Execute(sqlStatement, new {Id = id});
            }
        }
    }
}
