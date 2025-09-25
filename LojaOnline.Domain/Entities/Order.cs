namespace LojaOnline.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public List<OrderItem> Itens { get; set; } = new();
    }
}
