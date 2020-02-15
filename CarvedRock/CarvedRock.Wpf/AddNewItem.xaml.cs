using CarvedRock.Models;
using CarvedRock.Services;
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
using System.Windows.Shapes;

namespace CarvedRock.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewItem.xaml
    /// </summary>
    public partial class AddNewItem : Window
    {
        public AddNewItem()
        {
            InitializeComponent();
            txtItemText.Text = "Text here";
            txtDetailText.Text = "Details here";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataStore = new MockDataStore();
            var newItem = new Item() { 
                Text = txtItemText.Text, 
                Description = txtDetailText.Text 
            };

            dataStore.AddItemAsync(newItem).Wait();
            Close();
        }
    }
}
