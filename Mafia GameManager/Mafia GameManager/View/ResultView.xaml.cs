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
    public partial class ResultView : ContentPage
    {
        readonly ResultVM vm;
        public ResultView()
        {
            InitializeComponent();
            BindingContext = vm = new ResultVM();
        }
    }
}