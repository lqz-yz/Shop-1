using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MODEL;

namespace DAL
{
    public class ProductAttrKeyDAL : BaseDAL<ProductAttrKey>, IProductAttrKeyDAL
    {
        public List<ProductAttrKey> GetByCategoryID(int categoryId)
        {
            return entities.ProductAttrKey.Where(x => x.ProductCategoryID == categoryId).ToList();
        }
    }
}
