using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_POS.Model;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmPurchase.xaml
    /// </summary>
    public partial class frmPurchase : Window
    {
        private string employeeFileName = "Employee";
        private string productFileName = "Product";
        private string purchaseFileName = "Purchase";
        private string stockFileName = "Stock";

        private List<Employee> employees;
        private List<Product> products;
        private List<Purchase> purchases;
        private Purchase purchase = new Purchase();
        private List<Stock> stocks;

        private IOManager ioManager = new IOManager();
        private string employeeName;

        public frmPurchase(string employeeName)
        {
            InitializeComponent();
            this.employeeName = employeeName;

            //read from file
            employees = ioManager.Read<List<Employee>>(employeeFileName);
            products = ioManager.Read<List<Product>>(productFileName);
            purchases = ioManager.Read<List<Purchase>>(purchaseFileName);
            stocks = ioManager.Read<List<Stock>>(stockFileName);

            if(employees == null) { employees = new List<Employee>(); }
            if(products == null) { products = new List<Product>(); }
            if(purchases == null) { purchases = new List<Purchase>(); }
            if(stocks == null) { stocks = new List<Stock>(); }

            //binding data to combo box
            foreach(Employee employee in employees) { cmbEmployeeName.Items.Add(employee.Name); }
            foreach(Product product in products) { cmbProductName.Items.Add(product.Name); }
            foreach(Purchase purchase in purchases) { cmbHistoryId.Items.Add(purchase.InvoiceId); }

            MainClear();
        }

        private int GetLastId()
        {
            if(purchases.Count == 0 || purchases == null)
            {
                return 1;
            }
            else
            {
                return purchases.OrderByDescending(p => p.InvoiceId).FirstOrDefault().InvoiceId + 1;
            }
        }

        public void SubClear()
        {            
            cmbProductName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCostPrice.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;
        }

        private void MainClear()
        {
            SubClear();

            txtInvoiceId.Text = GetLastId().ToString();
            cmbEmployeeName.Text = employeeName;

            purchase = new Purchase();

            dgvPurchase.ItemsSource = string.Empty;
            purchase.PurchaseDetails.Clear();

            cmbHistoryId.Items.Clear();
            foreach (Purchase purchase in purchases) { cmbHistoryId.Items.Add(purchase.InvoiceId); }
        }

        private void bindDataToGrid()
        {
            dgvPurchase.ItemsSource = null;
            dgvPurchase.ItemsSource = purchase.PurchaseDetails;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(cmbProductName.Text))
            {
                MessageBox.Show("Please select a product");
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
            double costPrice = double.Parse(txtCostPrice.Text);
            double sellingPrice = double.Parse(txtSellingPrice.Text);

            Product product = products.Where(p => p.Name == productName).FirstOrDefault();
            if(product != null)
            {
                int purchaseDetailsCount = purchase.PurchaseDetails.Count;

                PurchaseDetail purchaseDetail = new PurchaseDetail(purchaseDetailsCount + 1, productName, quantity, product.Unit, costPrice, sellingPrice);
                purchase.PurchaseDetails.Add(purchaseDetail);       

                MessageBox.Show("Product added!");

                bindDataToGrid();
                SubClear();

                //also need to add the product name, id, and quantity to another file called stock
                //to track the stock, when customer buy, need to subtract (-)
                //when employees buy more, need to add (+)
                //cannot add or subtract directly from the purchase detail list
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SubClear();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dgvPurchase.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvPurchase.SelectedIndex;
                purchase.PurchaseDetails.RemoveAt(selectedIndex);

                //update n.o in data grid
                int updateId = 1;
                foreach(PurchaseDetail purchaseDe in purchase.PurchaseDetails)
                {
                    purchaseDe.Id = updateId;
                    updateId++;
                }

                MessageBox.Show("product removed!");

                bindDataToGrid();
            }
        }

        private void btnSavePurchase_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPurchase.Items.Count == 0)
            {
                MessageBox.Show("no product yet");
                return;
            }
            if(string.IsNullOrEmpty(cmbEmployeeName.Text))
            {
                MessageBox.Show("please select an employee");
                return;
            }

            int invoiceId = int.Parse(txtInvoiceId.Text);
            string employeeName = cmbEmployeeName.Text;

            purchases.Add(new Purchase(invoiceId, employeeName, purchase.PurchaseDetails));
            ioManager.Write(purchaseFileName, purchases);

            //add purchase qty to stock qty
            foreach(PurchaseDetail purchaseDetail in purchase.PurchaseDetails)
            {
                //find product, becuz in else, when add new stock, will need product Id
                Product product = products.Where(p => p.Name == purchaseDetail.ProductName).FirstOrDefault();

                Stock stock = stocks.Where(st => st.Id == product.Id).FirstOrDefault();
                if(stock != null) //if already has that product in stock, add more quantity to it
                {
                    stock.Quantity += purchaseDetail.Quantity;
                }
                else //if doesn't have product in stock yet, add new stock
                {
                    Stock newStock = new Stock(product.Id, purchaseDetail.ProductName, purchaseDetail.Quantity);
                    stocks.Add(newStock);
                }
                ioManager.Write(stockFileName, stocks);
            }

            MessageBox.Show("Purchase success!");
            MainClear();
        }

        private void btnNewPurchase_Click(object sender, RoutedEventArgs e)
        {
            MainClear();
        }

        private void cmbHistoryId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHistoryId.SelectedValue == null)
            {
                return;
            }

            int historyId = (int)(cmbHistoryId.SelectedValue);
            Purchase purchase = purchases.Where(p => p.InvoiceId == historyId).FirstOrDefault();

            if (purchase != null)
            {
                txtInvoiceId.Text = purchase.InvoiceId.ToString();
                cmbEmployeeName.Text = purchase.EmployeeName;

                dgvPurchase.ItemsSource = purchase.PurchaseDetails;
            }
        }

        private void cmbProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProductName.SelectedValue == null)
            {
                return;
            }

            string productName = cmbProductName.SelectedValue.ToString();
            if (string.IsNullOrEmpty(productName))
            {
                return;
            }

            Product product = products.Where(p => p.Name == productName).FirstOrDefault();
            if(product != null)
            {
                txtCostPrice.Text = product.CostPrice.ToString();
                txtSellingPrice.Text = product.SellingPrice.ToString();
                txtQuantity.Focus();
                tbUnit.DataContext = product;
            }
        }
    }
}