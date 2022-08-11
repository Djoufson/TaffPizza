using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaffPizza.ViewModels
{
    public interface IPageServices
    {
        Task DisplayAlert(string title, string message, string ok);
    }
}
