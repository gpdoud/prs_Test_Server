using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Prs_Test_Server.Models;

namespace Prs_Test_Server.Controllers {

    public class VendorTestController {

        private UrlFormatter urlfmt = null;
        private HttpClient http = new HttpClient();
        private List<Vendor> vendors = new List<Vendor>();
        AsyncHttpClient<Vendor> ahttp = new AsyncHttpClient<Vendor>();

        public async Task TestList() {
            var url = urlfmt.Url;
            var arr = await ahttp.GetAll(url);
            vendors.AddRange(arr);
            Console.WriteLine($"Test Vendor List:");
            Console.WriteLine($" - Rows: {vendors.Count}");
        }
        public async Task TestInsert() {
            var url = urlfmt.Url;
            var instance = new Vendor {
                Id = 0, Code = "DSI", Name = "DSI",
                Address = "123 Any St.", City = "Cincinnati",
                State = "OH", Zip = "54321",
                PhoneNumber = "513-555-1212", Email = "info@dsi.com"
            };
            var newinstance = await ahttp.Create(url, instance);
            Console.WriteLine($"Test Vendor Insert:");
            Console.WriteLine($" - Code: {newinstance.Code}");
        }
        public VendorTestController() {
            urlfmt = new UrlFormatter {
                Https = false,
                Server = "localhost",
                Port = 5000,
                Controller = "api/vendors"
            };
        }
    }
}
