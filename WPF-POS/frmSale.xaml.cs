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
        private string stockFileName = "Stock";

        private IOManager ioManager = new IOManager();

        private List<Product> products;
        private List<Customer> customers;
        private List<Employee> employees;
        private List<Sale> sales;

        private Sale sale = new Sale(); //declare globally because it's related between two functions, add product and save sale
        
        private List<Stock> stocks;

        private string employeeName;
        private double totalAmount;

        public frmSale(string employeeName)
        {
            InitializeComponent();
            this.employeeName = employeeName;

             //read from file
            products = ioManager.Read<List<Product>>(productFileName);
            customers = ioManager.Read<List<Customer>>(customerFileName);
            employees = ioManager.Read<List<Employee>>(employeeFileName);
            sales = ioManager.Read<List<Sale>>(saleFileName);
            stocks = ioManager.Read<List<Stock>>(stockFileName);

            if(products == null) { products = new List<Product>();  }
            if(customers == null) { customers = new List<Customer>();  }
            if(employees == null) { employees = new List<Employee>();  }
            if(sales == null) { sales = new List<Sale>(); }
            if(stocks == null) { stocks = new List<Stock>(); }

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
            cmbEmployeeName.Text = employeeName;

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

        private void calculateTotalAmount()
        {
            totalAmount = 0;
            foreach (SaleDetail saleDe in sale.SaleDetails)
            {
                totalAmount += saleDe.TotalPrice;
            }
            txtTotalAmountSale.Text = totalAmount.ToString();
            tbTotalAmount.Text = totalAmount.ToString();
        }

        private void bindDataToGrid()
        {
            dgvSale.ItemsSource = null;
            dgvSale.ItemsSource = sale.SaleDetails;

            calculateTotalAmount();
        }

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
            if (string.IsNullOrEmpty(txtSellingPrice.Text) || string.IsNullOrEmpty(txtTotalPrice.Text))
            {
                return;
            }

            string productName = cmbProductName.Text;
            int quantity = int.Parse(txtQuantity.Text);

            Product pro = products.Where(p => p.Name == productName).FirstOrDefault();
            if(pro != null)
            {
                //need to check if we have enough stock for customer product's qty
                //stock remain = stock qty - sale qty

                //selling price, is price of one item
                double sellingPrice = pro.SellingPrice;

                //total price, is price of selling price * qty
                double totalPrice = quantity * sellingPrice;

                int saleDetailsCount = sale.SaleDetails.Count;

                //declare saleDetail locally because, in each add product function, will have new saleDetail (new product each add, but not new customer)
                SaleDetail saleDetail = new SaleDetail(saleDetailsCount + 1, productName, quantity, pro.Unit, sellingPrice, totalPrice); 
                sale.SaleDetails.Add(saleDetail); //add saleDetail to list 

                MessageBox.Show("Product added!");

                //bind to data grid and calculate total amount
                bindDataToGrid();

                SubClear();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SubClear();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dgvSale.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvSale.SelectedIndex;
                sale.SaleDetails.RemoveAt(selectedIndex);

                //update n.o in data grid
                int updateId = 1;
                foreach (SaleDetail saleDe in sale.SaleDetails)
                {
                    saleDe.Id = updateId;
                    updateId++;
                }

                MessageBox.Show("Product removed!");

                //bind to data grid and calculate total amount
                bindDataToGrid();
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

            sales.Add(new Sale(invoiceId, customerName, employeeName, totalAmount, totalAmountPaid, remainAmount, sale.SaleDetails));

            //subtract from stock
            foreach(SaleDetail saleDe in sale.SaleDetails)
            {
                Stock stock = stocks.Where(st => st.ProductName == saleDe.ProductName).FirstOrDefault();
                if(stock != null)
                {
                    stock.Quantity -= saleDe.Quantity;
                    ioManager.Write(stockFileName, stocks);
                }
            }

            ioManager.Write(saleFileName, sales);
            MessageBox.Show("Sale success!");

            MainClear();
        }

        private void btnNewSale_Click(object sender, RoutedEventArgs e)
        {
            MainClear();
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string productName = cmbProductName.Text;
                int quantity = int.Parse(txtQuantity.Text);

                if(quantity > 0)
                {
                    Stock stock = stocks.Where(st => st.ProductName == productName).FirstOrDefault();

                    if (stock != null)
                    {
                        int stockRemain = stock.Quantity;

                        if (stockRemain < quantity)
                        {
                            MessageBox.Show($"sorry, not enough stock! remain only {stock.Quantity}");
                            tbStockRemain.Text = stockRemain.ToString();
                            return;
                        }
                        else
                        {
                            stockRemain -= quantity;
                            tbStockRemain.Text = stockRemain.ToString();

                            double sellingPrice = double.Parse(txtSellingPrice.Text);
                            double totalPrice = quantity * sellingPrice;
                            txtTotalPrice.Text = totalPrice.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("uh oh! product not in stock yet!");
                        return;
                    }
                }
            } catch(Exception ex) { }
        }

        private void txtAmountPaidSale_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double amountPaid = double.Parse(txtAmountPaidSale.Text);

                double remainAmount = amountPaid - totalAmount;
                txtBalanceSale.Text = remainAmount.ToString();
            } catch(Exception ex) { }
        }

        private void cmbHistoryId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbHistoryId.SelectedValue == null)
            {
                return;
            }

            int historyId = (int)cmbHistoryId.SelectedValue;
            Sale sale = sales.Where(s => s.InvoiceId == historyId).FirstOrDefault();

            if(sale != null)
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

        private void cmbProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbProductName.SelectedValue == null)
            {
                return;
            }

            string productName = cmbProductName.SelectedValue.ToString();
            if(productName == null)
            {
                return;
            }

            Product product = products.Where(p => p.Name == productName).FirstOrDefault();
            if(product != null)
            {
                //that product exist in products list, but it might or might not exist in stock
                Stock stock = stocks.Where(st => st.ProductName == product.Name).FirstOrDefault();
                if(stock == null)
                {
                    MessageBox.Show("uh oh! product is not in stock yet");
                    SubClear();
                    return;
                }

                tbStockRemain.Text = stock.Quantity.ToString();
                txtSellingPrice.Text = product.SellingPrice.ToString();
                txtQuantity.Focus();
                tbUnit.DataContext = product;
            }
        }
    }
}