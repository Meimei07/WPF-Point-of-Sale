using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Unit { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }

        public Product() { }
        public Product(int id, string name, Category category, string unit, double costPrice, double sellingPrice)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Unit = unit;
            this.CostPrice = costPrice;
            this.SellingPrice = sellingPrice;
        }
    }
}