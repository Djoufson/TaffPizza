using System;
using TaffPizza.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaffPizza
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.FromHex("#1abbd4")
            };
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
