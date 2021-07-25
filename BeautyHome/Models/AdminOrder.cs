using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class AdminOrder
    {
        public long orderId { get; set; }
        public long userId { get; set; }
        public String fullName { get; set; }
        public String dateOrder { get; set; }
        public String address { get; set; }
        public String phone { get; set; }
        public double totalPrice { get; set; }
        public int status { get; set; }
    }
}