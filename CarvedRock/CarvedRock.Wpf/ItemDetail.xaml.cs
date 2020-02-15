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
    public partial class ItemDetail : Window
    {
        public ItemDetail()
        {
            InitializeComponent();
         }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.DataContext = Item;
        }
        public Item Item { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
