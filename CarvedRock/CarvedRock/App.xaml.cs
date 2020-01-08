using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarvedRock.Services;
using CarvedRock.Views;

namespace CarvedRock
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
