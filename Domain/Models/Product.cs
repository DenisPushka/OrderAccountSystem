using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        ///<summary>
        /// Наличие товара
        ///</summary>
        public bool IsStock { get; set; } 
        public int Count { get; set; }
    }
}