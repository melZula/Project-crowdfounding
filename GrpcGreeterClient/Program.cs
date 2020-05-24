#region snippet2
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcGreeter;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        #region snippet
        static async Task Main(string[] args)
        {
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001",
                                new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Greeter.GreeterClient(channel);

            // var myrep = await client.GetFoundBalanceAsync(
            //     new Id { Id_ = 2 });
            // System.Console.WriteLine("Response: " + myrep.Value);

            var myrep = await client.GetUserBalanceAsync(
                new Id { Id_ = 2 });
            System.Console.WriteLine("Response: " + myrep.Value);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        #endregion
        static async Task Menu(Greeter.GreeterClient client)
        {
            Console.WriteLine("-----Crowfounding system-----");

            String cmd = "000";
            while (cmd != "exit")
            {
                if (cmd.Contains("getfoundbalance"))
                {
                    var myrep = await client.GetFoundBalanceAsync(
                        new Id { Id_ = 2 });
                    System.Console.WriteLine("Response: " + myrep.Value);
                }
                else if (cmd.Contains("getuserbalance"))
                {
                    var myrep = await client.GetUserBalanceAsync(
                    new Id { Id_ = 2 });
                    System.Console.WriteLine("Response: " + myrep.Value);
                }
                else if (cmd.Contains("givetofound"))
                {
                    var myrep = await client.GiveToFoundAsync(
                    new UserToFound { UserId = 2, FoundId = 2, Value = 50 });
                    System.Console.WriteLine("Response: " + myrep.Status);
                }
                else if (cmd.Contains("addbalance"))
                {
                    var myrep = await client.AddBalanceAsync(
                    new UserAmount { UserId = 2, Value = 50 });
                    System.Console.WriteLine("Response: " + myrep.Status);
                }
                else if (cmd.Contains("createfound"))
                {
                    var myrep = await client.CreateFoundAsync(
                   new FoundOwner { Name = "example", Owner = "example" });
                    System.Console.WriteLine("Response: " + myrep.Status);
                }
                Console.WriteLine("\nChoose action (write num): ");
                cmd = Console.ReadLine().Trim().ToLower();

            }
        }
    }
}
#endregion
