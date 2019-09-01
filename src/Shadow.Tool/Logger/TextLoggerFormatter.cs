using Shadow.Infrastructure.Extensions;
using Shadow.Tool.Http;

namespace Shadow.Tool.Logger
{
    public class TextLoggerFormatter
    {
        public TextLoggerPropertyOptions TextPropertyOptions { get; set; }

        public static string DefaultLogMessageFormatter(string location, string message)
        {
            var msg = new
            {
                TraceId = HttpContextGlobal.CurrentTraceId,
                HttpContextGlobal.Current?.TraceIdentifier,
                Route = $"{HttpContextGlobal.Current?.Request.Path}{HttpContextGlobal.Current?.Request.QueryString}",
                Class = location,
                Log = message,
            };

            return msg.ToJson();
        }

        public string LogMessageFormatter(string location, string message)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.TraceId = HttpContextGlobal.CurrentTraceId;
            obj.TraceIdentifier = HttpContextGlobal.Current?.TraceIdentifier;
            obj.Route = $"{HttpContextGlobal.Current?.Request.Path}{HttpContextGlobal.Current?.Request.QueryString}";
            obj.Class = location;

            if (TextPropertyOptions != null)
            {
                if (TextPropertyOptions.HasRequestHeaders)
                {
                    obj.RequestHeaders = HttpContextGlobal.Current?.Request.Headers;
                }
                if (TextPropertyOptions.HasResponseHeaders)
                {
                    obj.ResponseHeaders = HttpContextGlobal.Current?.Response.Headers;
                }
            }

            obj.Log = message;

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
