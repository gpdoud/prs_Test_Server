using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Prs_Test_Server.Models;

namespace Prs_Test_Server.Controllers {

    public class UserTestController {

        private UrlFormatter urlfmt = null;
        private HttpClient http = null;

        public async Task TestUserList() {
            var url = urlfmt.Url;
            var httpResp = await http.GetAsync(url);
            var resp = await httpResp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<User[]>(resp, options);
            Console.WriteLine($"Test User List:");
            Console.WriteLine($" - Rows: {users.Length}");
        }

        public async Task TestUserInsert() {
            var url = urlfmt.Url;
            var user = new User {
                Id = 0, Username = "PRS TEST USER", Password = "TEST",
                Firstname = "Test Firstname", Lastname = "Test Lastname",
                PhoneNumber = "Test Phone", Email = "Test Email",
                IsReviewer = true, IsAdmin = false
            };

            var json = JsonSerializer.Serialize(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResp = await http.PostAsync(url, httpContent);
            var resp = await httpResp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var newuser = JsonSerializer.Deserialize<User>(resp, options);
            Console.WriteLine($"Test User Insert:");
            Console.WriteLine($" - Username: {newuser.Username}");
        }

        public UserTestController() {
            urlfmt = new UrlFormatter {
                Https = false,
                Server = "localhost",
                Port = 5000,
                Controller = "api/users"
            };
            http = new HttpClient();
        }
    }
}
