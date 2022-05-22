namespace Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsStock { get; set; } //в наличии
        public int Count { get; set; }
    }
}