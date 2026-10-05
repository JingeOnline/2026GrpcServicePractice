using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerLookupReply> GetCustomerInfo(CustomerLookupRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Received request for customer ID: {request.Id}");
            List<CustomerLookupReply> customers = new List<CustomerLookupReply>
            {
                new CustomerLookupReply
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Phone = "+1-555-1234",
                    IsActive = true,
                    Age = 30
                },
                new CustomerLookupReply
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "+1-555-5678",
                    IsActive = false,
                    Age = 25
                },
                new CustomerLookupReply
                {
                    Id= 3,
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Phone = "+1-555-9012",
                    IsActive = true,
                    Age = 40
                }
            };

            var customer = customers.FirstOrDefault(c => c.Id == request.Id);
            if (customer == null)
            {
                //找不到的情况，抛出这个异常，可以返回一个 gRPC 错误状态码，例如 NotFound。
                //服务器不会因为抛出异常而崩溃，而是会将错误状态码和消息返回给客户端。官方推荐这种做法。
                throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));
            }

            return Task.FromResult(customer);
        }
    }
}
