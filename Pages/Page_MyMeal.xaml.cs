using MyGooseLibrary;
using MyGooseLibrary.DatabaseClasses;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for Page_MyMeal.xaml
    /// </summary>
    public partial class Page_MyMeal : Page
    {

        public Page_MyMeal()
        {
            InitializeComponent();
            
        }

        private void comboBoxChosenCategory_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void radiobtnDishes_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categoryOfDishesList)
                comboBoxChosenCategory.Items.Add(a);

        }

        private void radiobtnProducts_Checked(object sender, RoutedEventArgs e)
        {
            
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categorOfProdList)
                comboBoxChosenCategory.Items.Add(a);
                

        }


        private void comboBoxChosenCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxChosenEntity.Items.Clear();
            //comboBoxChosenEntity.SelectedIndex = 0;

            if (radiobtnProducts.IsChecked == true)
            {

                foreach (var a in Goose.productsList)
                {
                    if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id)
                        comboBoxChosenEntity.Items.Add(a);

                }
            }
            else if(radiobtnDishes.IsChecked==true){
                foreach (var a in Goose.dishesList)
                {
                    if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfDish).id)
                        comboBoxChosenEntity.Items.Add(a);

                }
            }
            
        }
    }
}
