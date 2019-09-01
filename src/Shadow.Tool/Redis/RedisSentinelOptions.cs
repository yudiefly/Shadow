namespace Shadow.Tool.Redis
{
    public class RedisSentinelOptions
    {
        public string[] Hosts { get; set; }

        public string MasterName { get; set; }

        public string HostFilter { get; set; }

        public int? ManualConnectionTimeout { get; set; }
    }
}
