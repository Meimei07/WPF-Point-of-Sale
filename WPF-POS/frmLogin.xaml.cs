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
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        private List<Employee> employees;
        private IOManager ioManager = new IOManager();

        public frmLogin()
        {
            InitializeComponent();

            employees = ioManager.Read<List<Employee>>("Employee");
            if(employees == null) { employees = new List<Employee>(); }

            txtUsername.Focus();
        }

        private void Clear()
        {
            txtUsername.Text = string.Empty;
            pbPassword.Password = string.Empty;
        }

        private Employee employeeExist(string username, string password)
        {
            Employee employee = employees.Where(em => em.Username == username && em.Password == password).FirstOrDefault();
            if(employee == null)
            {
                return null;
            }
            else
            {
                return employee;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("username/password can't be empty");
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("password can't be empty");
                pbPassword.Focus();
                return;
            }

            string username = txtUsername.Text;
            string password = pbPassword.Password;

            Employee employee = employeeExist(username, password);
            if(employee != null)
            {
                //go into main form
                frmMain frmMain = new frmMain(this);
                frmMain.ShowDialog();
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
                txtUsername.Focus();
            }
            Clear();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}