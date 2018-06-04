namespace Simulator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public int TransactionId { get; set; }

        [StringLength(36)]
        public string VendingMachineId { get; set; }

        [StringLength(255)]
        public string ItemName { get; set; }

        public int? ItemId { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? PurchasePrice { get; set; }

        public int? TransactionStatus { get; set; }

        public DateTime? TransactionDate { get; set; }
    }
}
