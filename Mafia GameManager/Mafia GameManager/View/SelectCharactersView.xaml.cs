using Mafia_GameManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mafia_GameManager.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCharactersView : ContentPage
    {
        readonly SelectCharactersVM vm;
        public SelectCharactersView()
        {
            InitializeComponent();
            BindingContext = vm = new SelectCharactersVM();
        }
    }
}