using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MODEL;

namespace DAL
{
    public class ProductAttrValueDAL : BaseDAL<ProductAttrValue>, IProductAttrValueDAL
    {
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID)
        {
            return entities.ProductAttrValue.Where(x => x.ProductAttrKeyID == attrKeyID).ToList();
        }

    }
}
