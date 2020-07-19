using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using BLL;
using IBLL;
using MODEL;

namespace API.Controllers
{
    public class SearchController : BaseApiController<Product, IProductBLL>
    {
        //public override IProductBLL Bll { get { return new ProductBLL(); } }

        //public ResponsMessage<List<Product>> Get(string query)
        //{
        //    try
        //    {
        //        var Data = Bll.Search(x => x.KeyWords.Contains(query));
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求成功",
        //            Data = Data,
        //            Total = Data.Count
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return Error<List<Product>>("在搜索商品名称时出现异常");
        //    }
        //}
    }
}
