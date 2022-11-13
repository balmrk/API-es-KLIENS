using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NorthwindApiDemo.Domain
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public short ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        [JsonIgnore]
        public short? SupplierId { get; set; }
        public short? CategoryId { get; set; }
        [JsonIgnore]
        public string? QuantityPerUnit { get; set; }
        public float? UnitPrice { get; set; }
        [JsonIgnore]
        public short? UnitsInStock { get; set; }
        [JsonIgnore]
        public short? UnitsOnOrder { get; set; }
        [JsonIgnore]
        public short? ReorderLevel { get; set; }
        [JsonIgnore]
        public int Discontinued { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual Supplier? Supplier { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
