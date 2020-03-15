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
    [Register ("ItemDetailviewController")]
    partial class ItemDetailviewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtItemDetail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtItemDetailDescriptionText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (txtItemDetail != null) {
                txtItemDetail.Dispose ();
                txtItemDetail = null;
            }

            if (txtItemDetailDescriptionText != null) {
                txtItemDetailDescriptionText.Dispose ();
                txtItemDetailDescriptionText = null;
            }
        }
    }
}