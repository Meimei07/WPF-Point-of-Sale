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

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmCategory.xaml
    /// </summary>
    public partial class frmCategory : Window
    {
        private string categoryFileName = "Category";

        private List<Category> categories;
        private IOManager ioManager = new IOManager();

        public frmCategory()
        {
            InitializeComponent();

            categories = ioManager.Read<List<Category>>(categoryFileName);
            if(categories == null) { categories = new List<Category>(); } 

            Clear();
        }

        private int GetLastId()
        {
            if(categories.Count == 0 || categories == null)
            {
                return 1;
            }
            else
            {
                return categories.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            }
        }

        private void Clear()
        {
            txtId.Text = GetLastId().ToString();
            txtName.Text = string.Empty;
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("name can't be empty");
                txtName.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;

            Category category = new Category(id, name);
            categories.Add(category);
            ioManager.Write(categoryFileName, categories);

            MessageBox.Show("Category added!");
            Clear();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            CategoryList categoryList = new CategoryList(this);
            categoryList.ShowDialog();
        }

        public void BindData(int categoryId)
        {
            Category category = categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if(category != null)
            {
                txtId.Text = category.Id.ToString();
                txtName.Text = category.Name;
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

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            Category category = categories.Where(c => c.Id == id && c.Name == name).FirstOrDefault();

            if(category != null)
            {
                categories.Remove(category);
                ioManager.Write(categoryFileName, categories);

                MessageBox.Show("Category removed!");
                Clear();
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

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            Category category = categories.Where(c => c.Id == id && c.Name == name).FirstOrDefault();

            if (category != null)
            {
                category.Name = txtName.Text;
                ioManager.Write(categoryFileName, categories);
                
                MessageBox.Show("Category updated!");
                Clear();
            }
        }
    }
}