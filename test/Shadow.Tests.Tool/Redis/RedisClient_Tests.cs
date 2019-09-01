using ServiceStack.Redis;
using Shadow.Tool.Redis;
using System;
using Xunit;

namespace Shadow.Tests.Tool.Redis
{
    public class RedisClient_Tests
    {
        private readonly IRedisClientsManager _redisClientsManager;
        private readonly Microsoft.Extensions.Logging.ILoggerFactory loggerFactory;

        public RedisClient_Tests()
        {
            loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            _redisClientsManager = TestRedisClient.CreateRedisClientsManager();

        }

        [Fact]
        public void Shoud_Get_Test()
        {
            var manualConf = new RedisManualConfig { ManualConnectionTimeout = 300 };
            using (var client = new RedisCache(_redisClientsManager, null, manualConf, loggerFactory))
            {
                var value = client.GetOrDefault<string>("a");

                Assert.True(value == "a1", value);
            }
        }

        [Fact]
        public void Should_Set_Test()
        {
            var manualConf = new RedisManualConfig { ManualConnectionTimeout = 1000 };
            using (var client = new RedisCache(_redisClientsManager, null, manualConf, loggerFactory))
            {
                client.Set("key02", new CacheModel { NO = 1, Name = "key02", Date = DateTime.Now, IsDelete = false });

                var value = client.GetOrDefault<CacheModel>("key02");

                Assert.NotNull(value);
                Assert.True(value.NO == 1);
            }
        }

        [Fact]
        public void Should_Clear_Test()
        {
            var manualConf = new RedisManualConfig { ManualConnectionTimeout = 1000 };
            using (var client = new RedisCache(_redisClientsManager, null, manualConf, loggerFactory))
            {
                client.Clear();
            }
        }
    }

    internal class CacheModel
    {
        public int NO { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public bool IsDelete { get; set; }
    }
}
