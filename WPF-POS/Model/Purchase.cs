using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Purchase
    {
        public int InvoiceId { get; set; }
        public string EmployeeName { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
        public Purchase() 
        {
            PurchaseDetails = new List<PurchaseDetail>();
        }
        public Purchase(int invoiceId, string employeeName, List<PurchaseDetail> purchaseDetails)
        {
            this.InvoiceId = invoiceId;
            this.EmployeeName = employeeName;
            this.PurchaseDetails = purchaseDetails;
        }
    }
}