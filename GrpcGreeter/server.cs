using System;

namespace GrpcGreeter
{
    class Server
    {
        private Store store;

        public Server(Store s)
        {
            this.store = s;
        }
        public void Test()
        {
            this.store.Test();
            System.Console.WriteLine("Done");
        }
    }
}