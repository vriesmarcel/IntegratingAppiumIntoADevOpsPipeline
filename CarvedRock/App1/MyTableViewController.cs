using Foundation;
using System;
using UIKit;

namespace App1
{
    public partial class MyTableViewController : UITableViewController
    {
        public MyTableViewController (IntPtr handle) : base (handle)
        {
        }

        //public override void ViewDidLoad()
        //{
        //    this.TableView.Source = new ItemsViewSource(this);

        //}

        public override void ViewDidAppear(bool animated)
        {
            this.TableView.Source = new ItemsViewSource(this);
        }

        public override UIViewController ChildViewControllerForStatusBarStyle()
        {
            return base.ChildViewControllerForStatusBarStyle();
        }
        partial void UIBarButtonItem247_Activated(UIBarButtonItem sender)
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var addNewViewController = storyboard.InstantiateViewController("AddNewViewController") as AddNewViewController;
            addNewViewController.TableViewController = this;
             this.NavigationController.PushViewController(addNewViewController, true);

            
        }
    }
}