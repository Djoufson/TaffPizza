using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaffPizza.ViewModels
{
    public class PageServices : IPageServices
    {
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }
    }
}
