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
    /// Interaction logic for frmStock.xaml
    /// </summary>
    public partial class frmStock : Window
    {
        private string stockFileName = "Stock";

        private List<Stock> stocks;
        private IOManager ioManager = new IOManager();

        public frmStock()
        {
            InitializeComponent();

            stocks = ioManager.Read<List<Stock>>(stockFileName);
            if(stocks == null) { stocks = new List<Stock>(); }

            stocks = stocks.OrderByDescending(s => s.Id).Reverse().ToList();

            dgvStock.ItemsSource = stocks;
        }
    }
}
