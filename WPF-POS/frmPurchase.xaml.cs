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

        private List<Employee> employees;
        private List<Product> products;
        private List<Purchase> purchases;
        private Purchase purchase = new Purchase();

        private IOManager ioManager = new IOManager();

        public frmPurchase()
        {
            InitializeComponent();

            //read from file
            employees = ioManager.Read<List<Employee>>(employeeFileName);
            products = ioManager.Read<List<Product>>(productFileName);
            purchases = ioManager.Read<List<Purchase>>(purchaseFileName);

            if(employees == null) { employees = new List<Employee>(); }
            if(products == null) { products = new List<Product>(); }
            if(purchases == null) { purchases = new List<Purchase>(); }

            //binding data to combo box
            foreach(Employee employee in employees) { cmbEmployeeName.Items.Add(employee.Name); }
            foreach(Product product in products) { cmbProductName.Items.Add(product.Name); }
            foreach(Purchase purchase in purchases) { cmbHistoryId.Items.Add(purchase.InvoiceId); }

            //dgvPurchase.ItemsSource = purchase.PurchaseDetails;

            Clear();
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

        public void Clear()
        {
            txtInvoiceId.Text = GetLastId().ToString();
            cmbProductName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCostPrice.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;
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
            if (string.IsNullOrEmpty(txtCostPrice.Text))
            {
                MessageBox.Show("cost price can't be empty");
                txtCostPrice.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSellingPrice.Text))
            {
                MessageBox.Show("selling price can't be empty");
                txtSellingPrice.Focus();
                return;
            }

            string productName = cmbProductName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            double costPrice = double.Parse(txtCostPrice.Text);
            double sellingPrice = double.Parse(txtSellingPrice.Text);

            PurchaseDetail purchaseDetail = new PurchaseDetail(productName, quantity, "unit", costPrice, sellingPrice);
            purchase.PurchaseDetails.Add(purchaseDetail);

            dgvPurchase.ItemsSource = null;
            dgvPurchase.ItemsSource = purchase.PurchaseDetails;

            MessageBox.Show("Product added!");
            //Clear();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Clear();
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
                txtQuantity.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCostPrice.Text))
            {
                MessageBox.Show("cost price can't be empty");
                txtQuantity.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSellingPrice.Text))
            {
                MessageBox.Show("selling price can't be empty");
                txtQuantity.Focus();
                return;
            }

            string productName = cmbProductName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            double costPrice = double.Parse(txtCostPrice.Text);
            double sellingPrice = double.Parse(txtSellingPrice.Text);
            
            PurchaseDetail purchaseDetail = purchase.PurchaseDetails.Where(pd => pd.ProductName == productName && pd.Quantity == quantity && pd.CostPrice == costPrice && pd.SellingPrice == sellingPrice).FirstOrDefault();

            if(purchaseDetail != null)
            {
                purchase.PurchaseDetails.Remove(purchaseDetail);

                MessageBox.Show("product removed!");
                Clear();

                dgvPurchase.ItemsSource = string.Empty;
                dgvPurchase.ItemsSource = purchase.PurchaseDetails;
            }
            else
            {
                MessageBox.Show("uh oh! product doesn't exist in the grid");
                return;
            }
        }

        private void btnSavePurchase_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPurchase.Items.Count == 0)
            {
                MessageBox.Show("No product yet");
                return;
            }
            if(string.IsNullOrEmpty(cmbEmployeeName.Text))
            {
                MessageBox.Show("Please select an employee");
                return;
            }

            int invoiceId = int.Parse(txtInvoiceId.Text);
            string employeeName = cmbEmployeeName.Text;

            purchases.Add(new Purchase(invoiceId, employeeName, purchase.PurchaseDetails));
            ioManager.Write(purchaseFileName, purchases);

            MessageBox.Show("Purchase success!");
        }

        private void btnNewPurchase_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            cmbEmployeeName.Text = string.Empty;

            purchase = new Purchase();

            dgvPurchase.ItemsSource = string.Empty;
            purchase.PurchaseDetails.Clear();

            cmbHistoryId.Items.Clear();
            foreach(Purchase purchase in purchases) { cmbHistoryId.Items.Add(purchase.InvoiceId); }
        }

        private void cmbHistoryId_DropDownClosed(object sender, EventArgs e)
        {
            int historyId = int.Parse(cmbHistoryId.Text);
            Purchase purchase = purchases.Where(p => p.InvoiceId == historyId).FirstOrDefault();

            if(purchase != null)
            {
                txtInvoiceId.Text = purchase.InvoiceId.ToString();
                cmbEmployeeName.Text = purchase.EmployeeName;

                dgvPurchase.ItemsSource = purchase.PurchaseDetails;
            }
        }

        private void dgvPurchase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgvPurchase.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvPurchase.SelectedIndex;
                PurchaseDetail purchaseDetail = purchase.PurchaseDetails[selectedIndex];

                cmbProductName.Text = purchaseDetail.ProductName;
                txtQuantity.Text = purchaseDetail.Quantity.ToString();
                txtCostPrice.Text = purchaseDetail.CostPrice.ToString();
                txtSellingPrice.Text = purchaseDetail.SellingPrice.ToString();

                dgvPurchase.CommitEdit(DataGridEditingUnit.Row, true);
            }
        }

    }
}