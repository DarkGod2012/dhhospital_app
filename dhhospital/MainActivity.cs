using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using dhhospital.Resources;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Android.Content;

namespace dhhospital
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btn_thembn;
        Button btn_thongke;
        ListView lvbenhnhan { get; set; }
        BenhNhanAdapter benhNhanAdapter { get; set; }
        public BenhNhanAdapter GetbenhNhanAdapter()
        {
            return benhNhanAdapter;
        }

        List<BenhNhan> listBenhNhan = new List<BenhNhan>();
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            lvbenhnhan = FindViewById<ListView>(Resource.Id.listView1);
            listBenhNhan = await LayDanhSachBenhNhan();
            benhNhanAdapter = new BenhNhanAdapter(this, listBenhNhan);
            lvbenhnhan.Adapter = benhNhanAdapter;

            btn_thembn = FindViewById<Button>(Resource.Id.btn_gdthembn);
            btn_thembn.Click += delegate {
                Intent intent = new Intent(this, typeof(ThemBenhNhan_Activity));

                StartActivity(intent);
            };
            btn_thongke = FindViewById<Button>(Resource.Id.btn_thongke);
            btn_thongke.Click += delegate {
                Intent intent = new Intent(this, typeof(ThongKeBenhNhan_Activity));

                StartActivity(intent);
            };

            

          
        }

        public async Task<List<BenhNhan>> LayDanhSachBenhNhan()
        {
            var url = new Uri(new Routes().Route);
            var client = new HttpClient();

            List<BenhNhan> listkq = new List<BenhNhan>();
            try
            {
                var res = await client.GetAsync(url);
                var content = await res.Content.ReadAsStringAsync();

                content = content.Replace(@"\", string.Empty);
               content = content.Trim().Substring(1, (content.Length) - 2);
                listkq = JsonConvert.DeserializeObject<List<BenhNhan>>(content);
    


            }
            catch (Exception c)
            {
                Toast.MakeText(Android.App.Application.Context, c.Message, ToastLength.Short);
                Console.WriteLine("lỗi " + c.Message);

            }
            return listkq;

        }

        protected override async void OnResume()
        {
            base.OnResume();
            listBenhNhan.Clear();
            listBenhNhan= await LayDanhSachBenhNhan();
             benhNhanAdapter = new BenhNhanAdapter(this, listBenhNhan);
             lvbenhnhan.Adapter = benhNhanAdapter;
           // RunOnUiThread(()=>benhNhanAdapter.NotifyDataSetChanged());
        }

    }
}

