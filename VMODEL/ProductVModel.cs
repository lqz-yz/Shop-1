using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMODEL
{
    public class ProductVModel
    {
        public Product Product { get; set; }
        //public List<ProductAttrKeyVModel> Attrs { get; set; }
        public List<ProductAttr> Attrs { get; set; }
        public List<ProductSku> ProductSkus { get; set; }
    }
}
