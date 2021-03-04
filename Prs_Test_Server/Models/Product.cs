using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prs_Test_Server.Models {

    public class Product {

        public int Id { get; set; }
        [StringLength(30), Required]
        public string PartNbr { get; set; }
        [StringLength(30), Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [StringLength(15)]
        public string Unit { get; set; } = "Each";
        public string PhotoPath { get; set; }

        public Product() {
        }
    }
}
