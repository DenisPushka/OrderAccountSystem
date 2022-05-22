using System;

namespace Domain.Models
{
    public class Client : Entity
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
    }
}