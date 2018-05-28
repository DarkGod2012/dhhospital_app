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
    public class Cell : Java.Lang.Object
    {

        public TextView tv_mabn { get; set; }
        public TextView tv_hoten { get; set; }
        public TextView tv_ngaysinh { get; set; }
        public TextView tv_gioitinh { get; set; }
        public Button btn_sua { get; set; }
        public Button btn_xoa { get; set; }
        public Cell(TextView mabn, TextView hoten, TextView ngaysinh, TextView gioitinh, Button btn_sua, Button btn_xoa)
        {
            this.tv_mabn = mabn;
            this.tv_hoten = hoten;
            this.tv_ngaysinh = ngaysinh;
            this.tv_gioitinh = gioitinh;
            this.btn_sua = btn_sua;
            this.btn_xoa = btn_xoa;
        }

    }
}