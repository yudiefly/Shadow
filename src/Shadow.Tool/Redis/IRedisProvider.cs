using ServiceStack.Redis;

namespace Shadow.Tool.Redis
{
    public interface IRedisProvider
    {
        IRedisClientsManager CreateRedisClientsManager();
    }
}
