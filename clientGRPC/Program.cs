// See https://aka.ms/new-console-template for more information
using clientGRPC;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:5003");
var client = new DiscountProtoService.DiscountProtoServiceClient(channel);

var reply = await client.GetDiscountAsync(new GetDiscountRequest { ProductName = "IPhone X" });
Console.WriteLine("Greeting: " + reply.Amount );
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
