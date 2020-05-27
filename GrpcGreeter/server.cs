using System;

namespace GrpcGreeter
{
    public class Server
    {
        public Store store;
        public Server(Store s)
        {
            store = s;
            System.Console.WriteLine("Start serve");
        }
        public void Test()
        {
            var i = store.SearchUser("Zula");
            System.Console.WriteLine(i);
        }
    }
}