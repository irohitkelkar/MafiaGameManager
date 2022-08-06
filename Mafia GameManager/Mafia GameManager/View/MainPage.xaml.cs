using Mafia_GameManager.Core;
using Mafia_GameManager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mafia_GameManager
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageVM vm;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = vm = new MainPageVM();
        }

    }
}
