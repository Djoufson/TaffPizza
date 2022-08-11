using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TaffPizza.ViewModels;
using Xamarin.Forms;
using TaffPizza.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace TaffPizza
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            string errMessage;
            BindingContext = new PizzaViewModel(new PageServices(), out errMessage);
            if(App.Current.MainPage == null)
                Console.WriteLine("Null");
            InitializeComponent();
            if (!String.IsNullOrEmpty(errMessage))
                DisplayAlert("No Connection", errMessage, "OK");
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        private void StarClicked(object sender, EventArgs e)
        {
            ((ImageButton)sender).Source = "star2.png";
        }
    }
}
