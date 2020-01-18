using CarvedRock.Models;
using CarvedRock.Services;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace App1
{
    internal class ItemsViewSource : UITableViewSource
    {
        private UITableViewController myTableViewController;
        IEnumerable<Item> _items;
        public ItemsViewSource(UITableViewController myTableViewController)
        {
            this.myTableViewController = myTableViewController;
            _items = new MockDataStore().GetItemsAsync(true).Result;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var cell = tableView.DequeueReusableCell("RUI");
            if(cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle,"RUI");
            var items = _items.ToArray();
            
            cell.TextLabel.Text = items[indexPath.Row].Text;
            cell.DetailTextLabel.Text = items[indexPath.Row].Description;
            cell.AccessibilityIdentifier = items[indexPath.Row].Text.Replace(" ","");
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _items.Count();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var detailviewcontroller = storyboard.InstantiateViewController("ItemDetailViewController") as ItemDetailviewController;

            var items = _items.ToArray();


            detailviewcontroller.ItemDetailText = items[indexPath.Row].Description;
            detailviewcontroller.ItemText = items[indexPath.Row].Text;
            
            myTableViewController.NavigationController.PushViewController(detailviewcontroller, true);
        }
    }
}