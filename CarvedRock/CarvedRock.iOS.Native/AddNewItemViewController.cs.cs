using CarvedRock.Models;
using CarvedRock.Services;
using Foundation;
using System;
using UIKit;

namespace CarvedRock.iOS.Native
{
    public partial class AddNewItemViewController : UIViewController
    {
        UITableViewController parent = null;
        public AddNewItemViewController (IntPtr handle, MasterViewController masterViewController) : base (handle)
        {
            parent = masterViewController;
        }

        partial void UIButton576_TouchUpInside(UIButton sender)
        {
            var newItem = new Item()
            {
                Text = ItemText.Text,
                Description = ItemDescription.Text,
                Id = new Guid().ToString()
            };

            var success = new MockDataStore().AddItemAsync(newItem).Result;
            parent.NavigationController.DismissViewController(true, null);
        }
    }
}