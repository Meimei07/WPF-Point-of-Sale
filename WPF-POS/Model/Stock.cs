using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public Stock() { }
        public Stock(int id, string productName, int quantity)
        {
            this.Id = id;
            this.ProductName = productName;
            this.Quantity = quantity;
        }
    }
}