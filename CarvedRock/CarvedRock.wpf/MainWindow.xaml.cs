using CarvedRock.Models;
using CarvedRock.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CarvedRock.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public IEnumerable<Item> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MockDataStore store = new MockDataStore();
            Items = store.GetItemsAsync().Result;
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnAddClicked(object sender, RoutedEventArgs e)
        {
            var AddWindow = new AddNewItem();
            AddWindow.ShowInTaskbar = false;
            AddWindow.Owner = Application.Current.MainWindow;
            AddWindow.ShowDialog();
            MockDataStore store = new MockDataStore(); 
            Items = store.GetItemsAsync().Result;
            // NotifyPropertyChanged("Items");
            this.DataContext = null;
            this.DataContext = this;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var detailsWindow = new ItemDetail();
            detailsWindow.Item = listview.SelectedItem as Item;
            detailsWindow.ShowInTaskbar = false;
            detailsWindow.Owner = Application.Current.MainWindow;
            detailsWindow.ShowDialog();
        }
    }
}
