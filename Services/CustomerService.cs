using System.Threading.Tasks;
using Grpc.Core;
using grpc_scratch.Proto;
using static grpc_scratch.Proto.CustomerService;

namespace grpc_scratch
{
    public class CustomerService : CustomerServiceBase
    {
        public override Task<CustomerResponse> GetCustomerById(CustomerIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CustomerResponse()
            {
                FullName = "Hello" + request.CustId,
                CustId = request.CustId
            });
        }
    }
}