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

namespace WPF_POS.SupplierFolder
{
    /// <summary>
    /// Interaction logic for SupplierList.xaml
    /// </summary>
    public partial class SupplierList : Window
    {
        private string supplierFileName = "Supplier";

        private List<Supplier> suppliers;
        private frmSupplier frmSupplier;
        private IOManager ioManager = new IOManager();

        public SupplierList(frmSupplier frmSupplier)
        {
            InitializeComponent();

            suppliers = ioManager.Read<List<Supplier>>(supplierFileName);
            if(suppliers == null)
            {
                suppliers = new List<Supplier>();
            }

            this.frmSupplier = frmSupplier;

            dgvSupplier.ItemsSource = suppliers;
        }

        private void dgvSupplier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(frmSupplier == null)
            {
                return;
            }

            if(dgvSupplier.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvSupplier.SelectedIndex;
                Supplier supplier = suppliers[selectedIndex];

                frmSupplier.BindData(supplier.Id);

                dgvSupplier.CommitEdit(DataGridEditingUnit.Row, true);

                this.Close();
            }
        }
    }
}