using Prs_Test_Server.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using static System.Net.WebRequestMethods;

namespace Prs_Test_Server {

    public class AsyncHttpClient<T> {

        private HttpClient HttpClient = new HttpClient();
        public HttpResponseMessage HttpResponse { get; private set; }

        public async Task<T[]> GetAll(string url) {
            var TSerializer = new Serializer<T>();
            HttpResponse = await HttpClient.GetAsync(url);
            var resp = await HttpResponse.Content.ReadAsStringAsync();
            return TSerializer.ToCollection(resp);
        }
        public async Task<T> GetByPk(string url) {
            var TSerializer = new Serializer<T>();
            HttpResponse = await HttpClient.GetAsync(url);
            var resp = await HttpResponse.Content.ReadAsStringAsync();
            return TSerializer.ToInstance(resp);
        }
        public async Task<T> Create(string url, T t) {
            var TSerializer = new Serializer<T>();
            var instance = TSerializer.ToJson(t);
            var httpContent = new StringContent(instance, Encoding.UTF8, "application/json");
            HttpResponse = await HttpClient.PostAsync(url, httpContent);
            var resp = await HttpResponse.Content.ReadAsStringAsync();
            return TSerializer.ToInstance(resp);
        }
        public async Task Change(string url) {
            HttpResponse = await HttpClient.GetAsync(url);
            var resp = await HttpResponse.Content.ReadAsStringAsync();
            var TSerializer = new Serializer<T>();
            var t = TSerializer.ToInstance(resp);
            var json = TSerializer.ToJson(t);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponse = await HttpClient.PutAsync(url, httpContent);
        }
        public async Task Remove(string url) {
            HttpResponse = await HttpClient.DeleteAsync(url);
        }
    }
}
