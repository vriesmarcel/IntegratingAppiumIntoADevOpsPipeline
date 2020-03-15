// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace App1
{
    [Register ("AddNewViewController")]
    partial class AddNewViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NewItemDetails { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNewItem { get; set; }

        [Action ("UIButton269_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton269_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (NewItemDetails != null) {
                NewItemDetails.Dispose ();
                NewItemDetails = null;
            }

            if (txtNewItem != null) {
                txtNewItem.Dispose ();
                txtNewItem = null;
            }
        }
    }
}