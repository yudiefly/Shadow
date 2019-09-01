using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Text;
using Xunit;

namespace Shadow.Tests.Tool.Redis
{
    public class RedisSentinelTests
    {
        [Fact]
        public void Can_Connect_Redis_Test()
        {
            var redisHosts = new string[]
            {
                "10.110.76.178:7000",  // master
                "10.110.76.178:7001",  // master
                "10.110.76.178:7002",  // master
                "10.110.76.178:7003",
                "10.110.76.178:7004",
                "10.110.76.178:7005",
            };
            var redisManager = new RedisManagerPool(redisHosts);  // redis 至少有一个 master 主键服务
            var redisClient = redisManager.GetClient();
            redisClient.Dispose();
        }

        [Fact]
        public void Can_Connect_Sentinel_Test()
        {
            var sentinelHosts = new string[]
            {
                "10.110.76.178:26379",
            };

            var sentinel = new RedisSentinel(sentinelHosts, "host6379")
            {
                OnFailover = manager =>
                {
                    "Redis Managers were Failed Over to new hosts".Print();
                },
                OnWorkerError = ex =>
                {
                    "Worker error: {0}".Print(ex);
                },
                OnSentinelMessageReceived = (channel, msg) =>
                {
                    "Received '{0}' on channel '{1}' from Sentinel".Print(channel, msg);
                },

                //HostFilter = host => $"redis123456@{host}"
                HostFilter = host => $"{host}?password=redis123456",
            };

            var redisManager = sentinel.Start();  // 尝试与哨兵服务器进行通信(检查服务器是否可用)
            var redisClient = redisManager.GetClient();  // 尝试连接服务器
            var foo2 = redisClient.Get<string>("foo2");
            redisClient.Dispose();

            Assert.True(foo2 == "f2");
        }
    }
}
