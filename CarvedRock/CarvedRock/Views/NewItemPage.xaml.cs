using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CarvedRock.Models;
using System.Threading;

namespace CarvedRock.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            // simulate progress bar, that is random in how long it takes
            Random random = new Random();
            var numSecondsDelay =  random.Next(3, 10);
            int secondsElapsed = 0;

            progressGrid.IsVisible = true;
            double stepsize = 1d / numSecondsDelay;
            while (secondsElapsed++ != numSecondsDelay)
            {
                await progressBar.ProgressTo(secondsElapsed * stepsize, 250, Easing.Linear);
                Thread.Sleep(1000);
            }

            progressGrid.IsVisible = false;
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}