using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace dhhospital
{
    [Activity(Label = "ThongKeBenhNhan_Activity")]
    public class ThongKeBenhNhan_Activity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TextView tvslnam, tvslnu, tvslkhac;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.thongkebenhnhan_layout);
            // Create your application here
            tvslnam = FindViewById<TextView>(Resource.Id.tvSoLuongNam);
            tvslnu = FindViewById<TextView>(Resource.Id.tvSoLuongNu);
            tvslkhac = FindViewById<TextView>(Resource.Id.tvSoLuongKhac);
   
            var url = new Uri(new Routes("http://192.168.1.111:9999/api/ThongKeBenhNhan/gioitinh/").Route);
            var client = new HttpClient();
            Console.WriteLine("url tk " + url.ToString());
            try
            {
                var res = await client.GetAsync(url);
                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine("typeof " + content.GetType());
                string listsl = JsonConvert.DeserializeObject<string>(content);
                Console.WriteLine("so luong "+listsl);
                tvslnam.Text = listsl[5].ToString();
                tvslnu.Text = listsl[1].ToString();
                tvslkhac.Text = listsl[3].ToString();

            }
            catch (Exception c)
            {
                Toast.MakeText(Android.App.Application.Context, c.Message, ToastLength.Short);
                Console.WriteLine("loi thong ke " + c.Message);

            }

        }   
    }
}