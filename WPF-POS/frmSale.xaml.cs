using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_POS.Model;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmSale.xaml
    /// </summary>
    public partial class frmSale : Window
    {
        private string productFileName = "Product";
        private string customerFileName = "Customer";
        private string employeeFileName = "Employee";
        private string saleFileName = "Sale"; //related to history id
        private string purchaseFileName = "Purchase"; //related to stock remain

        private IOManager ioManager = new IOManager();

        private List<Product> products;
        private List<Customer> customers;
        private List<Employee> employees;
        private List<Sale> sales;
        private Sale sale = new Sale(); //declare globally because it's related between two functions, add product and save sale
        private List<Purchase> purchases;

        public frmSale()
        {
            InitializeComponent();

             //read from file
            products = ioManager.Read<List<Product>>(productFileName);
            customers = ioManager.Read<List<Customer>>(customerFileName);
            employees = ioManager.Read<List<Employee>>(employeeFileName);
            sales = ioManager.Read<List<Sale>>(saleFileName);
            purchases = ioManager.Read<List<Purchase>>(purchaseFileName);

            if(products == null) { products = new List<Product>();  }
            if(customers == null) { customers = new List<Customer>();  }
            if(employees == null) { employees = new List<Employee>();  }
            if(sales == null) { sales = new List<Sale>(); }
            if(purchases == null) { purchases = new List<Purchase>(); }

            //binding data to form
            foreach(Product product in products) { cmbProductName.Items.Add(product.Name); }
            foreach(Customer customer in customers) { cmbCustomerName.Items.Add(customer.Name); }
            foreach(Employee employee in employees) { cmbEmployeeName.Items.Add(employee.Name); }
            foreach(Sale sale in sales) { cmbHistoryId.Items.Add(sale.InvoiceId); }

            MainClear();
        }

        private int GetLastId()
        {
            if(sales.Count == 0 || sales == null)
            {
                return 1;
            }
            else
            {
                return sales.OrderByDescending(s => s.InvoiceId).FirstOrDefault().InvoiceId + 1;
            }
        }

        private void SubClear()
        {
            cmbProductName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            tbStockRemain.Text = "0";
        }

        private void MainClear()
        {
            SubClear();

            txtInvoiceId.Text = GetLastId().ToString();
            cmbCustomerName.Text = string.Empty;
            cmbEmployeeName.Text = string.Empty;

            txtTotalAmountSale.Text = string.Empty;
            txtAmountPaidSale.Text = string.Empty;
            txtBalanceSale.Text = string.Empty;
            tbTotalAmount.Text = "0";

            sale = new Sale(); //need new sale() because, if click on NewSale button, means new customer, so new sale, too

            dgvSale.ItemsSource = string.Empty;
            sale.SaleDetails.Clear();

            cmbHistoryId.Items.Clear();
            foreach (Sale sale in sales) { cmbHistoryId.Items.Add(sale.InvoiceId); }
        }

        private double totalAmount;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(cmbProductName.Text))
            {
                MessageBox.Show("please select a product");
                return;
            }
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("quantity can't be empty");
                txtQuantity.Focus();
                return;
            }

            string productName = cmbProductName.Text;
            int quantity = int.Parse(txtQuantity.Text);

            Product pro = products.Where(p => p.Name == productName).FirstOrDefault();
            if(pro != null)
            {
                //need to check if we have enough stock for customer product's qty
                //stock remain = purchase from supplier qty - sale qty
                int stockRemain;
                foreach (Purchase purchase in purchases)
                {
                    PurchaseDetail purchaseDetail = purchase.PurchaseDetails.Where(pd => pd.ProductName == productName).FirstOrDefault();
                    if (purchaseDetail != null)
                    {
                        stockRemain = purchaseDetail.Quantity - quantity;

                        if(stockRemain >= 0)
                        {
                            tbStockRemain.Text = stockRemain.ToString();
                            purchaseDetail.Quantity -= quantity;
                            ioManager.Write(purchaseFileName, purchases);
                        }
                        else 
                        {
                            MessageBox.Show($"stock remain only {purchaseDetail.Quantity}, sorry!");
                            tbStockRemain.Text = purchaseDetail.Quantity.ToString();
                            return;
                        }
                    }
                }

                //selling price, is price of one item
                double sellingPrice = pro.SellingPrice;
                txtSellingPrice.Text = sellingPrice.ToString();

                //total price, is price of selling price * qty
                double totalPrice = quantity * sellingPrice;
                txtTotalPrice.Text = totalPrice.ToString();

                int saleDetailsCount = sale.SaleDetails.Count;
                //declare saleDetail locally because, in each add product function, will have new saleDetail (new product each add, but not new customer)
                SaleDetail saleDetail = new SaleDetail(saleDetailsCount + 1, productName, quantity, pro.Unit, sellingPrice, totalPrice); 
                sale.SaleDetails.Add(saleDetail); //add saleDetail to list 

                //bind to data grid
                dgvSale.ItemsSource = null;
                dgvSale.ItemsSource = sale.SaleDetails;

                MessageBox.Show("product added!");

                totalAmount = 0;
                foreach (SaleDetail saleDe in sale.SaleDetails)
                {
                    totalAmount += saleDe.TotalPrice;
                }
                txtTotalAmountSale.Text = totalAmount.ToString();
                tbTotalAmount.Text = totalAmount.ToString();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SubClear();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(cmbProductName.Text))
            {
                MessageBox.Show("product name can't be empty");
                return;
            }
            if(string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("quantity can't be empty");
                return;
            }

            string productName = cmbProductName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            SaleDetail saleDetail = sale.SaleDetails.Where(s => s.ProductName == productName && s.Quantity == quantity).FirstOrDefault();
            
            if(saleDetail != null)
            {
                if(string.IsNullOrEmpty(txtSellingPrice.Text))
                {
                    MessageBox.Show("please select product from the grid to remove!");
                    return;
                }

                sale.SaleDetails.Remove(saleDetail);

                int updateId = 1;
                foreach (SaleDetail saleDe in sale.SaleDetails)
                {
                    saleDe.Id = updateId;
                    updateId++;
                }

                MessageBox.Show("product removed!");
                SubClear();

                dgvSale.ItemsSource = null;
                dgvSale.ItemsSource = sale.SaleDetails;

                totalAmount = 0;
                foreach (SaleDetail saleDe in sale.SaleDetails)
                {
                    totalAmount += saleDe.TotalPrice;
                }
                txtTotalAmountSale.Text = totalAmount.ToString();
                tbTotalAmount.Text = totalAmount.ToString();
            }
            else if(saleDetail == null)
            {
                MessageBox.Show("uh oh! this product doesn't exist in the grid");
                return;
            }
        }

        private void btnSaveSale_Click(object sender, RoutedEventArgs e)
        {
            if(dgvSale.Items.Count == 0)
            {
                MessageBox.Show("no product yet");
                return;
            }
            if(string.IsNullOrEmpty(cmbCustomerName.Text))
            {
                MessageBox.Show("don't forget customer name");
                return;
            }
            if(string.IsNullOrEmpty(cmbEmployeeName.Text))
            {
                MessageBox.Show("don't forget employee name");
                return;
            }
            if(string.IsNullOrEmpty(txtAmountPaidSale.Text))
            {
                MessageBox.Show("don't forget the amount paid");
                txtAmountPaidSale.Focus();
                return;
            }

            int invoiceId = int.Parse(txtInvoiceId.Text);
            string customerName = cmbCustomerName.Text;
            string employeeName = cmbEmployeeName.Text;
            double totalAmountPaid = double.Parse(txtAmountPaidSale.Text);
            double remainAmount = 0;

            if (totalAmountPaid < totalAmount)
            {
                MessageBox.Show("uh oh! not enough amount paid");
                txtAmountPaidSale.Focus();
                return;
            }

            remainAmount = totalAmountPaid - totalAmount;
            txtBalanceSale.Text = remainAmount.ToString();

            sales.Add(new Sale(invoiceId, customerName, employeeName, totalAmount, totalAmountPaid, remainAmount, sale.SaleDetails));

            ioManager.Write(saleFileName, sales);
            MessageBox.Show("Sale success!");
        }

        private void btnNewSale_Click(object sender, RoutedEventArgs e)
        {
            MainClear();
        }

        private void dgvSale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgvSale.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvSale.SelectedIndex;
                SaleDetail saleDetail = sale.SaleDetails[selectedIndex];

                cmbProductName.Text = saleDetail.ProductName;
                txtQuantity.Text = saleDetail.Quantity.ToString();
                txtSellingPrice.Text = saleDetail.SellingPrice.ToString();
                txtTotalPrice.Text = saleDetail.TotalPrice.ToString();

                dgvSale.CommitEdit(DataGridEditingUnit.Row, true);

                //this.Close();
                //don't need this one, because if uncomment, when run and double click on grid, it will close 'this', which is frmSale
            }
        }

        private void cmbHistoryId_DropDownClosed(object sender, EventArgs e)
        {
            sales = ioManager.Read<List<Sale>>(saleFileName); //read immediately after new customer (sale) added, so that it's up to date

            int historyId = int.Parse(cmbHistoryId.Text);
            Sale sale = sales.Where(s => s.InvoiceId == historyId).FirstOrDefault();

            if (sale != null)
            {
                txtInvoiceId.Text = sale.InvoiceId.ToString();
                cmbCustomerName.Text = sale.CustomerName;
                cmbEmployeeName.Text = sale.EmployeeName;

                txtTotalAmountSale.Text = sale.TotalAmount.ToString();
                txtAmountPaidSale.Text = sale.TotalAmountPaid.ToString();
                txtBalanceSale.Text = sale.RemainAmount.ToString();
                tbTotalAmount.Text = sale.TotalAmount.ToString();

                dgvSale.ItemsSource = sale.SaleDetails;
            }
        }
    }
}