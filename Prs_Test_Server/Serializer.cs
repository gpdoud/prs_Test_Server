using Prs_Test_Server.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prs_Test_Server {
    
    public class Serializer<T> {

        JsonSerializerOptions options = new JsonSerializerOptions() {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public string ToJson(T t) {
            var json = JsonSerializer.Serialize(t);
            return json;
        }

        public T ToInstance(string resp) {
            var inst = JsonSerializer.Deserialize<T>(resp, options);
            return inst;
        }
        public T[] ToCollection(string resp) {
            var arrTs = JsonSerializer.Deserialize<T[]>(resp, options);
            return arrTs;
        }
    }
}
