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
using MyGooseLibrary.DatabaseClasses;
using System.Collections;

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for Page_Add.xaml
    /// </summary>
    public partial class Page_Add : Page
    {
        public TreeViewItem tv1 = new TreeViewItem();
        public TreeViewItem tv2 = new TreeViewItem();

        public Page_Add()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tv1.Header = "Products";
            foreach (var item in Goose.categorOfProdList)
            {
                tv1.Items.Add(new TreeViewItem { DataContext = item, Header = $"{item.name}", ItemsSource = Goose.productsList.Where(s => s.categoryId == item.id) });
            }
            
            tv2.Header = "Dishes";
            foreach (var item in Goose.categoryOfDishesList)
            {
                tv2.Items.Add(new TreeViewItem { DataContext = item, Header = $"{item.name}", ItemsSource = Goose.dishesList.Where(s => s.categoryId == item.id) });
            }

            mainTreeView.Items.Add(tv1);
            mainTreeView.Items.Add(tv2);
        }

        //TOCHECK
        private void mainTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            bool wasFound = false;

            if((mainTreeView.SelectedItem as object) != (mainTreeView.SelectedItem as TreeViewItem))
            {
                foreach (var item in Goose.productsList)
                {
                    if ((mainTreeView.SelectedItem as object) == item)
                    {
                        label.Content = $"{item.name}";
                        wasFound = true;
                        break;
                    }
                }

                if(!wasFound)
                {
                    foreach (var item in Goose.dishesList)
                    {
                        if ((mainTreeView.SelectedItem as object) == item)
                        {
                            label.Content = $"{item.name}";
                            wasFound = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in Goose.categorOfProdList)
                {
                    if ((mainTreeView.SelectedItem as TreeViewItem).DataContext == item)
                    {
                        label.Content = $"{item.name}";
                        wasFound = true;
                        break;
                    }
                }

                if (!wasFound)
                {
                    foreach (var item in Goose.categoryOfDishesList)
                    {
                        if ((mainTreeView.SelectedItem as TreeViewItem).DataContext == item)
                        {
                            label.Content = $"{item.name}";
                            wasFound = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}

