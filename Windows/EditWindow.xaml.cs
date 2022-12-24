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

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Dish dish)
        {
            InitializeComponent();
            rBtnDish.IsChecked= true;
        }

        public EditWindow(Product product) {
            InitializeComponent();
            rBtnProduct.IsChecked = true;
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
                        labelChosenMeasure.Content = "can/cans";
                    }                    
                    break;
                }
            }
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            Goose.cmd = Goose.connection.CreateCommand();

            if (rBtnProduct.IsChecked == true)
            {
                long categId = (long)(comboBoxChosenCategory.SelectedItem as CategoryOfProduct).id;
                string prodName = textBoxChosenName.Text;
                int kcal = Convert.ToInt32(textBoxKcalAmount.Text);
                //string measure = textBoxChosenQuantity.Text + comboBoxChosenMeasure.Text;
                //EditProductInDatabase(categId, prodName, kcal, measure);
            }
            else
            {
                long categId = (long)(comboBoxChosenCategory.SelectedItem as CategoryOfDish).id;
                string prodName = textBoxChosenName.Text;
                int kcal = Convert.ToInt32(textBoxKcalAmount.Text);
                //string measure = textBoxChosenQuantity.Text + comboBoxChosenMeasure.Text;
                //EditDishInDatabase(categId, prodName, kcal, measure);
            }
        }

        private void EditProductInDatabase(long categid, string prodname, int kcal, string measure)
        {
            Goose.cmd.CommandText = "Insert into Products(Name, NumberOfCalories, Measure, CategoryId)" +
                "Values(@prodname, @kcal, @measure, @categid)";

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
        }

        private void AddNewProductToList(long categid, string prodname, int kcal, string measure)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandType = CommandType.StoredProcedure;
            Goose.cmd.CommandText = "GetIdforList_Products";

            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = prodname,
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int64,
            });

            Goose.cmd.Parameters.Add(itemId);
            Goose.cmd.ExecuteNonQuery();

            Goose.productsList.Add(new Product
            {
                id = (long)itemId.Value,
                name = prodname,
                numberOfCalories = kcal,
                measure = measure
            });
            this.Close();
        }

        private void EditDishInDatabase(long categid, string prodname, int kcal, string measure)
        {
            Goose.cmd.CommandText = "Insert into Dishes(Name, NumberOfCalories, Measure, CategoryId)" +
                                    "Values(@categid, @prodname, @kcal, @measure)";

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
            AddNewDishToList(categid, prodname, kcal, measure);
        }

        private void AddNewDishToList(long categid, string prodname, int kcal, string measure)
        {
            Goose.cmd = Goose.connection.CreateCommand();
            Goose.cmd.CommandType = CommandType.StoredProcedure;
            Goose.cmd.CommandText = "GetIdforList_Products";

            Goose.cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = prodname,
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32,
            });

            Goose.cmd.Parameters.Add(itemId);
            Goose.cmd.ExecuteNonQuery();

            Goose.productsList.Add(new Product
            {
                id = (long)itemId.Value,
                name = prodname,
                numberOfCalories = kcal,
                measure = measure
            });
            this.Close();
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
