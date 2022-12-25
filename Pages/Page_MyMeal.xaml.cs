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
using TheUnknownGoose.MyGooseLibrary.DatabaseClasses;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for Page_MyMeal.xaml
    /// </summary> 

    public partial class Page_MyMeal : Page
    {
        public static MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public Page_MyMeal()
        {
            InitializeComponent();            
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
            Dispatcher.Invoke(() =>
            {
                mainWindow.progBar.Value = 0;
                mainWindow.curCaloris = 0;
                mainWindow.lblCurrentCallorie.Content = mainWindow.curCaloris;
                mainWindow.lblMaxCalorie.Content = mainWindow.maxCalories;
                mainWindow.lblCurrentCallorie.Foreground = Brushes.Black;               
            });
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.curCaloris -= (double)(listBoxChosenProductsDishes.SelectedItem as VM).numberOfCalories;
            mainWindow.progBar.Value = mainWindow.curCaloris;
            mainWindow.lblCurrentCallorie.Content = mainWindow.curCaloris;
            mainWindow.lblMaxCalorie.Content = mainWindow.maxCalories;
            if (mainWindow.curCaloris <= mainWindow.maxCalories)
            {
                mainWindow.lblCurrentCallorie.Foreground = Brushes.Black;
            }
            listBoxChosenProductsDishes.Items.Remove(listBoxChosenProductsDishes.SelectedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {            
            int? kcal = 0;
            if (radiobtnDishes.IsChecked == true)
            {
                if (comboBoxChosenEntity.SelectedIndex >= 0) {
                    Dish item = (Dish)comboBoxChosenEntity.SelectedItem;
                    if (radiobtnDishes.IsChecked == true)
                    {
                        listBoxChosenProductsDishes.Items.Add(new VM
                        {
                            id = (comboBoxChosenEntity.SelectedItem as Dish).id,
                            name = (comboBoxChosenEntity.SelectedItem as Dish).name,
                            measure = "portion(s)",
                            numberOfCalories = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text),
                            categoryId = (comboBoxChosenEntity.SelectedItem as Dish).categoryId,
                            countOfitems = Convert.ToInt32(textBoxGramms.Text)
                        });
                        //listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} kcal per {textBoxGramms.Text} portions");
                        kcal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text);
                    }
                }
            }
            else if (radiobtnProducts.IsChecked == true)
            {
                if(comboBoxChosenEntity.SelectedIndex >= 0) {
                    Product item = (Product)comboBoxChosenEntity.SelectedItem;
                    if (radiobtnGramms.IsChecked == true)
                    {
                        listBoxChosenProductsDishes.Items.Add(new VM
                        {
                            id = (comboBoxChosenEntity.SelectedItem as Product).id,
                            name = (comboBoxChosenEntity.SelectedItem as Product).name,
                            measure = (comboBoxChosenEntity.SelectedItem as Product).measure,
                            numberOfCalories = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text) / 100,
                            categoryId = (comboBoxChosenEntity.SelectedItem as Product).categoryId,
                            countOfitems = Convert.ToInt32(textBoxGramms.Text)
                        });
                        //listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text) / 100} kcal per {textBoxGramms.Text} gramms ");
                        kcal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text) / 100;
                    }
                    else if (radiobtnPieces.IsChecked == true)
                    {
                        listBoxChosenProductsDishes.Items.Add(new VM
                        {
                            id = (comboBoxChosenEntity.SelectedItem as Product).id,
                            name = (comboBoxChosenEntity.SelectedItem as Product).name,
                            measure = (comboBoxChosenEntity.SelectedItem as Product).measure,
                            numberOfCalories = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text),
                            categoryId = (comboBoxChosenEntity.SelectedItem as Product).categoryId,
                            countOfitems = Convert.ToInt32(textBoxGramms.Text)
                        });
                        //listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} kcal per {textBoxGramms.Text}");
                        kcal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text);
                    }
                    else if (radiobtnCans.IsChecked == true)
                    {
                        listBoxChosenProductsDishes.Items.Add(new VM
                        {
                            id = (comboBoxChosenEntity.SelectedItem as Product).id,
                            name = (comboBoxChosenEntity.SelectedItem as Product).name,
                            measure = (comboBoxChosenEntity.SelectedItem as Product).measure,
                            numberOfCalories = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text),
                            categoryId = (comboBoxChosenEntity.SelectedItem as Product).categoryId,
                            countOfitems = Convert.ToInt32(textBoxGramms.Text)
                        });
                        //listBoxChosenProductsDishes.Items.Add(item + $"- {item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text)} kcal per {textBoxGramms.Text} cans");
                        kcal = item.numberOfCalories * Convert.ToInt32(textBoxGramms.Text);
                    }
                }
            }

            Dispatcher.Invoke(() =>
            {
                mainWindow.progBar.Value += (double)kcal;
                mainWindow.curCaloris += (double)kcal;
                mainWindow.lblCurrentCallorie.Content = mainWindow.curCaloris;
                mainWindow.lblMaxCalorie.Content = mainWindow.maxCalories;
                if (mainWindow.curCaloris > mainWindow.maxCalories)
                {
                    mainWindow.lblCurrentCallorie.Foreground = Brushes.Red;
                }
            });
        }

        private void radiobtnGramms_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "100";
            lblMeasure.Content = "grams";
        }

        private void radiobtnPieces_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "1";
            lblMeasure.Content = "stk";
        }

        private void radiobtnCans_Checked(object sender, RoutedEventArgs e)
        {
            textBoxGramms.Text = "1";
            lblMeasure.Content = "can/cans 355ml";
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
