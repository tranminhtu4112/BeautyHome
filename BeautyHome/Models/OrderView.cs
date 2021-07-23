using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class OrderView
    {
        public long orderId { get; set; }

        public String dateOrder { get; set; }
        public int status { get; set; }
        public double totalPrice { get; set; }
    }
}