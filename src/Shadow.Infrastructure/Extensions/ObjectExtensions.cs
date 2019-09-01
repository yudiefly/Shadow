using Newtonsoft.Json;

namespace Shadow.Infrastructure.Extensions
{
    /// <summary>
    /// Object 对象扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将对象转换为 Json 数据
        /// </summary>
        /// <param name="obj">要转换的对</param>
        /// <param name="settings">序列化设置选项</param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerSettings settings = null)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string)
            {
                return (string)obj;
            }

            return JsonConvert.SerializeObject(obj, settings ?? new JsonSerializerSettings());
        }
    }
}
