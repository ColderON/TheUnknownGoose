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

namespace TheUnknownGoose
{
    /// <summary>
    /// Interaction logic for Page_CalorieCounter.xaml
    /// </summary>
    public partial class Page_CalorieCounter : Page
    {
        double result = 0;
        int goal = 0;
        public Page_CalorieCounter()
        {
            InitializeComponent();
        }

        private bool checkIfGenderPicked() {
            if (radiobtnMale.IsChecked == true)
                return true;
            if(radiobtnFemale.IsChecked==true)
                return true;
            return false;
        }

        private bool checkIfWeightEntered() {
            if (textBoxWeight.Text.Length == 0)
                return false;
            return true;
        }

        public bool checkIfHeightEntered() { 
            if(textBoxHeight.Text.Length==0)
                return false;
            return true;
        }

        public bool checkIfAgeEntered() {//////////add check for numbers 
            if(textBoxAge.Text.Length==0 )
                return false;
            return true; 
        }

        public bool checkIfGoalChosen() {
            if (radiobtnMeintenance.IsChecked == true)
            { 
                goal = 0; 
                return true; 
            }

            if (radiobtnShred.IsChecked == true)
            {
                goal = 1;
                return true;
            }

            if (radiobtnBulk.IsChecked == true)
            {
                goal = 2;
                return true;
            }

            return false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string numbersCheck= textBoxAge.Text+textBoxHeight.Text+textBoxWeight.Text;
            if (Goose.CheckForNumbers(numbersCheck) == false)
            {
                return;
            }

            if (checkIfGenderPicked() && checkIfWeightEntered() && checkIfHeightEntered() && checkIfAgeEntered() && checkIfGoalChosen()) {
                if (radiobtnMale.IsChecked == true)
                {
                    result=10* Convert.ToInt32(textBoxWeight.Text) + 6.25 * Convert.ToInt32(textBoxHeight.Text) + 5 * Convert.ToInt32(textBoxAge.Text) +5;                    
                }
                else {
                    result = 10 * Convert.ToInt32(textBoxWeight.Text) + 6.25 * Convert.ToInt32(textBoxHeight.Text) + 5 * Convert.ToInt32(textBoxAge.Text) -161;                    
                }
            }
            switch (goal) {
                case 1:
                    result -= 150;
                    break;
                case 2:
                    result += 300;
                    break;
            }

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.maxCalories = result;
            mainWindow.progBar.Maximum= result;
            mainWindow.lblMaxCalorie.Content = mainWindow.maxCalories;
            textBoxResult.Text = result.ToString();
        }
    }
}
