using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{

    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double unitPrice { get; set; }
    }
    public class PutProduct
    {
        public string productName { get; set; }
        public double unitPrice { get; set; }
    }

}
