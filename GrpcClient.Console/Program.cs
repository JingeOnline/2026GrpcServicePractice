using Grpc.Net.Client;

namespace GrpcClient.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CallGrpcServiceAsync();
        }

        static async Task CallGrpcServiceAsync()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            while(true)
            {
                Console.WriteLine("Type 'exit' to quit or press Enter to call the gRPC service...");
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
    }
}
