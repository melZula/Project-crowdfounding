using System;
using Npgsql;

namespace GrpcGreeter
{
    class Store
    {
        private String databaseURL;
        public Store(String URL)
        {
            databaseURL = URL;
        }
        public void AddUser(String name)
        {
            //int id = -1;
            var conn = new NpgsqlConnection(this.databaseURL);
            conn.Open();
            // Insert new user 
            using (var cmd = new NpgsqlCommand("INSERT INTO foundb.public.'Users' (name) VALUES (@n);", conn))
            {
                cmd.Parameters.AddWithValue("n", name);
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("inserted");
            }
            conn.Close();

        }
        public async void Test()
        {
            await using var conn = new NpgsqlConnection(this.databaseURL);
            await conn.OpenAsync();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM Founds;", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    Console.WriteLine(reader.GetString(0));
        }
    }
}