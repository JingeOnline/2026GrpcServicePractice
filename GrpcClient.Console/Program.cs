using Grpc.Net.Client;
using GrpcServer.Protos;

namespace GrpcClient.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CallGrpcServiceGreetAsync();
            //await CallGrpcServiceCustomerAsync();
            await RunAsync();
        }

        /// <summary>
        /// 请求gRPC服务端的Greet服务
        /// </summary>
        /// <returns></returns>
        static async Task CallGrpcServiceGreetAsync()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            while (true)
            {
                Console.WriteLine("Please enter your name (or 'exit' to quit):");
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                else
                {
                    var reply = await client.SayHelloAsync(new HelloRequest { Name = input });
                    Console.WriteLine("Reply from server: " + reply.Message);
                }
            }
        }

        static async Task CallGrpcServiceCustomerAsync()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);
            while (true)
            {
                Console.WriteLine("Please enter the customer ID 1-3 (or 'exit' to quit):");
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                else
                {
                    var request = new CustomerLookupRequest { Id = int.Parse(input) };
                    try
                    {
                        var reply = await client.GetCustomerInfoAsync(request);
                        ConsoleBeautifulWriteCustomer(reply);
                    }
                    catch (Grpc.Core.RpcException ex)
                    {
                        Console.WriteLine($"Error: {ex.Status.Detail}");
                    }
                }
            }
        }

        static async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("Type 'exit' to quit or press [1] for Greet, [2] for Customer");
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                if (input == "1")
                {
                    await CallGrpcServiceGreetAsync();
                }
                else if (input == "2")
                {
                    await CallGrpcServiceCustomerAsync();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        static void ConsoleBeautifulWriteCustomer(CustomerLookupReply reply)
        {
            Console.WriteLine($"----------------------Reply from server:----------------------");
            Console.WriteLine($"ID: {reply.Id}");
            Console.WriteLine($"Name: {reply.Name}");
            Console.WriteLine($"Email: {reply.Email}");
            Console.WriteLine($"Phone: {reply.Phone}");
            Console.WriteLine($"IsActive: {reply.IsActive}");
            Console.WriteLine($"Age: {reply.Age}");
            Console.WriteLine($"--------------------------------------------------------------");
        }
    }
}
