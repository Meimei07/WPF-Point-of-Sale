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
    /// Interaction logic for frmProduct.xaml
    /// </summary>
    public partial class frmProduct : Window
    {
        private string productFileName = "Product";
        private string categoryFileName = "Category";

        private List<Product> products;
        private List<Category> categories;
        private IOManager ioManager = new IOManager();

        public frmProduct()
        {
            InitializeComponent();

            products = ioManager.Read<List<Product>>(productFileName);
            categories = ioManager.Read<List<Category>>(categoryFileName);

            if(products == null) { products = new List<Product>(); }

            foreach (Category category in categories)
            {
                cmbCategory.Items.Add(category.Name);
            }
            
            Clear();
        }

        private int GetLatestId()
        {
            if(products.Count == 0 || products == null)
            {
                return 1;
            }
            else
            {
                return products.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            }
        }

        private void Clear()
        {
            txtId.Text = GetLatestId().ToString();
            txtName.Text = string.Empty;
            cmbCategory.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtCostPrice.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show("category need to be selected");
                return;
            }
            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                MessageBox.Show("unit can't be empty");
                txtUnit.Focus();
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

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;

            string categoryName = cmbCategory.Text;
            Category category = categories.Where(c => c.Name == categoryName).FirstOrDefault();
            
            string unit = txtUnit.Text;
            double costPrice = double.Parse(txtCostPrice.Text);
            double sellingPrice = double.Parse(txtSellingPrice.Text);

            Product product = new Product(id, name, category, unit, costPrice, sellingPrice);
            products.Add(product);
            ioManager.Write(productFileName, products);

            MessageBox.Show("Product added!");
            Clear();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            ProductList productList = new ProductList(this);
            productList.ShowDialog();
        }

        public void BindData(int productId)
        {
            Product product = products.Where(p => p.Id == productId).FirstOrDefault();
            if(product != null)
            {
                //this.DataContext = product; //this need binding in xaml

                //no need Text, binding in xaml of text box
                txtId.Text = product.Id.ToString();
                txtName.Text = product.Name;
                txtCostPrice.Text = product.CostPrice.ToString();
                txtSellingPrice.Text = product.SellingPrice.ToString();
                txtUnit.Text = product.Unit;
                cmbCategory.Text = product.Category.Name;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show("category need to be selected");
                return;
            }
            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                MessageBox.Show("unit can't be empty");
                txtUnit.Focus();
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

            int id = int.Parse(txtId.Text);
            Product product = products.Where(p => p.Id == id).FirstOrDefault();

            if(product != null)
            {
                product.Name = txtName.Text;
                product.Unit = txtUnit.Text;
                product.CostPrice = double.Parse(txtCostPrice.Text);
                product.SellingPrice = double.Parse(txtSellingPrice.Text);
                
                string categoryName = cmbCategory.Text;
                Category category = categories.Where(c => c.Name == categoryName).FirstOrDefault();
                product.Category = category;

                ioManager.Write(productFileName, products);
                
                MessageBox.Show("Product updated!");
                Clear();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show("category need to be selected");
                return;
            }
            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                MessageBox.Show("unit can't be empty");
                txtUnit.Focus();
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

            int id = int.Parse(txtId.Text);
            Product product = products.Where(p => p.Id == id).FirstOrDefault();

            if (product != null)
            {
                products.Remove(product);
                ioManager.Write(productFileName, products);
                
                MessageBox.Show("Product removed!");
                Clear();
            }
        }
    }
}