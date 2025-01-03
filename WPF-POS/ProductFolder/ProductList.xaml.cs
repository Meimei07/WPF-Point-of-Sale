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
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private frmProduct frmProduct;
        private List<Product> products;
        private IOManager ioManager = new IOManager();
        
        public ProductList(frmProduct frmProduct)
        {
            InitializeComponent();

            products = ioManager.Read<List<Product>>("Product");
            if (products == null)
            {
                products = new List<Product>();
            }

            this.frmProduct = frmProduct;
            
            dgvProduct.ItemsSource = products;
        }

        private void dgvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(frmProduct == null)
            {
                return;
            }

            if(dgvProduct.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvProduct.SelectedIndex;

                //casting
                //Product product = (Product)dgvProduct.SelectedCells[0].Item;

                Product product = products[selectedIndex];

                frmProduct.BindData(product.Id);

                dgvProduct.CommitEdit(DataGridEditingUnit.Row, true);

                this.Close();
            }

        }
    }
}