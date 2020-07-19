using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using MODEL;
using IDAL;
using DAL;

namespace BLL
{
    public class ProductAttrValueBLL : BaseBLL<ProductAttrValue,ProductAttrValueDAL>,IProductAttrValueBLL
    {
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID)
        {
            return dal.GetAllByAttrKeyID(attrKeyID);
        }
    }
}
