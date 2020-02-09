using Foundation;
using System;
using UIKit;

namespace App1
{
    public partial class ItemDetailviewController : UIViewController
    {

        public ItemDetailviewController (IntPtr handle) : base (handle)
        {
        }

        public string ItemText { get; set; }
        public string ItemDetailText { get; set; }
        public override void ViewDidLoad()
        {
            txtItemDetail.Text = ItemText;
            txtItemDetail.AccessibilityIdentifier = ItemText;
            txtItemDetail.AccessibilityLabel = ItemText;

            txtItemDetailDescriptionText.Text = ItemDetailText;
            txtItemDetailDescriptionText.AccessibilityIdentifier = ItemDetailText;
        }
    }
}