using StackExchange.Redis;

string RedisConnectionString = "localhost:6379";

ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);

string Channel = "customer-redis";

Console.WriteLine("Ouvindo o canal");

connection.GetSubscriber().Subscribe(Channel, (channel, message) => Console.WriteLine("Usuario recebido no canal mensagem: " + message));

Console.ReadLine();
