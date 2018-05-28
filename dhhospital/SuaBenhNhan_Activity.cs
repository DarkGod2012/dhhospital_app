using System;
using System.Collections.Generic;
using System.Globalization;
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
    [Activity(Label = "SuaBenhNhan_Activity")]
    public class SuaBenhNhan_Activity : Activity
    {
        Button btn_sua, btn_quayve;
        EditText ed_mabn, ed_holot, ed_ten, ed_ngaysinh;
        RadioButton rd_gtnam, rd_gtnu, rd_gtkhac;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.suabenhnhan_activity_layout1);
            // Create your application here
            //BenhNhan bn = JsonConvert.DeserializeObject<BenhNhan>(Intent.GetStringExtra("thongtinbenhnhan"));
            // Console.WriteLine("ahihi " + bn.mabn);
            btn_sua = FindViewById<Button>(Resource.Id.btnSuaBN);
            btn_quayve = FindViewById<Button>(Resource.Id.btnQuayVeSua);
            ed_mabn = FindViewById<EditText>(Resource.Id.edMabnSua);
            ed_holot = FindViewById<EditText>(Resource.Id.edHolotSua);
            ed_ten = FindViewById<EditText>(Resource.Id.edTenSua);
            ed_ngaysinh = FindViewById<EditText>(Resource.Id.edNgaySinhSua);
           
            rd_gtnam = FindViewById<RadioButton>(Resource.Id.rdNamSua);
            rd_gtnu = FindViewById<RadioButton>(Resource.Id.rdNuSua);
            rd_gtkhac = FindViewById<RadioButton>(Resource.Id.rdKhacSua);

            BenhNhan bn = JsonConvert.DeserializeObject<BenhNhan>(Intent.GetStringExtra("thongtinbenhnhan"));
             ed_mabn.Text = bn.mabn;
             ed_holot.Text = bn.holot;
             ed_ten.Text = bn.ten;
             ed_ngaysinh.Text = bn.ngaysinh;
             switch (bn.gioitinh)
             {
                 case 0:
                     rd_gtnu.Checked = true;
                     break;
                 case 1:
                     rd_gtnam.Checked = true;
                     break;
                 default:
                     rd_gtkhac.Checked = true;
                     break;
             }

            DateTime ngay = DateTime.ParseExact(ed_ngaysinh.Text, "dd/MM/yyyy", null);
            
            ed_ngaysinh.Click += delegate
            {
                DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSet, ngay.Year, ngay.Month - 1, ngay.Day);
                datePickerDialog.DatePicker.MinDate = ngay.Millisecond;
                datePickerDialog.Show();
            };
            btn_quayve.Click += delegate
            {
                OnBackPressed();
            };
            btn_sua.Click += async delegate

            {
                
                    var gioitinh = 1;
                    if (rd_gtnu.Checked)
                    {
                        gioitinh = 0;
                    }
                    else if (rd_gtkhac.Checked)
                    {
                        gioitinh = 2;
                    }
                    if (ed_mabn.Text.Equals("") || ed_holot.Equals("") || ed_ten.Equals("") || ed_ngaysinh.Equals(""))
                    {
                        Toast.MakeText(this, "Chưa nhập đủ thông tin", ToastLength.Short).Show();
                    }
                    BenhNhan benhnhan = new BenhNhan(ed_mabn.Text, ed_holot.Text, ed_ten.Text, ed_ngaysinh.Text, gioitinh);
                    await SuaBenhNhan(benhnhan);
                    
                  
                //    MainActivity main = new MainActivity();

                   // main.GetbenhNhanAdapter().NotifyDataSetChanged();

              
            };
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ed_ngaysinh.Text = e.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public async Task SuaBenhNhan(BenhNhan benhnhan)
        {

            Uri url = new Uri(new Routes().Route + benhnhan.mabn);
            Console.WriteLine("url sua: "+url);
            HttpClient client = new HttpClient();
            try
            {
                var data = JsonConvert.SerializeObject(benhnhan);
                Console.WriteLine(data);
               
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var res = await client.PutAsync(url, content);
                var resMessage = await res.Content.ReadAsStringAsync();
              
                Console.WriteLine(resMessage);
                if (res.IsSuccessStatusCode)
                {
                    Toast.MakeText(this, "thành công", ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }

        }
    }
}