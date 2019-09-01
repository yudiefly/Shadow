namespace Shadow.Tool.Redis
{
    public class RedisManualConfig
    {
        /// <summary>
        /// 手动地设置连接服务器超时时长，milliseconds
        /// 若设置该值会覆盖 host 中配置的时间或默认时长(20s)。若设置的超时时长值超过组件的配置时长或默认时长，则不会生效。
        /// </summary>
        public int? ManualConnectionTimeout { get; set; }
    }
}
