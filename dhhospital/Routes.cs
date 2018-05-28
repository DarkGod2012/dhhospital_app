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
    class Routes
    {   
        public string Route { get; set; }
        public Routes()
        {
             Route = "http://192.168.1.111:9999/api/BenhNhan/";
        }

      

        public Routes(string r)
        {
            this.Route = r;
        }
        
            
    }
}