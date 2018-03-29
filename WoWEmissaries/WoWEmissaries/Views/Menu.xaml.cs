using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WoWEmissaries.Models;
using WoWEmissaries.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoWEmissaries.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public ListView ListView;

        public Menu()
        {
            InitializeComponent();

            BindingContext = new MenuModel();
            ListView = MenuItemsListView;
        }
    }
}