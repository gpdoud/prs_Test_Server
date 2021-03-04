using System;
namespace Prs_Test_Server.Models {

    public class Request {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; } = "NEW";
        public decimal Total { get; set; }

        public Request() {
        }
    }
}
