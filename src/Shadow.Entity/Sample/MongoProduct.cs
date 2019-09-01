using System;

namespace Shadow.Entity.Sample
{
    public class MongoProduct : ZZH.MongoDB.StandardService.Entities.MongoDbEntity 
    {
        public string NO { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public DateTime InBound { get; set; }

        public string Remark { get; set; }

        public bool IsDeleted { get; set; }
    }
}
