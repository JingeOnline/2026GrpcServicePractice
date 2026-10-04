using Grpc.Core;

namespace GrpcServer.Services
{
    //GreeterBase 类型是从 .proto 文件的 Greeter 服务生成的。
    public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("The message is received from {Name}", request.Name);
            HelloReply reply = new HelloReply
            {
                Message = "Hello " + request.Name
            };
            return Task.FromResult(reply);
        }
    }
}
