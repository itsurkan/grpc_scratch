using Grpc.Net.Client;
using grpc_scratch.Proto;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace gRPC_Client
{
    class Program
    {
        private ILoggerFactory _loggerFactory;
        static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();

                builder.SetMinimumLevel(LogLevel.Debug);
            });
            var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { LoggerFactory = loggerFactory });

            var client = new CustomerService.CustomerServiceClient(channel);

            var request = new CustomerIdRequest() { CustId = 1 };
            var reply = await client.GetCustomerByIdAsync(request);
            Console.WriteLine(reply.FullName);

            Console.ReadLine();
        }
    }
}
