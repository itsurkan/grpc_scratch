using Grpc.Net.Client;
using grpc_scratch.Proto;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace gRPC_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient(
               new HttpClientHandler()
               {
                   ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
               });

            var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpClient = httpClient });

            var client = new CustomerService.CustomerServiceClient(channel);

            var request = new CustomerIdRequest() { CustId = 1 };
            var reply = await client.GetCustomerByIdAsync(request);
            Console.WriteLine(reply.FullName);

            Console.ReadLine();
        }
    }
}
