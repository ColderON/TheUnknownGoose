using MyGooseLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowProductsList_Click(object sender, RoutedEventArgs e)
        {   
            MainPage.Content = new Page_ShowProducts();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = new Page_Add();
        }

        private void btnCalories_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = new Page_CalorieCounter();
        }
    }
}
