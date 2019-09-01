namespace Shadow.Tool.Logger
{
    public class TextLoggerPropertyOptions
    {
        /// <summary>
        /// 是否记录请求头
        /// </summary>
        public bool HasRequestHeaders { get; set; }

        /// <summary>
        /// 是否记录响应头
        /// </summary>
        public bool HasResponseHeaders { get; set; }
    }
}
