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
        public double curCaloris = 0;
        public double maxCalories= 2200;
        public static Page_MyMeal myMeal = new Page_MyMeal();
        public static Page_ProductsList productsList = new Page_ProductsList();
        public static Page_CalorieCounter calorieCounter = new Page_CalorieCounter();


        public MainWindow()
        {           
            InitializeComponent();
            Goose.CreateConnection();
            Goose.FillCategoriesOfDishesList();
            Goose.FillCategorOfProdList();
            Goose.FillDishesList();
            Goose.FillProductsList();
            progBar.Maximum = maxCalories;
            progBar.Value = 0;
            lblCurrentCallorie.Content = curCaloris;
            lblMaxCalorie.Content = maxCalories;
        }

        private void btnMyMeal_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = myMeal;
        }

        private void btnProductsList_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = productsList;
        }       

        private void btnCalorieCounter_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = calorieCounter;
        }
    }
}
