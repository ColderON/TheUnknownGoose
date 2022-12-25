using MyGooseLibrary.DatabaseClasses;
using MyGooseLibrary;
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
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for Page_ProductsList.xaml
    /// </summary>
    public partial class Page_ProductsList : Page
    {       
        public Page_ProductsList()
        {   
            InitializeComponent();
            btnEdit.IsEnabled= false;
            btnDelete.IsEnabled= false;
        }

        public string GetSelectedCategory(string category)
        {
            return Convert.ToString(comboBoxChosenCategory.SelectedItem);
        }
        public string GetSelectedName(string name)
        {
            return Convert.ToString(listBoxShowChosenProducts.SelectedItem);
        }
       

        private void radiobtnDishes_Checked(object sender, RoutedEventArgs e)
        {            
            radiobtnProducts.IsChecked = false;
            comboBoxChosenCategory.Items.Clear();
            foreach (var a in Goose.categoryOfDishesList) { comboBoxChosenCategory.Items.Add(a); }
            comboBoxChosenCategory.SelectedIndex = 0;
        }

        private void radiobtnProducts_Checked(object sender, RoutedEventArgs e)
        {          
            radiobtnDishes.IsChecked = false;            
            comboBoxChosenCategory.Items.Clear();
            foreach (var a in Goose.categorOfProdList) { comboBoxChosenCategory.Items.Add(a); }
            comboBoxChosenCategory.SelectedIndex = 0;
        }

        private void comboBoxChosenCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxShowChosenProducts.Items.Clear();
            if (radiobtnProducts.IsChecked == true)
            {
                if(comboBoxChosenCategory.SelectedIndex >= 0)
                {
                    foreach (var a in Goose.productsList)
                    {
                        if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id)
                            listBoxShowChosenProducts.Items.Add(a);
                    }
                }
            }
            else if (radiobtnDishes.IsChecked == true)
            {
                if (comboBoxChosenCategory.SelectedIndex >= 0)
                {
                    foreach (var a in Goose.dishesList)
                    {                  
                        if (a.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfDish).id)
                            listBoxShowChosenProducts.Items.Add(a);
                    }
                }
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Closed += AddWindow_Closed;
            addWindow.Show();
        }

        private void AddWindow_Closed(object? sender, EventArgs e)
        {
            listBoxShowChosenProducts.Items.Clear();
            if (radiobtnProducts.IsChecked == true && comboBoxChosenCategory.SelectedIndex >= 0)
            {
                foreach (var item in Goose.productsList)
                {
                    if (item.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id)
                    {
                        listBoxShowChosenProducts.Items.Add(item);
                    }
                }
            }
            else if (radiobtnDishes.IsChecked == true && comboBoxChosenCategory.SelectedIndex >= 0)
            {
                foreach (var item in Goose.dishesList)
                {
                    if (item.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfDish).id)
                    {
                        listBoxShowChosenProducts.Items.Add(item);
                    }
                }
            }
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if (radiobtnProducts.IsChecked == true && listBoxShowChosenProducts.SelectedIndex >= 0)
            {
                EditWindow editWindow = new EditWindow((listBoxShowChosenProducts.SelectedItem as Product));                
                editWindow.Show();
                editWindow.Closed += EditWindow_Closed;

            }
            else if (radiobtnDishes.IsChecked == true && listBoxShowChosenProducts.SelectedIndex >= 0)
            {
                EditWindow editWindow = new EditWindow((listBoxShowChosenProducts.SelectedItem as Dish));              
                editWindow.Show();
                editWindow.Closed += EditWindow_Closed;
            }
        }

        private void EditWindow_Closed(object? sender, EventArgs e)
        {
            CollectionViewSource.GetDefaultView(listBoxShowChosenProducts.Items).Refresh();
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            if(listBoxShowChosenProducts.SelectedIndex >= 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("You sure?\nDelete this item?", "DELETE", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if(radiobtnProducts.IsChecked == true)
                    {
                        DeleteProduct((listBoxShowChosenProducts.SelectedItem as Product));
                    }
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    if(radiobtnDishes.IsChecked == true)
                    {
                        DeleteDish((listBoxShowChosenProducts.SelectedItem as Dish));
                    }
                }
            }
        }
        private void DeleteProduct(Product? product)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandText = $"Delete from Products where id = {product.id}";
            Goose.cmd.ExecuteNonQuery();

            foreach (var item in Goose.productsList)
            {
                if(item.id == product.id)
                {
                    Goose.productsList.Remove(item);
                    break;
                }
            }
            listBoxShowChosenProducts.Items.Clear();
            foreach (var item in Goose.productsList)
            {               
                if(item.categoryId == (comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id)
                {
                    listBoxShowChosenProducts.Items.Add(item);
                }
            }
        }

        private void DeleteDish(Dish? dish)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandText = $"Delete from Dishes where id = {dish.id}";
            Goose.cmd.ExecuteNonQuery();

            foreach (var item in Goose.dishesList)
            {
                if (item.id == dish.id)
                {
                    Goose.dishesList.Remove(item);
                    CollectionViewSource.GetDefaultView(listBoxShowChosenProducts.Items).Refresh();
                    break;
                }
            }
        }

        private void listBoxShowChosenProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxShowChosenProducts.SelectedIndex >= 0)
            {
                btnEdit.IsEnabled= true;
                btnDelete.IsEnabled= true;
                if (radiobtnProducts.IsChecked == true)
                {
                    textBoxShowKcal.Text = Convert.ToString((listBoxShowChosenProducts.SelectedItem as Product).numberOfCalories);
                    textBoxShowMeasure.Text = (listBoxShowChosenProducts.SelectedItem as Product).measure;
                }
                else if (radiobtnDishes.IsChecked == true)
                {
                    textBoxShowKcal.Text = Convert.ToString((listBoxShowChosenProducts.SelectedItem as Dish).numberOfCalories);
                    textBoxShowMeasure.Text = "per portion";
                }
            }
            else
            {
                textBoxShowKcal.Text = null;
                textBoxShowMeasure.Text = null;
            }
        }
    }
}
