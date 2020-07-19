using API.Models;
using BLL;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ProductDetailController : BaseApiController<Product, IProductBLL>
    {
        public override IProductBLL BLL { get => new ProductBLL(); }
        [Route("api/ProductDetail/")]
        public ResponsMessage<ProductDetailVModel> GetProductDetail(int id)
        {
            try
            {
                var list = BLL.Search(x => x.ID == id);

                List<ProductSku> skus;
                List<ProductAttr> attrs;
                var product = BLL.GetOne(id, out skus, out attrs);
                var result = new ProductDetailVModel
                {
                    Product = product,
                    Attrs = attrs,
                    Skus = skus
                };
                return Success(result);
            }
            catch (Exception ex)
            {
                return Error<ProductDetailVModel>("在查询商品详情过程中出现异常"+ ex.Message);
            }
        }
    }
}
