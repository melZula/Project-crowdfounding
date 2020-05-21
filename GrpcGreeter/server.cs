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
            var i = store.GetFoundBalance(1);
            System.Console.WriteLine(i);
        }
    }
}