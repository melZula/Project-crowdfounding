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