using System;
using Xunit;

namespace Shadow.Tests.Infrastructure.Json
{
    public class JsonTests
    {
        [Fact]
        public void Should_Dynamic_Serialize_Test()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.Name = "Name";
            obj.Time = DateTime.Now;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            var jobj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            Assert.True(jobj.Name == "Name", json);
        }
    }
}
