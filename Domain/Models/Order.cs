using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Order : Entity
    {
        public List<Guid> ProductId { get; set; }
        
        
        public Product Product { get; set; }

        ///<summary>
        /// Количество товара
        ///</summary>
        public int QuantityGoods { get; set; }

        public int Price { get; set; }
    }
}