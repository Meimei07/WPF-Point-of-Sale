using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Sale
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public double TotalAmount { get; set; }
        public double TotalAmountPaid { get; set; }
        public double RemainAmount { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }

        public Sale() 
        {
            this.SaleDetails = new List<SaleDetail>();
        }
        public Sale(int invoiceId, string customerName, string employeeName, double totalAmount, double totalAmountPaid, double remainAmount, List<SaleDetail> saleDetails)
        {
            this.InvoiceId = invoiceId;
            this.CustomerName = customerName;
            this.EmployeeName = employeeName;
            this.TotalAmount = totalAmount;
            this.TotalAmountPaid = totalAmountPaid;
            this.RemainAmount = remainAmount;
            this.SaleDetails = saleDetails;
        }
    }
}