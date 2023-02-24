namespace OrderFunctions.Models
{
    public class OrderDto
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public bool? IsApproved { get; set; }
    }
}
