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
using the_debt_book.ViewModels;

namespace the_debt_book.Views
{
    /// <summary>
    /// Interaction logic for mainWindow.xaml
    /// </summary>
    public partial class mainWindow : Window
    {
        public mainWindow()
        {
            InitializeComponent();
            DataContext = new AddDebtorsViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDebtorsWin addDebtorsWin = new AddDebtorsWin();

            addDebtorsWin.Show();
        }
    }
}
