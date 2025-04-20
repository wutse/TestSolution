using StackExchange.Redis;

namespace TestRedis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            try
            {
                ConfigurationOptions options = ConfigurationOptions.Parse("127.0.0.1:8080,user=mdqry,password=mdqry");

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
                IDatabase db = redis.GetDatabase();

            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            Console.ReadLine();
        }
    }
}
