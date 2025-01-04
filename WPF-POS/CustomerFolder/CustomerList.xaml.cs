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

namespace WPF_POS.CustomerFolder
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        private string customerFileName = "Customer";

        private List<Customer> customers;
        private frmCustomer frmCustomer;
        private IOManager ioManager = new IOManager();

        public CustomerList(frmCustomer frmCustomer)
        {
            InitializeComponent();

            customers = ioManager.Read<List<Customer>>(customerFileName);
            if(customers == null) { customers = new List<Customer>(); }

            this.frmCustomer = frmCustomer;

            dgvCustomer.ItemsSource = customers;
        }

        private void dgvCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(frmCustomer == null)
            {
                return;
            }
            
            if(dgvCustomer.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvCustomer.SelectedIndex;
                Customer customer = customers[selectedIndex];

                frmCustomer.BindData(customer.Id);

                dgvCustomer.CommitEdit(DataGridEditingUnit.Row, true);
                this.Close();
            }
        }
    }
}