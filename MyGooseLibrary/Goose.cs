﻿using MyGooseLibrary.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyGooseLibrary
{
    public static class Goose
    {
        public static IDbConnection? connection;
        public static IDbCommand? cmd;
        public static IDataReader? reader;

        public static ObservableCollection<CategoryOfProduct> categorOfProdList = new ObservableCollection<CategoryOfProduct>();
        public static ObservableCollection<CategoryOfDish> categoryOfDishesList = new ObservableCollection<CategoryOfDish>();
        public static ObservableCollection<Product> productsList = new ObservableCollection<Product>();       
        public static ObservableCollection<Dish> dishesList = new ObservableCollection<Dish>();

        public static void CreateConnection()
        {
            //NuGet to install - System.Data.SqlClient
            connection = new SqlConnection(new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "TheUnknownGoose_Database",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true,
                TrustServerCertificate = true,
            }.ConnectionString);

            try
            {
                connection.Open();
            }
            catch (DbException ex) {
            
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }       

        public static void FillCategorOfProdList()
        {
            categorOfProdList.Clear();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM CategoriesOfProducts";
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               categorOfProdList.Add(new CategoryOfProduct(reader));
            }

            //TOCHECK
            reader.Close();
        }
        public static long GetIdCategoryOfProduct(string itemName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetIdforList_CategProduct";

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = itemName
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int64,
            });

            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();

            return (long)itemId.Value;
        }

        public static void FillCategoriesOfDishesList()
        {
            categoryOfDishesList.Clear();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM CategoriesOfDishes";
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                categoryOfDishesList.Add(new CategoryOfDish(reader));
            }

            //TOCHECK
            reader.Close();
        }
        public static long GetIdCategoryOfDish(string itemName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetIdforList_CategDish";

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = itemName
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int64,
            });

            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();

            return (long)itemId.Value;
        }
        
        public static void FillProductsList()
        {
            productsList.Clear();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, NumberOfCalories, Measure, CategoryId, Comment from Products";
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                productsList.Add(new Product(reader));
            }
        }
        public static long GetIdProduct(string itemName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetIdforList_Products";

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = itemName
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int64,
            });

            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();

            return (long)itemId.Value;
        }

        public static void FillDishesList()
        {
            dishesList.Clear();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, NumberOfCalories, Comment, CategoryId FROM Dishes";
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dishesList.Add(new Dish(reader));
            }

            //TOCHECK
            reader.Close();
        }
        public static long GetIdDish(string itemName)
        {
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetIdforList_Dishes";

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "itemName",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Size = 32,
                Value = itemName
            });

            SqlParameter itemId = (new SqlParameter
            {
                ParameterName = "itemId",
                Direction = ParameterDirection.Output,
                DbType = DbType.Int64,
            });

            cmd.Parameters.Add(itemId);
            cmd.ExecuteNonQuery();

            return (long)itemId.Value;
        }

        public static bool CheckForNumbers(string tbText)
        {
            for (int i = 0; i < tbText.Length; i++)
            {
                if (tbText[i] < (char)48 || tbText[i] > (char)59)
                {
                    MessageBox.Show("Some of the textboxes contains letters", "Error!", MessageBoxButton.OK, icon:MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
