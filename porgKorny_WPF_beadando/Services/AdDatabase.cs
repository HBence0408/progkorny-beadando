using MySql.Data.MySqlClient;
using porgKorny_WPF_beadando.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace porgKorny_WPF_beadando.Services
{
    class AdDatabase : Database
    {

        private static AdDatabase instance;

        public static AdDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdDatabase();
                }
                return instance;
            }
        }

        public List<Ad> GetOwnAds(string user)
        {
            var ads = new List<Ad>();

            using var conn = new MySqlConnection(base.connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM ads WHERE user = @user ORDER BY created_at DESC", conn);
            cmd.Parameters.AddWithValue("@user", user);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Price = (int)Convert.ToDecimal(reader["price"]),
                   // CreatedAt = Convert.ToDateTime(reader["created_at"])
                    UserName = reader["user"].ToString()
                });
            }

            return ads;
        }

        public List<Ad> SearchOtherAds(string user, string query)
        {
            var ads = new List<Ad>();

            using var conn = new MySqlConnection(base.connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM ads WHERE user != @user AND title LIKE @keyword", conn);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@keyword", $"%{query}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Id = (int)Convert.ToDecimal(reader["id"]),
                    Title = reader["title"].ToString(),
                    Description = reader["description"].ToString(),
                    Price = (int)Convert.ToDecimal(reader["price"]),
                  //  CreatedAt = Convert.ToDateTime(reader["created_at"])
                  UserName = reader["user"].ToString()
                });
            }

            return ads;
        }

        public void InsertAd(string title, string desc, decimal price, string user)
        {
            using var conn = new MySqlConnection(base.connectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO ads (title, description, price, user) VALUES (@title, @desc, @price, @user)", conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@user", user);

            cmd.ExecuteNonQuery();
        }

        public bool PurchaseAd(int adId, decimal price, string userId, string seller)
        {
            using var conn = new MySqlConnection(base.connectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                var deductCmd = new MySqlCommand("UPDATE users SET money = money - @price WHERE name = @userId", conn, transaction);
                deductCmd.Parameters.AddWithValue("@price", (int)price);
                deductCmd.Parameters.AddWithValue("@userId", userId);
                deductCmd.ExecuteNonQuery();

                var sellerdeductCmd = new MySqlCommand("UPDATE users SET money = money + @price WHERE name = @userId", conn, transaction);
                sellerdeductCmd.Parameters.AddWithValue("@price", (int)price);
                sellerdeductCmd.Parameters.AddWithValue("@userId", seller);
                sellerdeductCmd.ExecuteNonQuery();

                var deleteCmd = new MySqlCommand("DELETE FROM ads WHERE id = @adId", conn, transaction);
                deleteCmd.Parameters.AddWithValue("@adId", adId);
                deleteCmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
