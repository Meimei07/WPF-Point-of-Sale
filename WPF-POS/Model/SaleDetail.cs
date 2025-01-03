using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double SellingPrice { get; set; }
        public double TotalPrice { get; set; }

        public SaleDetail() { }
        public SaleDetail(int id, string productName, int quantity, string unit, double sellingPrice, double totalPrice)
        {
            this.Id = id;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Unit = unit;
            this.SellingPrice = sellingPrice;
            this.TotalPrice = totalPrice;
        }
    }
}