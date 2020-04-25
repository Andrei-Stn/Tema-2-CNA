using System;
using System.Globalization;
using Grpc.Net.Client;
using Tema2CNA.Protos;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Zodiac.ZodiacClient(channel);

            var request = new DateRequest();
            request.Birthday = ValidateInput();
            var response = client.GetZodiacSign(request);
            Console.WriteLine($"Zodia este {response.Sign}");

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static string ValidateInput()
        {
            Console.WriteLine("Enter your birthdate:");
            var input = Console.ReadLine();

            while (true)
            {
                DateTime dateTime;
                if (!DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out dateTime))
                {
                    Console.WriteLine("Invalid date input.");
                    input = Console.ReadLine();
                }
                return input;
            }
        }
    }
}
