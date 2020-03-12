using System.Threading.Tasks;
using Grpc.Core;
using grpc_scratch.Proto;
using Microsoft.AspNetCore.Authorization;
using static grpc_scratch.Proto.CustomerService;

namespace grpc_scratch
{
    public class CustomerService : CustomerServiceBase
    {
        [Authorize]
        public override Task<CustomerResponse> GetCustomerById(CustomerIdRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;

            return Task.FromResult(new CustomerResponse()
            {
                FullName = "Hello " + user.Identity.Name,
                CustId = request.CustId
            });
        }

        public override async Task GetCustomerStreamById(CustomerIdRequest request, IServerStreamWriter<CustomerResponse> responseStream, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;
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