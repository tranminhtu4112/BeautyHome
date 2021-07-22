using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHome.Models
{
    public class UserComment
    {
        public long comment_puduct_id { get; set; }
        public long product_id { get; set; }
        public long user_id { get; set; }
        public string txt_comment { get; set; }
        public string fullname { get; set; }
    }
}