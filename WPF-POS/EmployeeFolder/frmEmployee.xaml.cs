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
using WPF_POS.EmployeeFolder;
using WPF_POS.Model;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for frmEmployee.xaml
    /// </summary>
    public partial class frmEmployee : Window
    {
        private string employeeFileName = "Employee";

        private List<Employee> employees;
        private IOManager ioManager = new IOManager();

        public frmEmployee()
        {
            InitializeComponent();

            employees = ioManager.Read<List<Employee>>(employeeFileName);
            if(employees == null)
            {
                employees = new List<Employee>();
            }
            txtName.Focus();
            Clear();
        }

        private int GetLastId()
        {
            if(employees.Count == 0 || employees == null)
            {
                return 1;
            }
            else
            {
                return employees.OrderByDescending(e => e.Id).FirstOrDefault().Id + 1;
            }
        }

        private void Clear()
        {
            txtId.Text = GetLastId().ToString();
            txtName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtSalary.Text = string.Empty;
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
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username can't be empty");
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password can't be empty");
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                MessageBox.Show("Salary can't be empty");
                txtSalary.Focus();
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
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhone.Text;
            double salary = double.Parse(txtSalary.Text);
            string address = txtAddress.Text;

            Employee employee = new Employee(id, name, username, password, phone, salary, address);
            employees.Add(employee);

            ioManager.Write(employeeFileName, employees);
            MessageBox.Show("Employee added!");
            txtName.Focus();
            Clear();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name can't be empty");
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username can't be empty");
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password can't be empty");
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                MessageBox.Show("Salary can't be empty");
                txtSalary.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Employee employee = employees.Where(em => em.Id == id).FirstOrDefault();

            if(employee != null)
            {
                employee.Name = txtName.Text;
                employee.Username = txtUsername.Text;
                employee.Password = txtPassword.Text;
                employee.Phone = txtPassword.Text;
                employee.Salary = double.Parse(txtPassword.Text);
                employee.Address = txtPassword.Text;

                ioManager.Write(employeeFileName, employees);
                MessageBox.Show("Employee updated!");
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
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username can't be empty");
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password can't be empty");
                txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone can't be empty");
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                MessageBox.Show("Salary can't be empty");
                txtSalary.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address can't be empty");
                txtAddress.Focus();
                return;
            }

            int id = int.Parse(txtId.Text);
            Employee employee = employees.Where(em => em.Id == id).FirstOrDefault();

            if (employee != null)
            {
                employees.Remove(employee);

                ioManager.Write(employeeFileName, employees);
                MessageBox.Show("Employee removed!");
                txtName.Focus();
                Clear();
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList employeeList = new EmployeeList(this);
            employeeList.ShowDialog();
        }

        public void BindData(int employeeId)
        {
            Employee employee = employees.Where(e => e.Id == employeeId).FirstOrDefault();
            if(employee != null)
            {
                txtId.Text = employee.Id.ToString();
                txtName.Text = employee.Name;
                txtUsername.Text = employee.Username;
                txtPassword.Text = employee.Password;
                txtPhone.Text = employee.Phone;
                txtSalary.Text = employee.Salary.ToString();
                txtAddress.Text = employee.Address;
            }
        }
    }
}