using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class TypeProductView
    {
        public List<type_product> listtype { get; set; }
        public List<furniture> listfur { get; set; }
        public List<product> listProduct { get; set; }
        public List<ProductView> listProductViews { get; set; }
        public List<OrderView> listOrderViews { get; set; }
        public List<ProductView> listGioHang { get; set; }
        public List<UserComment> listUserComment { get; set; }
        public List<DetailsOrder> listDetailsOrder { get; set; }
    }
}