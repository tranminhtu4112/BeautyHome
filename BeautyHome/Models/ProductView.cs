using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class ProductView
    {
        public long productId { get; set; }
        public string name { get; set; }
        public string descriptionDetails { get; set; }
        public string description { get; set; }
        public double evaluate { get; set; }
        public double amount { get; set; }
        public double price { get; set; }
        public string color { get; set; }
        public string url_image1 { get; set; }
        public string url_image2 { get; set; }
        public string url_image3 { get; set; }
        public ArrayList listIdUserCmt { get; set; }
        public long type_product_id { get; set; }
        public string nametype { get; set; }

    }
}