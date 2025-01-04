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
using WPF_POS.CustomerFolder;
using WPF_POS.EmployeeFolder;
using WPF_POS.Model;
using WPF_POS.SupplierFolder;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        private string productFileName = "Product";

        private List<Product> products;
        private IOManager ioManager = new IOManager();

        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            frmLogin.Close();

            products = ioManager.Read<List<Product>>(productFileName);
            if(products == null) { products = new List<Product>(); }

            dgvProduct.ItemsSource = products;
        }

        private void categoryEntryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            frmCategory frmCategory = new frmCategory();
            frmCategory.ShowDialog();
        }

        private void categoryListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CategoryList categoryList = new CategoryList(null);
            categoryList.ShowDialog();
        }

        private void customerEntryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.ShowDialog();
        }

        private void customerListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CustomerList customerList = new CustomerList(null);
            customerList.ShowDialog();
        }

        private void supplierEntryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.ShowDialog();
        }

        private void supplierListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SupplierList supplierList = new SupplierList(null);
            supplierList.ShowDialog();
        }

        private void employeeEntryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            frmEmployee frmEmployee = new frmEmployee();
            frmEmployee.ShowDialog();
        }

        private void employeeListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList employeeList = new EmployeeList(null);
            employeeList.ShowDialog();
        }

        private void productEntryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            frmProduct frmProduct = new frmProduct();
            frmProduct.ShowDialog();

            //so that, if in product entry, the product is being updated or deleted or added, 
            //the dgvProduct in main will auto refresh
            products = ioManager.Read<List<Product>>(productFileName);
            dgvProduct.ItemsSource = products;
        }

        private void productListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProductList productList = new ProductList(null);
            productList.ShowDialog();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
        }

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            frmPurchase frmPurchase = new frmPurchase();
            frmPurchase.ShowDialog();
        }

        private void btnSale_Click(object sender, RoutedEventArgs e)
        {
            frmSale frmSale = new frmSale();
            frmSale.ShowDialog();
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            frmStock frmStock = new frmStock();
            frmStock.ShowDialog();
        }
    }
}