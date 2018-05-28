using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace dhhospital
{
    [Activity(Label = "ThemBenhNhan_Activity")]
    public class ThemBenhNhan_Activity : Activity
    {
        Button btn_them, btn_quayve,btn_nhaplai;
        EditText ed_mabn, ed_holot, ed_ten, ed_ngaysinh;
        RadioButton rd_gtnam, rd_gtnu, rd_gtkhac;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.thembenhnhan_activity_layout);

            // Create your application here
            btn_them = FindViewById<Button>(Resource.Id.btnThemBN);
            btn_quayve = FindViewById<Button>(Resource.Id.btnQuayVe);
            btn_nhaplai = FindViewById<Button>(Resource.Id.btnNhapLai);
            ed_mabn = FindViewById<EditText>(Resource.Id.edMabn);
            ed_holot = FindViewById<EditText>(Resource.Id.edHolot);
            ed_ten = FindViewById<EditText>(Resource.Id.edTen);
            ed_ngaysinh = FindViewById<EditText>(Resource.Id.edNgaySinh);
            DateTime homnay = DateTime.Today;
            ed_ngaysinh.Click += delegate
            {
                DatePickerDialog datePickerDialog = new DatePickerDialog(this,OnDateSet, homnay.Year, homnay.Month-1,homnay.Day);
                datePickerDialog.DatePicker.MinDate = homnay.Millisecond;
                datePickerDialog.Show();
            };
            rd_gtnam = FindViewById<RadioButton>(Resource.Id.rdNam);
            rd_gtnu = FindViewById<RadioButton>(Resource.Id.rdNu);
            rd_gtkhac= FindViewById<RadioButton>(Resource.Id.rdKhac);
            btn_nhaplai.Click += delegate
            {
                ed_mabn.Text="";
                ed_holot.Text = "";
                ed_ten.Text = "";
                ed_ngaysinh.Text = "";
            };
            btn_quayve.Click += delegate
            {
                OnBackPressed();
            };
            btn_them.Click += async delegate
            {
                try
                {
                    var mabn = ed_mabn.Text;
                    var holot = ed_holot.Text;
                    var ten = ed_ten.Text;
                    var ngaysinh = ed_ngaysinh.Text;
                    
                    var gioitinh = 1;
                    if (rd_gtnu.Checked)
                    {
                        gioitinh = 0;
                    }
                    else if (rd_gtkhac.Checked)
                    {
                        gioitinh = 2;
                    }
                    if (mabn.Equals("") || holot.Equals("") || ten.Equals("") || ngaysinh.Equals(""))
                    {
                        Toast.MakeText(this, "Chưa nhập đủ thông tin", ToastLength.Short).Show();
                    }
                    else
                    {
                        BenhNhan benhnhan = new BenhNhan(mabn, holot, ten, ngaysinh, gioitinh);
                        await ThemBenhNhan(benhnhan);

                     
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Lỗi khi thêm benhnhan " + e.Message);
                }

            };


        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ed_ngaysinh.Text = e.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }


        public async Task ThemBenhNhan(BenhNhan benhnhan)
        {

            Uri url = new Uri(new Routes().Route);
            HttpClient client = new HttpClient();
            try
            {
                var data = JsonConvert.SerializeObject(benhnhan);
                Console.WriteLine(data);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var res = await client.PostAsync(url, content);
                var resMessage = await res.Content.ReadAsStringAsync();
                Toast.MakeText(Android.App.Application.Context, resMessage, ToastLength.Short).Show();
                Console.WriteLine(resMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}