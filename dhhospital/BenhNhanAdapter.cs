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
using Java.IO;
using Newtonsoft.Json;

namespace dhhospital.Resources
{
    public class BenhNhanAdapter : BaseAdapter<BenhNhan>
    {
        private Context context;
        public List<BenhNhan> listbn = new List<BenhNhan>();
        public BenhNhanAdapter(Context context, List<BenhNhan> listbn)
        {
            this.context = context;
            this.listbn = listbn;
        }
        public override BenhNhan this[int position]
        {
            get { return listbn[position]; }
        }

        public override int Count
        {
            get
            {
                return listbn.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.listbenhnhan_layout, parent, false);
                var mabn = view.FindViewById<TextView>(Resource.Id.txtMaBenhNhan);
                var hoten = view.FindViewById<TextView>(Resource.Id.txtTenBenhNhan);
                var ngaysinh = view.FindViewById<TextView>(Resource.Id.txtNgaySinh);
                var gioitinh = view.FindViewById<TextView>(Resource.Id.txtGioiTinh);
                var btnSua = view.FindViewById<Button>(Resource.Id.btnSuaBNAdapter);
                var btnXoa = view.FindViewById<Button>(Resource.Id.btnXoaBNAdapter);
                view.Tag = new Cell(mabn, hoten, ngaysinh, gioitinh, btnSua, btnXoa);
            }
            Cell cell = (Cell)view.Tag;
            //Console.WriteLine("testhere " + listbn[0].mabn+"position"+position.ToString());
            cell.tv_mabn.Text = listbn[position].mabn;
            cell.tv_hoten.Text = listbn[position].holot + " " + listbn[position].ten;
            cell.tv_ngaysinh.Text = listbn[position].ngaysinh;
            cell.tv_gioitinh.Text = listbn[position].gioitinh.ToString();
            cell.btn_sua.Click += delegate
            {

                BenhNhan bn = listbn[position];
                try
                {
                    Intent intent = new Intent(context, typeof(SuaBenhNhan_Activity));
                    var bundle = new Bundle();
                    //    bundle.PutString("thongtinbenhnhan", bn);
                    //   intent.PutExtras(bundle);

                    intent.PutExtra("thongtinbenhnhan", JsonConvert.SerializeObject(bn));
                    intent.SetFlags(ActivityFlags.NewTask);
                    context.StartActivity(intent);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("loi adapter " + e.Message);
                }

            };
            cell.btn_xoa.Click += async delegate
            {
                HttpClient client = new HttpClient();
                Uri url = new Uri(new Routes().Route + listbn[position].mabn);
                var res = await client.DeleteAsync(url);
                var resMessage = await res.Content.ReadAsStringAsync();
                Toast.MakeText(Android.App.Application.Context, resMessage, ToastLength.Short).Show();
                listbn.RemoveAt(position);
                //this.Update(listbn);
                this.NotifyDataSetChanged();
                
            };
            return view;
        }

     /*   public void Add(BenhNhan bn)
        {
            listbn.Add(bn);
            this.NotifyDataSetChanged();
        }*/

        public void Update(List<BenhNhan> lbn)
        {
            listbn.Clear();
            listbn.AddRange(lbn);
            this.NotifyDataSetChanged();
        }

    }
}