using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using porgKorny_WPF_beadando.Model;

namespace porgKorny_WPF_beadando.Services
{
    class UserDatabase : Database
    {
        //public List<User> GetUsers()
        //{
        //    var users = new List<User>();

        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = "SELECT * FROM users";

        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                users.Add(new User
        //                {
        //                    Id = reader.GetInt32("id"),
        //                    Username = reader.GetString("username"),
        //                    Password = reader.GetString("password")
        //                });
        //            }
        //        }
        //    }

        //    return users;
        //}

        private static UserDatabase instance;

        public static UserDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDatabase();
                }
                return instance; 
            }
        }


        public bool CheckUserCredentials(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM users WHERE name = @username AND password = @password";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    long count = (long)cmd.ExecuteScalar();
                    return count == 1;
                }
            }
        }

        public User LoadUserData(string userId)
        {
            using var conn = new MySqlConnection(base.connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM users WHERE name = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserName = reader["name"].ToString(),
                    Balance = (int)Convert.ToDecimal(reader["money"])
                };
            }

            return null;
        }


    }
}
