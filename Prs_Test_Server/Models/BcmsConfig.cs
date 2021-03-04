using System;
namespace Prs_Test_Server.Models {

    public class BcmsConfig {

        public string KeyValue { get; set; }
        public string DataValue { get; set; }
        public bool System { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime?  Updated { get; set; }

        public override string ToString() {
            return $"KV:{KeyValue}, DV:{DataValue}";
        }

        public BcmsConfig() {}
    }
}
