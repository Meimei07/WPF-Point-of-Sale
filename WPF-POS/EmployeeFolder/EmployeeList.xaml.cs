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

namespace WPF_POS.EmployeeFolder
{
    /// <summary>
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        private string employeeFileName = "Employee";

        private List<Employee> employees;
        private frmEmployee frmEmployee;
        private IOManager ioManager = new IOManager();

        public EmployeeList(frmEmployee frmEmployee)
        {
            InitializeComponent();

            employees = ioManager.Read<List<Employee>>(employeeFileName);
            if(employees == null)
            {
                employees = new List<Employee>();
            }

            this.frmEmployee = frmEmployee;

            dgvEmployee.ItemsSource = employees;
        }

        private void dgvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(frmEmployee == null)
            {
                return;
            }

            if(dgvEmployee.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvEmployee.SelectedIndex;
                Employee employee = employees[selectedIndex];

                frmEmployee.BindData(employee.Id);

                dgvEmployee.CommitEdit(DataGridEditingUnit.Row, true);

                this.Close();
            }
        }
    }
}
