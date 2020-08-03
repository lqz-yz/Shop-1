using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMODEL
{
    public class OrderVModel
    {
        public MainOrder MainOrder { get; set; }
        public List<SubOrder> SubOrders { get; set; }
    }
}
