using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace dhhospital
{
    public class BenhNhan
    {
        public string mabn { get; set; }
        public string ten { get; set; }
        public string holot { get; set; }
        public string ngaysinh { get; set; }
        public int gioitinh { get; set; }

        public BenhNhan()
        {

        }

        public BenhNhan(string mabn, string holot, string ten, string ngaysinh, int gioitinh)
        {
            this.mabn = mabn;
            this.holot = holot;
            this.ten = ten;
            this.ngaysinh = ngaysinh;
            this.gioitinh = gioitinh;
        }



    }
}