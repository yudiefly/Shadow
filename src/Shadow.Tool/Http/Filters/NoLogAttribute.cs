using System;

namespace Shadow.Tool.Http.Filters
{
    /// <summary>
    /// 表示不会使用 <see cref="LogAttribute"/> 进行日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NoLogAttribute : Attribute
    {
    }
}
