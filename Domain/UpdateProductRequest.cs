using Microsoft.Build.Framework;

namespace NorthwindApiDemo.Domain
{
    public class UpdateProductRequest
    {
        [Required]
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
    }
}
