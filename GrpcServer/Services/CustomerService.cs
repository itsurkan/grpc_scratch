using System.Threading.Tasks;
using Grpc.Core;
using grpc_scratch.Proto;
using static grpc_scratch.Proto.CustomerService;

namespace grpc_scratch
{
    public class CustomerService : CustomerServiceBase
    {
        public override Task<CustomerResponse> GetCustomerById(CustomerIdRequest request, ServerCallContext context) =>
            Task.FromResult(new CustomerResponse()
            {
                FullName = "Hello " + request.CustId,
                CustId = request.CustId
            });
        public override async Task GetCustomerStreamById(CustomerIdRequest request, IServerStreamWriter<CustomerResponse> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 50; i++)
            {
                await responseStream.WriteAsync(
                    new CustomerResponse()
                    {
                        FullName = "Hello " + request.CustId,
                        CustId = request.CustId
                    }
                    );
               await Task.Delay(1000);
            }
        }
    }
}