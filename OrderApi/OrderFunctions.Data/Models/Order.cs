using System;
using System.Collections.Generic;

namespace OrderFunctions.Data.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsInitial { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool? IsReviewed { get; set; }
    }
}
