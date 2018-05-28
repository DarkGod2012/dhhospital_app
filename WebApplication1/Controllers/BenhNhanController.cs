using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Processors;
using Newtonsoft.Json;

namespace Thuc_Tap_WebService.Controllers
{
    public class BenhNhaController : ApiController
    {
        

        [Route("api/BenhNhan/")]
        [HttpPost]
        public string ThemBenhNhan(BenhNhan benhnhan) {

            if (benhnhan == null)
            {

                return "Không có thông tin về bệnh nhân";
            }
            else
            {
               return  BenhNhanProcessor.ThemBenhNhan(benhnhan);

            }
        }

        [Route("api/BenhNhan/")]
        [HttpGet]
        public string HienThiBenhNhan()
        {
            List<BenhNhan> list = new List<BenhNhan>();
            list = BenhNhanProcessor.DanhSachBenhNhan();

            
            var convertedJson = JsonConvert.SerializeObject(list);
            return convertedJson;
        }

        [Route("api/ThongKeBenhNhan/gioitinh/")]
        [HttpGet]
        public string ThongKeBenhNhanTheoGioiTInh()
        {
            List<int> list = new List<int>();
            list = BenhNhanProcessor.ThongKeBenhNhanTheoGioiTinh();


            var convertedJson = JsonConvert.SerializeObject(list);
            return convertedJson;
        }

        [Route("api/BenhNhan/{mabn}")]
        [HttpPut]
        public string SuaBenhNhan(BenhNhan benhnhan)
        {
            if (benhnhan == null)
            {

                return "Không có thông tin về bệnh nhân";
            }
            else
            {
                
                return BenhNhanProcessor.SuaBenhNhan(benhnhan);

            }
        }

        [Route("api/BenhNhan/{mabn}")]
        [HttpDelete]
        public string XoaBenhNhan(string mabn)
        {
            if (mabn == null)
            {

                return "Không có thông tin về bệnh nhân";
            }
            else
            {
                return BenhNhanProcessor.XoaBenhNhan(mabn);

            }
        }



    }


}
