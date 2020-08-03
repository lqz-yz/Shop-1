using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMODEL;

namespace IBLL
{
    public interface IMainOrderBLL : IBaseBLL<MainOrder>
    {
        MainOrder GetOne(string id, out List<SubOrder> subOrders);
        string Add(string token, OrderVModel orderVModel);
    }
}
