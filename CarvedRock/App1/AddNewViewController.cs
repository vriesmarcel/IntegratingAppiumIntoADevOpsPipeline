using CarvedRock.Models;
using CarvedRock.Services;
using Foundation;
using System;
using UIKit;

namespace App1
{
    public partial class AddNewViewController : UIViewController
    {
        public AddNewViewController (IntPtr handle) : base (handle)
        {
        }

        public UITableViewController TableViewController { get; internal set; }

        partial void UIButton269_TouchUpInside(UIButton sender)
        {
            var dataStore = new MockDataStore();
            var newItem = new Item() { Text = txtNewItem.Text, Description = NewItemDetails.Text };

            dataStore.AddItemAsync(newItem).Wait();

            TableViewController.TableView.ReloadData();
            TableViewController.TableView.Source = new ItemsViewSource(TableViewController);
            this.NavigationController.PopViewController(true);
        }
    }
}