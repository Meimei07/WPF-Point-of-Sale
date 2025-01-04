using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }

        public PurchaseDetail() { }
        public PurchaseDetail(int id, string productName, int quantity, string unit, double costPrice, double sellingPrice)
        {
            this.Id = id;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Unit = unit;
            this.CostPrice = costPrice;
            this.SellingPrice = sellingPrice;
        }
    }
}