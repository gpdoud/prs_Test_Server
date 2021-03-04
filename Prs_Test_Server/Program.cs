using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Prs_Test_Server.Models;

namespace Prs_Test_Server {

    class Program {

        IDictionary<string, string> _args = null;

        private async Task Run(string[] args) {
            _args = Args.Parse(args);
            var cmd = _args["cmd"];
            switch(cmd) {
                case "test-all":
                    await TestUserList();
                    break;
                default:
                    NotImplemented();
                    break;
            }
        }

        private async Task TestUserList() {
            //var url = "http://doudsystems.com/bcms/dsi/configs/author";
            var urlfmt = new UrlFormatter();
            urlfmt.Server = "doudsystems.com";
            urlfmt.Folder = "bcms";
            urlfmt.Controller = "dsi/configs";
            urlfmt.Values = "author";
            var http = new HttpClient();
            var resp = await http.GetStringAsync(urlfmt.Url);
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var config = JsonSerializer.Deserialize<BcmsConfig>(resp, options);
            Console.WriteLine($"{config}");
        }

        private void NotImplemented() {
            Console.WriteLine("NotImplemented()");
        }

        public static async Task Main(string[] args) {
            await (new Program()).Run(args);
        }
    }
}
