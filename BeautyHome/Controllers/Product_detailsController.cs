using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Product_detailsController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Product_details
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order(String productId, String fullname, String count, String address, String phone, String email)
        {
            long userId = long.Parse(Session["userid"].ToString());

            /*String SQLInsert = "INSERT INTO HOADON(HOADON.MAHOADON, HOADON.MANHANVIEN, HOADON.TENKHACHHANG, HOADON.NGAYLAPDON, HOADON.GIA) " +
            "VALUES(@MAHOADON, @MANHANVIEN, @TENKHACHHANG, @NGAYLAPDON, @GIA)";
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(SQLInsert, connection);
                sqlCommand.Parameters.AddWithValue("MAHOADON", dtoHoaDon.maHoaDon);
                sqlCommand.Parameters.AddWithValue("MANHANVIEN", dtoHoaDon.maNhanVien);
                sqlCommand.Parameters.AddWithValue("TENKHACHHANG", dtoHoaDon.tenKhachHang);
                sqlCommand.Parameters.AddWithValue("NGAYLAPDON", dtoHoaDon.ngayLapDon);
                sqlCommand.Parameters.AddWithValue("GIA", dtoHoaDon.gia);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }*/
            return View();
        }
    }
}