using GrpcServer.Services;

namespace GrpcServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // 加载该服务后，gRPC 服务将会监听来自客户端的请求。
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<CustomerService>();
            // 该端点用于测试 gRPC 服务是否正常运行。
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
