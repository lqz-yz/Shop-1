using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ProductDetailVModel
    {
        public Product Product { get; set; }
        public List<ProductSku> Skus { get; set; }
        public List<ProductAttr> Attrs { get; set; }
    }
}