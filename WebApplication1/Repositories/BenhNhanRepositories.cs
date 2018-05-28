using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Npgsql;
using NpgsqlTypes;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BenhNhanRepositories
    {
        public static String ThemBenhNhan(BenhNhan benhnhan)
        {
            string ketnoi = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            NpgsqlConnection conn = new NpgsqlConnection(ketnoi);
            DateTime ngaysinhtemp;
            string sql = "INSERT INTO current.dmbenhnhan VALUES(@mabn,@holot,@ten,@ngaysinh,@gioitinh)";
            try
            {
                ngaysinhtemp = DateTime.ParseExact(benhnhan.ngaysinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return "Ngày sinh không hợp lệ";
            }
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@mabn", NpgsqlDbType.Varchar).Value = benhnhan.mabn;
                cmd.Parameters.Add("@holot", NpgsqlDbType.Varchar).Value = benhnhan.holot;
                cmd.Parameters.Add("@ten", NpgsqlDbType.Varchar).Value = benhnhan.ten;
                cmd.Parameters.Add("@ngaysinh", NpgsqlDbType.Date).Value = ngaysinhtemp;
                cmd.Parameters.Add("@gioitinh", NpgsqlDbType.Integer).Value = benhnhan.gioitinh;
                cmd.ExecuteNonQuery();

                conn.Close();
                return "thành công";

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        /*public static BenhNhan ThongTinBenhNhan(String mabn)
        {
            string ketnoi = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            NpgsqlConnection conn = new NpgsqlConnection(ketnoi);
            string sql = "SELECT * FROM current.dmbenhnhan WHERE mabn=@mabn";
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteReader();

            }
            catch(Exception e)
            {
                return e.Message;
            }

        }*/

        public static String SuaBenhNhan(BenhNhan benhnhan)
        {
            string ketnoi = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            NpgsqlConnection conn = new NpgsqlConnection(ketnoi);
            String sql = "UPDATE current.dmbenhnhan SET holot=@holot,ten=@ten,ngaysinh=@ngaysinh,gioitinh=@gioitinh WHERE mabn=@mabn";
            DateTime ngaysinhtemp;
            try {
                ngaysinhtemp = DateTime.ParseExact(benhnhan.ngaysinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch(Exception)
            {
                return "Ngày sinh không hợp lệ";
            }
           
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@mabn", NpgsqlDbType.Varchar).Value = benhnhan.mabn;
                cmd.Parameters.Add("@holot", NpgsqlDbType.Varchar).Value = benhnhan.holot;
                cmd.Parameters.Add("@ten", NpgsqlDbType.Varchar).Value = benhnhan.ten;
                cmd.Parameters.Add("@ngaysinh", NpgsqlDbType.Date).Value = ngaysinhtemp;
                cmd.Parameters.Add("@gioitinh", NpgsqlDbType.Integer).Value = benhnhan.gioitinh;
                cmd.ExecuteNonQuery();

                conn.Close();
                return "thành công";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }


        public static String XoaBenhNhan(String mabn)
        {
            string ketnoi = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            NpgsqlConnection conn = new NpgsqlConnection(ketnoi);
            String sql = "DELETE FROM current.dmbenhnhan WHERE mabn=@mabn";
          

            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@mabn", NpgsqlDbType.Varchar).Value =mabn;
                cmd.ExecuteNonQuery();

                conn.Close();
                return "Xoa thành công";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static List<BenhNhan> DanhSachBenhNhan()
        {
            var connectionstring = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            var query = "SELECT * FROM current.dmbenhnhan";

            List<BenhNhan> list = new List<BenhNhan>();
            NpgsqlConnection conn = new NpgsqlConnection(connectionstring);

            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn); ;
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime ngay = reader.GetDateTime(3);
                 
                    string ngaysinh = ngay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    
                    var temp3 = reader.GetDecimal(4);
                    int gt = Decimal.ToInt32(temp3);
                    list.Add(new BenhNhan(reader.GetString(0), reader.GetString(1), reader.GetString(2), ngaysinh, gt));
                }
                conn.Close();
                Console.WriteLine("Thành công");
              
            }
            catch (Exception)
            {
                Console.WriteLine("Thất bại");
                
            }
            return list;
        }

        public static List<int> SoLuongBenhNhanTheoGioiTinh()
        {
            var connectionstring = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=imbatman;Database=svthuctap;";
            var query = "SELECT gt.gioitinh,  coalesce(COUNT(dm.gioitinh),0) as soluong FROM current.dmbenhnhan as dm RIGHT JOIN current.dmgioitinh as gt on dm.gioitinh = gt.gioitinh GROUP BY gt.gioitinh";

            List<int> list = new List<int>();
            NpgsqlConnection conn = new NpgsqlConnection(connectionstring);

            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn); ;
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
       
                    list.Add(reader.GetInt32(1));
                }
                conn.Close();
                Console.WriteLine("Thành công");

            }
            catch (Exception)
            {
                Console.WriteLine("Thất bại");

            }
            return list;
        }

    }
}