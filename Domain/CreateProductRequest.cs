using Microsoft.Build.Framework;

namespace NorthwindApiDemo.Domain
{
    public class CreateProductRequest
    {
        [Required]
        public short ProductId { get; set; }
        [Required]
        public String ProductName { get; set; }
        [Required]
        public float UnitPrice { get; set; }
    }
}
