using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Repositories;
namespace WebApplication1.Processors
{
    public class BenhNhanProcessor
    {
        public static string ThemBenhNhan(BenhNhan benhnhan)
        {
           return  BenhNhanRepositories.ThemBenhNhan(benhnhan);
        }

        public static List<BenhNhan> DanhSachBenhNhan()
        {
            return BenhNhanRepositories.DanhSachBenhNhan();
        }

        public static string SuaBenhNhan(BenhNhan benhnhan)
        {
            return BenhNhanRepositories.SuaBenhNhan(benhnhan);
        }

        public static string XoaBenhNhan(string mabenhnhan)
        {
            return BenhNhanRepositories.XoaBenhNhan(mabenhnhan);
        }

        public static List<int> ThongKeBenhNhanTheoGioiTinh()
        {
            return BenhNhanRepositories.SoLuongBenhNhanTheoGioiTinh();
        }
    }
}