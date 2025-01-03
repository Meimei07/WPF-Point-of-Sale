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
    /// Interaction logic for CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Window
    {
        private string categoryFileName = "Category";

        private frmCategory frmCategory;
        private List<Category> categories;
        private IOManager ioManager = new IOManager();

        public CategoryList(frmCategory frmCategory)
        {
            InitializeComponent();
            categories = ioManager.Read<List<Category>>(categoryFileName);

            if(categories == null)
            {
                categories = new List<Category>();
            }

            this.frmCategory = frmCategory;

            dgvCategory.ItemsSource = categories;
        }

        private void dgvCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(frmCategory == null)
            {
                return;
            }

            if(dgvCategory.SelectedItems.Count > 0)
            {
                int selectedIndex = dgvCategory.SelectedIndex;
                Category category = categories[selectedIndex];

                frmCategory.BindData(category.Id);

                dgvCategory.CommitEdit(DataGridEditingUnit.Row, true);
                this.Close();
            }
        }
    }
}