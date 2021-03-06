using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Prs_Test_Server.Models;

namespace Prs_Test_Server.Controllers {

    public class UserTestController {

        private UrlFormatter urlfmt = null;
        private HttpClient http = null;
        private List<User> users = new List<User>();

        public async Task TestUserList() {
            var url = urlfmt.Url;
            var httpResp = await http.GetAsync(url);
            var resp = await httpResp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var arrUsers = JsonSerializer.Deserialize<User[]>(resp, options);
            users.AddRange(arrUsers);
            Console.WriteLine($"Test User List:");
            Console.WriteLine($" - Rows: {users.Count}");
        }

        public async Task TestUserByPK() {
            urlfmt.Values = "1";
            var url = urlfmt.Url;
            var httpResp = await http.GetAsync(url);
            var resp = await httpResp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var user = JsonSerializer.Deserialize<User>(resp, options);
            Console.WriteLine($"Test User by primary key:");
            Console.WriteLine($" - Primary key: {urlfmt.Values} {(user != null ? "found" : "not found")}");
        }

        public async Task TestUserInsert() {
            var url = urlfmt.Url;
            var user = new User {
                Id = 0, Username = "**PRSTESTUSER**", Password = "TEST",
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
        public async Task TestUserUpdate() {
            urlfmt.Values = "1";
            var url = urlfmt.Url;
            var httpResp = await http.GetAsync(url);
            var resp = await httpResp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            var user = JsonSerializer.Deserialize<User>(resp, options);
            user.Password = "***PASSWORD**";
            var json = JsonSerializer.Serialize(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpResp = await http.PutAsync(url, httpContent);
            Console.WriteLine($"Test User Update:");
            Console.WriteLine($" - StatusCode: {httpResp.StatusCode}");
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
