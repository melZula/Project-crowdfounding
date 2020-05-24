using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GrpcGreeter
{
    public class Store
    {
        private String connectionString;
        public Store(String _connectionString)
        {
            connectionString = _connectionString;
        }
        // CreateFound ...
        public Found CreateFound(string name, string ownerName)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                Found f = SearchFound(name);
                User u = SearchUser(ownerName);
                if (u == null) return null;
                if (f != null)
                {
                    System.Console.WriteLine("Found already exists");
                    return null;
                }
                f = new Found { name = name, owner = u.id };

                db.Founds.Add(f);
                db.SaveChanges();

                return f;
            }
        }
        public Found SearchFound(string name)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                try
                {
                    Found f = (from found in db.Founds
                               where found.name == name
                               select found).Single();
                    return f;
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Found not found");
                    return null;
                }
            }
        }
        public Found SearchFound(long id)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                try
                {
                    Found f = (from found in db.Founds
                               where found.id == id
                               select found).Single();
                    return f;
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Found not found");
                    return null;
                }
            }
        }
        public User SearchUser(string name)
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
        public User SearchUser(long id)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                try
                {
                    User u = (from user in db.Users
                              where user.id == id
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
        // AddBalance ...
        public bool AddBalance(long userId, long amount)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                User u = SearchUser(userId);
                if (u == null) return true;
                u.balance += amount;
                db.Users.Update(u);
                db.SaveChanges();
            }
            return false;
        }
        // GetFoundBalance ...
        public long GetFoundBalance(long id)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                Found f = SearchFound(id);
                if (f == null) return 0;
                else return f.balance;
            }
        }
        // GetUserBalance ...
        public long GetUserBalance(long id)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                User u = SearchUser(id);
                if (u == null) return 0;
                else return u.balance;
            }
        }
        // GiveToFound
        public bool GiveToFound(long userId, long foundId, long amount)
        {
            using (ApplicationContext db = new ApplicationContext(connectionString))
            {
                Found f = SearchFound(foundId);
                User u = SearchUser(userId);
                if (f == null || u == null) return true;
                if (u.balance < amount)
                {
                    System.Console.WriteLine("Insufficient funds on account");
                    return true;
                }
                if (amount < 0)
                {
                    System.Console.WriteLine("Invalid amount");
                    return true;
                }
                u.balance -= amount;
                f.balance += amount;
                db.Users.Update(u);
                db.Founds.Update(f);
                db.SaveChanges();

            }
            return false;
        }
    }
}