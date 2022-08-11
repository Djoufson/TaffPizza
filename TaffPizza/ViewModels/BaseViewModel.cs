using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace TaffPizza.ViewModels
{
    public class BaseViewModel : BindableObject
    {        
        protected void SetValue<T>(ref T objectModel, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Comparer.Equals(objectModel, value))
            {
                objectModel = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
