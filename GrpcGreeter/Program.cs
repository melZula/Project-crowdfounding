using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GrpcGreeter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            doStore();
            // CreateHostBuilder(args).Build().Run();
        }

        public static void doStore()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // создаем два объекта User
                // User user1 = new User { name = "Zula", balance = 100 };
                // User user2 = new User { name = "Vaka", balance = 350 };
                // Found f = new Found { name = "ZulaFound", owner = 2 };

                // добавляем их в бд
                // db.Users.Add(user1);
                // db.Users.Add(user2);
                // db.Founds.Add(f);
                // db.SaveChanges();

                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.id}.{u.name} - {u.balance}");
                }
            }
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
