namespace Shadow.Tool.Redis
{
    public class RedisOptions
    {
        public string[] Hosts { get; set; }

        public int? MaxPoolSize { get; set; }

        public int? ManualConnectionTimeout { get; set; }
    }
}
