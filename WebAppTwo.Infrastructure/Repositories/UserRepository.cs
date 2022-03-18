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
        private readonly string _connectionString;
        public UserRepository()
        {
            _connectionString = "Server=DESKTOP-AGBAN3N\\TEW_SQLEXPRESS;Database=UsersDatabase;Trusted_Connection=True;";
        }

        public IEnumerable<User> GetUsers()
        {
            var sql = "select * from Users";
            var connection = new SqlConnection(_connectionString);
            var results = connection.Query<User>(sql, connection);

            return results;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlStatement = $"INSERT INTO Users (UserId, FirstName, LastName, EmailAddress, City) VALUES (1, 'Mateusz', 'Szefler', 'SSS@SSS.COM', 'Ciechanow')";
                connection.Execute(sqlStatement, connection);
            }
        }

        public void DatabaseDelete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlStatement = "DELETE Users WHERE UserId = @Id";
                connection.Execute(sqlStatement, new {Id = id});
            }
        }
    }
}
