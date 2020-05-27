#region snippet2
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcGreeter;
using Grpc.Net.Client;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

            await Menu(client);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        #endregion
        static string ToStatus(bool exitCode)
        {
            return ((!exitCode) ? "OK" : "Failed");
        }
        static async Task Menu(Greeter.GreeterClient client)
        {
            Console.WriteLine("-----Crowfounding system-----");

            String cmd = "000";
            while (cmd != "exit")
            {
                if (cmd.Contains("createfound"))
                {
                    string[] fields = cmd.Split(' ');
                    if (fields.Length == 3)
                    {
                        var myrep = await client.CreateFoundAsync(
                        new FoundOwner { Name = fields[1], Owner = fields[2] });
                        System.Console.WriteLine("Response: " + ToStatus(myrep.Status));
                    }
                }
                cmd = cmd.ToLower();
                if (cmd.Contains("getfoundbalance"))
                {
                    var res = Regex.Match(cmd, @"\d+").Value;
                    long id;
                    if (Int64.TryParse(res, out id))
                    {
                        var myrep = await client.GetFoundBalanceAsync(
                        new Id { Id_ = id });
                        System.Console.WriteLine("Response: " + myrep.Value);
                    }
                    else System.Console.WriteLine("No arguments");
                }
                else if (cmd.Contains("getuserbalance"))
                {
                    List<long> args = parseArgs(cmd);
                    var myrep = await client.GetUserBalanceAsync(
                    new Id { Id_ = args[0] });
                    System.Console.WriteLine("Response: " + myrep.Value);
                }
                else if (cmd.Contains("givetofound"))
                {
                    List<long> args = parseArgs(cmd);
                    if (args.Count == 3)
                    {
                        var myrep = await client.GiveToFoundAsync(
                        new UserToFound { UserId = args[0], FoundId = args[1], Value = args[2] });
                        System.Console.WriteLine("Response: " + ToStatus(myrep.Status));
                    }
                }
                else if (cmd.Contains("addbalance"))
                {
                    List<long> args = parseArgs(cmd);
                    if (args.Count == 2)
                    {
                        var myrep = await client.AddBalanceAsync(
                        new UserAmount { UserId = args[0], Value = args[1] });
                        System.Console.WriteLine("Response: " + ToStatus(myrep.Status));
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid numbers of arguments");
                    }
                }
                Console.WriteLine("\nChoose action: ");
                cmd = Console.ReadLine().Trim();
            }
        }
        static List<long> parseArgs(string cmd)
        {
            string[] numbers = Regex.Split(cmd, @"\D+");
            List<long> args = new List<long>();
            foreach (string number in numbers)
            {
                if (!string.IsNullOrEmpty(number))
                {
                    long arg;
                    if (Int64.TryParse(number, out arg))
                    {
                        args.Add(arg);
                    }
                    else
                    {
                        System.Console.WriteLine(arg);
                        System.Console.WriteLine("Invalid arguments");
                    }
                }
            }
            return args;
        }
    }
}
#endregion
