using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Client : Entity
    {
        public List<Guid> OrderId { get; set; }
        public string Name { get; set; }
    }
}