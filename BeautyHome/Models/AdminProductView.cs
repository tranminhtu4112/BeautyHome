using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class AdminProductView
    {
        public List<type_product> listtype { get; set; }
        public List<furniture> listfur { get; set; }
        public List<product> listProduct { get; set; }
        public List<ProductView> listProductViews { get; set; }
        public List<AdminOrder> listAdminOrder { get; set; }
        public Statistical statistical { get; set; }
    }
}