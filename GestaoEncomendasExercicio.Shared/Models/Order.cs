namespace GestaoEncomendasExercicio.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } // Pendente, Em preparação, Despachada, Entregue
        public DateTime CreatedAt { get; set; }
    }
}