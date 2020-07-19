using System;
using MODEL;
using DAL;
using IBLL;
using System.Collections.Generic;
using IDAL;
using System.Linq;

namespace BLL
{
    public class ProductCategoryBLL : BaseBLL<ProductCategory, ProductCategoryDAL>, IProductCategoryBLL
    {
        public override int Add(ProductCategory model)
        {
            var tran = this.BeginTran();
            int result = 0;
            string path = "";
            try
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);
                result += SaveChange();
                if (model.PID == 0)
                {
                    path = model.ID.ToString();
                }
                else
                {
                    var parent = this.Search(x => x.ID == model.PID).First();
                    path = parent.Path + "," + model.ID;
                }
                model.Path = path;
                this.Update(model);
                result += this.SaveChange();
                tran.Commit();
            }
            catch (Exception)
            {
                result = 0;
                tran.Rollback();
            }
            return result;
        }

        public List<ProductCategory> GetSub(int id)
        {
            return dal.GetSub(id);
        }
        public override int Update(ProductCategory model)
        {
            string path = "";
            if (model.PID == 0)
            {
                path = model.ID.ToString();
            }
            else
            {
                var parent = this.Search(x => x.ID == model.PID).First();
                path = parent.Path + "," + model.ID;
            }
            model.Path = path;

            dal.Update(model);
            return SaveChange();
        }
    }
}
