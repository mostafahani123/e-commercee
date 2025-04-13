namespace e_commercee.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string UserId { get; set; }

    }
}
