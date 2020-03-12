using Grpc.Net.Client;
using grpc_scratch.Proto;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Grpc.Core;

namespace gRPC_Client
{
    class Program
    {
        private const string Address = "https://localhost:5001";

        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress(Address);

            var client = new CustomerService.CustomerServiceClient(channel);

            var token = await Authenticate();

            Metadata? headers = new Metadata { { "Authorization", $"Bearer {token}" } };

            var request = new CustomerIdRequest() { CustId = 1 };
            var reply = await client.GetCustomerByIdAsync(request, headers);

            Console.WriteLine(reply.FullName);

            Console.ReadLine();
        }

        private static async Task<string> Authenticate()
        {
            Console.WriteLine($"Authenticating as {Environment.UserName}...");
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{Address}/generateJwtToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                Method = HttpMethod.Get,
                Version = new Version(2, 0)
            };
            var tokenResponse = await httpClient.SendAsync(request);
            tokenResponse.EnsureSuccessStatusCode();

            var token = await tokenResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Successfully authenticated.");

            return token;
        }
    }
}
