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

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RBtnDishChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RBtnProductChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
