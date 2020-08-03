using DAL;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class MainOrderBLL : BaseBLL<MainOrder, MainOrderDAL>, IMainOrderBLL
    {
    }
}
