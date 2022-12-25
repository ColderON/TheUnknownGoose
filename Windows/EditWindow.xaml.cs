using MyGooseLibrary.DatabaseClasses;
using MyGooseLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Collections.ObjectModel;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary> 
    public partial class EditWindow : Window
    {
        private static Product tempProd;
        private static Dish tempDish;

        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Dish dish)
        {
            InitializeComponent();
            rBtnDish.IsChecked= true;
            comboBoxChosenCategory.IsEnabled = false;
            tempDish= dish;

            foreach (var item in comboBoxChosenCategory.Items)
            {
                if(((CategoryOfDish)item).id == dish.categoryId)
                {
                    comboBoxChosenCategory.SelectedItem = item;
                    textBoxChosenName.Text = dish.name;
                    textBoxKcalAmount.Text = Convert.ToString(dish.numberOfCalories);
                    textBoxChosenQuantity.Text = "1";
                    labelChosenMeasure.Content = "Portion/Portions";
                }
            }
        }

        public EditWindow(Product product) {
            InitializeComponent();
            rBtnProduct.IsChecked = true;
            comboBoxChosenCategory.IsEnabled = false;
            tempProd= product;
            
            foreach (var item in comboBoxChosenCategory.Items)
            {
                if(((CategoryOfProduct)item).id == product.categoryId)
                {
                    comboBoxChosenCategory.SelectedItem = item;
                    textBoxChosenName.Text = product.name;
                    textBoxKcalAmount.Text = Convert.ToString(product.numberOfCalories);
                    if(product.measure == "stk")
                    {
                        textBoxChosenQuantity.Text = "1";
                        labelChosenMeasure.Content = "stk";
                    }
                    else if(product.measure == "per 100 grams")
                    {
                        textBoxChosenQuantity.Text= "100";
                        labelChosenMeasure.Content = "grams";
                    }
                    else
                    {
                        textBoxChosenQuantity.Text = "1";
                        labelChosenMeasure.Content = "can/cans 355ml";
                    }                    
                    break;
                }
            }
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            string numbersCheck = textBoxChosenQuantity.Text + textBoxKcalAmount.Text;
            if (Goose.CheckForNumbers(numbersCheck) == false)
            {
                return;
            }

            if (rBtnProduct.IsChecked == true)
            {
                string prodName = textBoxChosenName.Text;
                int kcal = 0;
                if(tempProd.measure == "per 100 grams")
                {
                    kcal = (Convert.ToInt32(textBoxKcalAmount.Text)/ Convert.ToInt32(textBoxChosenQuantity.Text)*100);
                }
                else if(tempProd.measure == "stk")
                {
                    kcal = (Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text));
                }
                else
                {
                    kcal = (Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text));
                }
                                
                EditProductInDatabase(prodName, kcal);
                EditProductInOC(prodName, kcal);
            }
            else
            {
                string dishName = textBoxChosenName.Text;
                int kcal = (Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text));
                EditDishInDatabase(dishName, kcal);
                EditDishInOC(dishName, kcal);
            }
        }

     

        private void EditProductInDatabase(string prodname, int kcal)
        {

            if (tempProd.name == prodname)
            {
                Goose.cmd = Goose.connection.CreateCommand();
                Goose.cmd.CommandText = $"Update Products Set NumberOfCalories = @kcal where Id = {tempProd.id}";
                Goose.cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "kcal",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = kcal
                });
                Goose.cmd.ExecuteNonQuery();
                this.Close();
            }
            else
            {
                Goose.cmd = Goose.connection.CreateCommand();
                Goose.cmd.CommandText = $"Update Products Set Name = @name, NumberOfCalories = @kcal where Id = {tempProd.id}";

                Goose.cmd.Parameters.Add(new SqlParameter {
                  ParameterName= "name",
                  DbType= DbType.String,
                  Size = 32,
                  Direction= ParameterDirection.Input,
                  Value= prodname
                });
                Goose.cmd.Parameters.Add(new SqlParameter {
                    ParameterName = "kcal",
                    DbType = DbType.Int32,
                    Direction= ParameterDirection.Input,
                    Value= kcal
                });
                Goose.cmd.ExecuteNonQuery();
                this.Close();
            }
        }
        private void EditProductInOC(string prodname, int kcal)
        {
            foreach (var item in Goose.productsList)
            {
                if(item.id == tempProd.id)
                {
                    item.name= prodname;
                    item.numberOfCalories = kcal;
                    break;
                }
            }
        }

        private void EditDishInDatabase(string dishname, int kcal)
        {
            if(tempDish.name == dishname)
            {
                Goose.cmd = Goose.connection.CreateCommand();
                Goose.cmd.CommandText = $"Update Dishes Set NumberOfCalories = @kcal where Id = {tempDish.id}";
                Goose.cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "kcal",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = kcal
                });
                Goose.cmd.ExecuteNonQuery();
                this.Close();               
            }
            else
            {
                Goose.cmd = Goose.connection.CreateCommand();
                Goose.cmd.CommandText = $"Update Dishes Set Name = @name, NumberOfCalories = @kcal where Id = {tempProd.id}";

                Goose.cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "name",
                    DbType = DbType.String,
                    Size = 32,
                    Direction = ParameterDirection.Input,
                    Value = dishname
                });
                Goose.cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "kcal",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = kcal
                });
                Goose.cmd.ExecuteNonQuery();
                this.Close();
            }
           
        }
        private void EditDishInOC(string dishname, int kcal)
        {
            foreach (var item in Goose.dishesList)
            {
                if (item.id == tempProd.id)
                {
                    item.name = dishname;
                    item.numberOfCalories = kcal;
                    break;
                }
            }
        }

        private void rBtnDish_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categoryOfDishesList)
                comboBoxChosenCategory.Items.Add(a);
        }

        private void rBtnProduct_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categorOfProdList)
                comboBoxChosenCategory.Items.Add(a);
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
