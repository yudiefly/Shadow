using Shadow.Entity.Sample;
using System;
using ZZH.AutoMapper.Service;

namespace Shadow.ContractModel
{
    [Mapper(typeof(Product2))]
    public class Product2Model
    {
        public string ID { get; set; }

        public string NO { get; set; }

        [MapMember(nameof(Product2.Name))]
        public string ProductName { get; set; }

        public double Weight { get; set; }

        public DateTime InBound { get; set; }

        public string Remark { get; set; }

        public bool IsDeleted { get; set; }
    }
}
