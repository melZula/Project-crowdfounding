using System;

namespace GrpcGreeter
{
    class Server
    {
        private Store store;
        public Server(Store s)
        {
            store = s;
            System.Console.WriteLine("Start serve");
        }
        public void Test()
        {
            var u = this.store.CreateUser("Kate");
            System.Console.WriteLine(u.id);
        }
    }
}