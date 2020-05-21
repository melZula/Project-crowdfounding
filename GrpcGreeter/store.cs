using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GrpcGreeter
{
    class Store
    {
        private String connectionString;
        public Store(String _connectionString)
        {
            connectionString = _connectionString;
        }
        public void CreateFound(string name, string ownerName)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                User u = SearchUser(ownerName);
                if (u == null) return;
                Found f = new Found { name = name, owner = u.id };

                db.Founds.Add(f);
                db.SaveChanges();

                System.Console.WriteLine($"Found {name}");
            }
        }
        protected User SearchUser(string name)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                try
                {
                    User u = (from user in db.Users
                              where user.name == name
                              select user).Single();
                    return u;
                }
                catch (Exception)
                {
                    System.Console.WriteLine("User not found");
                    return null;
                }
            }
        }
        public User CreateUser(string name)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                try
                {
                    User u = (from user in db.Users
                              where user.name == name
                              select user).Single();
                    System.Console.WriteLine("User already exists");
                    return u;
                }
                catch (Exception)
                {
                    User u = new User { name = name };
                    db.Users.Add(u);
                    db.SaveChanges();
                    return u;
                }
            }
        }
    }
}