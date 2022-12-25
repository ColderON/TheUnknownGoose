using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyGooseLibrary;
using MyGooseLibrary.DatabaseClasses;

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
            string numbersCheck = textBoxChosenQuantity.Text + textBoxKcalAmount.Text;
            if (Goose.CheckForNumbers(numbersCheck) == false)
            {
                return;
            }

            bool duplicateFound = false;

            if (rBtnProduct.IsChecked == true )
            {
                foreach (var item in Goose.productsList)
                {
                    if (textBoxChosenName.Text == item.name)
                    {
                        duplicateFound=true;
                        MessageBox.Show("This name already exists", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                long categId = (long)(comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id;
                string prodName = textBoxChosenName.Text;               
                string measure = comboBoxChosenMeasure.Text;
                int kcal = 0;
                if (comboBoxChosenMeasure.SelectedValue == "stk")
                {
                    kcal = Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text);
                }
                else if(comboBoxChosenMeasure.SelectedValue == "grams")
                {
                    kcal = (Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text) * 100);
                }
                else
                {
                    kcal = Convert.ToInt32(textBoxKcalAmount.Text) / Convert.ToInt32(textBoxChosenQuantity.Text);
                }               
                AddNewProductToDatabase(categId, prodName, kcal, measure);
            }
            else
            {
                foreach (var item in Goose.dishesList)
                {
                    if (textBoxChosenName.Text == item.name)
                    {
                        duplicateFound = true;
                        MessageBox.Show("This name already exists", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                long categId = (long)(comboBoxChosenCategory.SelectedItem as CategoryOfDish).id;
                string dishName = textBoxChosenName.Text;
                int kcal = Convert.ToInt32(textBoxKcalAmount.Text);
                AddNewDishToDatabase(categId, dishName, kcal);
            }
        }

        private void AddNewProductToDatabase(long categid, string prodname, int kcal, string measure)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandText = "Insert into Products(Name, NumberOfCalories, Measure, CategoryId)" +
                "Values(@prodname, @kcal, @measure, @categid)";

            Goose.cmd.Parameters.Add(new SqlParameter {
                ParameterName = "categid",
                DbType = System.Data.DbType.Int64,
                Size = 64,
                Direction = ParameterDirection.Input,
                Value = categid
            });
            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "prodname",
                DbType = DbType.String,
                Size = 32,
                Direction = ParameterDirection.Input,
                Value = prodname
            });
            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "kcal",
                DbType = System.Data.DbType.Int32,
                Size = 32,
                Direction = ParameterDirection.Input,
                Value = kcal
            });
            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "measure",
                DbType = DbType.String,
                Size = 32,
                Direction = ParameterDirection.Input,
                Value = measure
            });
            Goose.cmd.ExecuteNonQuery();
            AddNewProductToList(categid, prodname, kcal, measure);
        }
        private void AddNewProductToList(long categid, string prodname, int kcal, string measure)
        {
            long tempId =  Goose.GetIdProduct(prodname);            
            Goose.productsList.Add(new Product {
                id = tempId,
                name = prodname,
                numberOfCalories= kcal,
                categoryId = categid,
                measure = measure
            });            
            this.Close();
        }

        private void AddNewDishToDatabase(long categid, string dishname, int kcal)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandText = "Insert into Dishes(Name, NumberOfCalories, CategoryId)" +
                                   "Values(@dishname, @kcal, @categid)";

            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "categid",
                DbType = System.Data.DbType.Int64,
                Size = 64,
                Direction = ParameterDirection.Input,
                Value = categid
            });
            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "dishname",
                DbType = DbType.String,
                Size = 32,
                Direction = ParameterDirection.Input,
                Value = dishname
            });
            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "kcal",
                DbType = System.Data.DbType.Int32,
                Size = 32,
                Direction = ParameterDirection.Input,
                Value = kcal
            });
            Goose.cmd.ExecuteNonQuery();
            AddNewDishToList(categid, dishname, kcal);
        }

        private void AddNewDishToList(long categid, string dishname, int kcal)
        {
            long tempId = Goose.GetIdDish(dishname);
            Goose.dishesList.Add(new Dish
            {
                id = tempId,
                name = dishname,
                categoryId = categid,
                numberOfCalories = kcal
            });
            this.Close();
        }

        private void rBtnDish_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categoryOfDishesList) { comboBoxChosenCategory.Items.Add(a); }
            comboBoxChosenMeasure.Items.Clear();
            comboBoxChosenMeasure.Items.Add("portion");
            comboBoxChosenMeasure.SelectedIndex = 0;
        }

        private void rBtnProduct_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxChosenCategory.Items.Clear();
            comboBoxChosenCategory.SelectedIndex = 0;
            foreach (var a in Goose.categorOfProdList) { comboBoxChosenCategory.Items.Add(a); }
            comboBoxChosenMeasure.Items.Clear();
            comboBoxChosenMeasure.Items.Add("stk");
            comboBoxChosenMeasure.Items.Add("grams");
            comboBoxChosenMeasure.Items.Add("per 355 ml");
            comboBoxChosenMeasure.SelectedIndex= 0;
        }
    }
}
