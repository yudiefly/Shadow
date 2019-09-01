using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shadow.WebApi.Models
{
    /// <summary>
    /// 创建 Person 对象的 ViewModel
    /// </summary>
    public class ProductCreateViewModel
    {
        public string NO { get; set; }

        [Required]
        public string Name { get; set; }

        public double Weight { get; set; }

        public List<ProductItemViewModel> Items { get; set; }
    }

    public class ProductItemViewModel
    {

    }
}
