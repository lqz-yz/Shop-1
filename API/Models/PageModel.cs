using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PageModel<T>
    {
        public int total { get; set; }
        public List<T> data { get; set; }
    }
}