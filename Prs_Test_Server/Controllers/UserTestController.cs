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
        private HttpClient http = new HttpClient();
        private List<User> users = new List<User>();
        AsyncHttpClient<User> ahttp = new AsyncHttpClient<User>();

        public async Task TestUserList() {
            var url = urlfmt.Url;
            var arrUsers = await ahttp.GetAll(url);
            users.AddRange(arrUsers);
            Console.WriteLine($"Test User List:");
            Console.WriteLine($" - Rows: {users.Count}");
        }

        public async Task TestUserByPK() {
            var userId = users.FirstOrDefault().Id.ToString();
            urlfmt.Values = userId;
            var url = urlfmt.Url;
            var user = await ahttp.GetByPk(url);
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
            var newuser = await ahttp.Create(url, user);
            Console.WriteLine($"Test User Insert:");
            Console.WriteLine($" - Username: {newuser.Username}");
        }
        public async Task TestUserUpdate() {
            var userId = users.FirstOrDefault().Id.ToString();
            urlfmt.Values = userId;
            var url = urlfmt.Url;
            await ahttp.Change(url);
            Console.WriteLine($"Test User Update:");
            Console.WriteLine($" - StatusCode: {ahttp.HttpResponse.StatusCode}");
        }
        public async Task TestUserDelete() {
            var userId = users.FirstOrDefault().Id.ToString();
            urlfmt.Values = userId;
            var url = urlfmt.Url;
            await ahttp.Remove(url);
            Console.WriteLine($"Test User Delete:");
            Console.WriteLine($" - StatusCode: {ahttp.HttpResponse.StatusCode}");
        }

        public UserTestController() {
            urlfmt = new UrlFormatter {
                Https = false,
                Server = "localhost",
                Port = 5000,
                Controller = "api/users"
            };
        }
    }
}
