using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class DetailsOrder
    {
        public long productId { get; set; }
        public String url_image1 { get; set; }
        public String name { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
    }
}