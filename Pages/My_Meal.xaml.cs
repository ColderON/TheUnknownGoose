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
    /// Interaction logic for Page_ShowProducts.xaml
    /// </summary>
    public partial class My_Meal : Page
    {

        public My_Meal()
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


            radiobtnPieces.IsEnabled = true;
            radiobtnPieces.IsChecked = true;
            radiobtnCans.IsEnabled = false;
            radiobtnGramms.IsEnabled = false;


        }

        private void radiobtnProducts_Checked(object sender, RoutedEventArgs e)
        {
            
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categorOfProdList)
                comboBoxChosenCategory.Items.Add(a);
            radiobtnPieces.IsEnabled = true;
            radiobtnCans.IsEnabled = true;
            radiobtnGramms.IsEnabled = true;
            radiobtnPieces.IsChecked = false;
            radiobtnCans.IsChecked = false;
            radiobtnGramms.IsChecked = false;

        }


        private void comboBoxChosenCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxChosenEntity.Items.Clear();
            //comboBoxChosenEntity.SelectedIndex = 0;

            if (radiobtnProducts.IsChecked == true && comboBoxChosenCategory.SelectedIndex >= 0)
            {
                foreach (var a in Goose.productsList)
                {
                    if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id)
                        comboBoxChosenEntity.Items.Add(a);

                }
            }
            else if (radiobtnDishes.IsChecked == true && comboBoxChosenCategory.SelectedIndex >= 0)
            {
                foreach (var a in Goose.dishesList)
                {
                    if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfDish).id)
                        comboBoxChosenEntity.Items.Add(a);

                }
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listBoxChosenProductsDishes.Items.Clear();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            listBoxChosenProductsDishes.Items.Remove(listBoxChosenProductsDishes.SelectedItem);
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int? ccal = 0;
            if (radiobtnDishes.IsChecked == true)
            {
                Dish item = (Dish)comboBoxChosenEntity.SelectedItem;
                if (radiobtnDishes.IsChecked == true)
                {
                    
                    listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} ccal per {textBoxGramms.Text} portions");
                    ccal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text);
                }
            }
            else if (radiobtnProducts.IsChecked == true)
            {
                Product item = (Product)comboBoxChosenEntity.SelectedItem;
                if (radiobtnGramms.IsChecked == true)
                {
                    listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text) / 100} ccal per {textBoxGramms.Text} gramms ");
                    ccal=item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text) / 100;
                }
                else if (radiobtnPieces.IsChecked == true)
                {
                    listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} ccal per {textBoxGramms.Text}");
                    ccal=item.numberOfCalories* Convert.ToInt32(textBoxGramms.Text);
                }
                else if (radiobtnCans.IsChecked == true)
                {
                    listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} ccal per {textBoxGramms.Text} cans");
                    ccal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text);
                }
            }

            Dispatcher.Invoke(() =>
            {
                MainWindow main = new MainWindow();
                main.progBar.Value += (double)ccal;
            });
            
           
        }

        private void radiobtnGramms_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "100";
        }

        private void radiobtnPieces_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "1";
        }

        private void radiobtnCans_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "1";
        }

        private void comboBoxChosenEntity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChosenEntity.Items.Count > 0)
            {
                if (radiobtnDishes.IsChecked == true) {
                    //radiobtnPieces.IsEnabled = true;
                    //radiobtnPieces.IsChecked = true;
                    //radiobtnCans.IsEnabled = false;
                    //radiobtnGramms.IsEnabled = false;
                    return;
                }
                if ((comboBoxChosenEntity.SelectedItem as Product).measure == "per 100 grams") {
                    radiobtnGramms.IsEnabled = true;
                    radiobtnGramms.IsChecked = true;
                    radiobtnCans.IsEnabled = false;
                    radiobtnPieces.IsEnabled = false;


                }
                if ((comboBoxChosenEntity.SelectedItem as Product).measure == "stk") {
                    radiobtnPieces.IsEnabled = true;
                    radiobtnPieces.IsChecked = true;
                    radiobtnCans.IsEnabled = false;
                    radiobtnGramms.IsEnabled = false;

                }
                if ((comboBoxChosenEntity.SelectedItem as Product).measure == "per 355 ml") {
                    radiobtnCans.IsEnabled = true;
                    radiobtnCans.IsChecked = true;
                    radiobtnPieces.IsEnabled = false;
                    radiobtnGramms.IsEnabled = false;

                }
                
            }
        }
    }
}
