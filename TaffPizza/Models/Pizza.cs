using System;
using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;
using TaffPizza.ViewModels;
using Xamarin.Forms;

namespace TaffPizza.Models
{
    public class Pizza : BaseViewModel, IComparable
    {
        private string imageUrl;
        public ICommand StarCommand { get; private set; }

        [JsonProperty("nom")]
        public string Nom { get; set; }

        [JsonProperty("prix")]
        public int Prix { get; set; }

        [JsonProperty("ingredients")]
        public string[] _Ingredients { get; set; }
        private string ingredients;
        public bool IsFavourite { get; set; }
        public string Ingredients
        {
            get
            {
                if(_Ingredients != null)
                {
                    string result = _Ingredients[0];
                    if (_Ingredients.Length > 1)
                    {
                        for (int i = 1; i < _Ingredients.Length; i++)
                        {
                            result += $", {_Ingredients[i]}";
                        }
                    }
                    return result;
                }
                return null;
            }
            set
            {
                ingredients = value;
            }
        }
        [JsonProperty("imageUrl")]
        public string ImageUrl 
        {
            get => imageUrl;
            set
            {
                imageUrl = value;
                _ImageSource = new UriImageSource() { Uri = new Uri(imageUrl) };
                _ImageSource.CachingEnabled = true;
                _ImageSource.CacheValidity = TimeSpan.FromDays(1);
            }
        }

        public UriImageSource _ImageSource { get; set; }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Pizza otherPizza = obj as Pizza;
            if (otherPizza != null)
                return this.Nom.CompareTo(otherPizza.Nom);
            else
                throw new ArgumentException("Object is not a Pizza");
        }

        // CONSTRUCTOR //
        public Pizza()
        {
            StarCommand = new Command(OnStarClicked);
        }
        void OnStarClicked()
        {
            Console.WriteLine("Clicked");
            IsFavourite = !IsFavourite;
        }
    }
}
