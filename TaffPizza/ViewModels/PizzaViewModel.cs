using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Windows.Input;
using TaffPizza.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TaffPizza.ViewModels
{
    public class PizzaViewModel : BaseViewModel
    {
        private string path;
        private HttpClient client;
        private int indexer;
        private const string indexerKey = "indexer";
        private ObservableCollection<Pizza> pizzas;
        private Pizza selectedPizza;
        private bool isRefreshing;
        private string[] resourceIdStrings;
        private string resourceIdString;
        private ImageSource resourceId;
        public ICommand RefreshCommand { get; private set; }
        public ICommand SortPizzaCommand { get; private set; }
        private readonly IPageServices _pageServices;
        public ImageSource ResourceId
        {
            get
            {
                if (!String.IsNullOrEmpty(resourceIdString))
                {
                    resourceId = ImageSource.FromResource(resourceIdString);
                    return resourceId;
                }

                return null;
            }
            set
            {
                SetValue(ref resourceId, value);
            }
        }

        public bool _IsRefreshing 
        {
            get => isRefreshing;
            set
            {
                SetValue(ref isRefreshing, value);
            }
        }
        public Pizza SelectedPizza 
        {
            get => selectedPizza;
            set => SetValue(ref selectedPizza, value);
        }
        public ObservableCollection<Pizza> Pizzas
        {
            get => pizzas;
            set => SetValue(ref pizzas, value);
        }

        // CONSTRUCTOR //
        public PizzaViewModel(IPageServices pageServices, out string errMessage)
        {
            isRefreshing = false;
            _pageServices = pageServices;
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pizzas.json");
            indexer = 0;
            if (Application.Current.Properties.ContainsKey(indexerKey))
            {
                indexer = (int)Application.Current.Properties[indexerKey];
            }
            else
            {
                Application.Current.Properties[indexerKey] = indexer;
                Application.Current.SavePropertiesAsync();
            }
            resourceIdStrings = new string[] { "TaffPizza.Images.sort_none.png", "TaffPizza.Images.sort_nom.png", "TaffPizza.Images.sort_prix.png", "TaffPizza.Images.sort_fav.png" };
            resourceIdString = resourceIdStrings[indexer];
            pizzas = new ObservableCollection<Pizza>();
            RefreshCommand = new Command(OnRefreshing);
            SortPizzaCommand = new Command(OnSortPizza);
            var resp = InitializePizzas().Result;
            Console.WriteLine(resp);
            errMessage = resp;
        }

        public void OnRefreshing()
        {
            Pizzas = new ObservableCollection<Pizza>();
            var resp = InitializePizzas();
        }
        public async void OnSortPizza()
        {
            await _pageServices.DisplayAlert("Error", "", "OK");
            indexer++;
            if(indexer >= resourceIdStrings.Length)
            {
                indexer = 0;
            }
            resourceIdString = resourceIdStrings[indexer];
            ResourceId = ImageSource.FromResource(resourceIdString);
            Application.Current.Properties[indexerKey] = indexer;
            await Application.Current.SavePropertiesAsync();
            Pizzas.Sort(indexer);
        }
        private async Task<string> InitializePizzas()
        {
            string message = "";
            client = new HttpClient();
            const string Url = "https://codeavecjonathan.com/res/pizzas_app_1.json";

            try
            {
                using (var file = await client.GetStreamAsync(Url).ConfigureAwait(false))
                using (var jsonStream = File.Create(path))
                {
                    await file.CopyToAsync(jsonStream);
                }
            }
            catch
            {
                Console.WriteLine("=========== There is no Internet connexion ===========");
                message += "You are not connected";
            }
            try
            {
                string result = File.ReadAllText(path);
                Pizzas = JsonConvert.DeserializeObject<ObservableCollection<Pizza>>(result);
                Pizzas.Sort(indexer);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\n========= An error occured while attempting to read the Json file ==========");
                message += "\nAn error occured while attempting to update the content";
            }

            isRefreshing = false;
            if (!String.IsNullOrEmpty(message))
            {
                try
                {
                    await _pageServices.DisplayAlert("No Connection", message, "OK");
                }
                catch
                {
                    Console.WriteLine("Impossible to reach the static Engine");
                }
            }
            return message;
        }
    }
}
