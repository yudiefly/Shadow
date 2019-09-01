using ServiceStack.Redis;

namespace Shadow.Tests.Tool.Redis
{
    public class TestRedisClient
    {
        public static IRedisClientsManager CreateRedisClientsManager()
        {
            return new RedisManagerPool("127.0.0.1:6379?connectTimeout=8000");
        }
    }
}
