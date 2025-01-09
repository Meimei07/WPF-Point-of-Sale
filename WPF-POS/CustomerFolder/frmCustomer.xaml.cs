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
using WPF_POS.Model;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmCustomer.xaml
    /// </summary>
    public partial class frmCustomer : Window
    {
        private string customerFileName = "Customer";
        
        private List<Customer> customers;
        private IOManager ioManager = new IOManager();

        public frmCustomer()
        {
            InitializeComponent();

            customers = ioManager.Read<List<Customer>>(customerFileName);
            if(customers == null) { customers = new List<Customer>(); }

            Clear();
        }

        private int GetLastId()
        {
            if(customers.Count == 0 || customers == null)
            {
                return 1;
            }
            else
            {
                return customers.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            }
        }

        private void Clear()
        {
            txtId.Text = GetLastId().ToString();
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
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
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            Customer customer = new Customer(id, name, phone, address);
            customers.Add(customer);
            ioManager.Write(customerFileName, customers);
            
            MessageBox.Show("Customer added!");
            Clear();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            CustomerList customerList = new CustomerList(this);
            customerList.ShowDialog();
        }

        public void BindData(int customerId)
        {
            Customer customer = customers.Where(c => c.Id == customerId).FirstOrDefault();
            if(customer != null)
            {
                txtId.Text = customer.Id.ToString();
                txtName.Text = customer.Name;
                txtPhone.Text = customer.Phone;
                txtAddress.Text = customer.Address;
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
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Customer customer = customers.Where(c => c.Id == id).FirstOrDefault();

            if(customer != null)
            {
                customer.Name = txtName.Text;
                customer.Phone = txtPhone.Text;
                customer.Address = txtAddress.Text;
                
                ioManager.Write(customerFileName, customers);
                
                MessageBox.Show("Customer updated!");
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
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Customer customer = customers.Where(c => c.Id == id).FirstOrDefault();

            if (customer != null)
            {
                customers.Remove(customer);
                ioManager.Write(customerFileName, customers);
                
                MessageBox.Show("Customer removed!");
                Clear();
            }
        }
    }
}