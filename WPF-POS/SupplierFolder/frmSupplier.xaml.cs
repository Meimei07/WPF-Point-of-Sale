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
using WPF_POS.SupplierFolder;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmSupplier.xaml
    /// </summary>
    public partial class frmSupplier : Window
    {
        private string supplierFileName = "Supplier";

        private List<Supplier> suppliers;
        private IOManager ioManager = new IOManager();

        public frmSupplier()
        {
            InitializeComponent();

            suppliers = ioManager.Read<List<Supplier>>(supplierFileName);
            if(suppliers == null)
            {
                suppliers = new List<Supplier>();
            }
            txtName.Focus();
            Clear();
        }

        private int GetLastId()
        {
            if(suppliers.Count == 0 || suppliers == null)
            {
                return 1;
            }
            else
            {
                return suppliers.OrderByDescending(s => s.Id).FirstOrDefault().Id + 1;
            }
        }

        private void Clear()
        {
            txtId.Text = GetLastId().ToString();
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            Supplier supplier = new Supplier(id, name, phone, address);
            suppliers.Add(supplier);

            ioManager.Write(supplierFileName, suppliers);
            MessageBox.Show("Supplier added");
            txtName.Focus();
            Clear();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            SupplierList supplierList = new SupplierList(this);
            supplierList.ShowDialog();
        }

        public void BindData(int supplierId)
        {
            Supplier supplier = suppliers.Where(s => s.Id == supplierId).FirstOrDefault();
            if(supplier != null)
            {
                txtId.Text = supplier.Id.ToString();
                txtName.Text = supplier.Name;
                txtPhone.Text = supplier.Phone;
                txtAddress.Text = supplier.Address;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Supplier supplier = suppliers.Where(s => s.Id == id).FirstOrDefault();

            if(supplier != null)
            {
                supplier.Name = txtName.Text;
                supplier.Phone = txtPhone.Text;
                supplier.Address = txtAddress.Text;

                ioManager.Write(supplierFileName, suppliers);
                MessageBox.Show("Supplier updated!");
                txtName.Focus();
                Clear();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Supplier supplier = suppliers.Where(s => s.Id == id).FirstOrDefault();

            if (supplier != null)
            {
                suppliers.Remove(supplier);

                ioManager.Write(supplierFileName, suppliers);
                MessageBox.Show("Supplier removed!");
                txtName.Focus();
                Clear();
            }
        }
    }
}