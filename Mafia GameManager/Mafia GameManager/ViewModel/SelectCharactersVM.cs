using Mafia_GameManager.Core;
using Mafia_GameManager.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace Mafia_GameManager.ViewModel
{
    public class SelectCharactersVM : BaseViewModel
    {
        public ICommand NextCommand { private set; get; }
        public int NoOfPlayers { get; set; }

        public List<int> Numbers { get; set; }
        public int NoOfMafia { get; set; } = 1;
        public int NoOfDoctor { get; set; } = 1;
        public int NoOfPolice { get; set; } = 1;
        public int NoOfCitizen { get; set; } = 2;

        public SelectCharactersVM()
        {
            Title = "Select Characters";
            NoOfPlayers = App.gameManager.NoOfPlayers;
            Numbers = new List<int>() { 1, 2, 3, 4, 5 };

            NextCommand = new Command(execute: NextClicked);
        }

        private async void NextClicked(object obj)
        {
            var count = NoOfMafia + NoOfDoctor + NoOfPolice + NoOfCitizen;

            if (count != NoOfPlayers)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Data","Total number of characters should be " + NoOfPlayers,"OK");
                return;
            }
            else
            {
                App.gameManager.NoOfMafia = NoOfMafia;
                App.gameManager.NoOfDoctor = NoOfDoctor;
                App.gameManager.NoOfPolice = NoOfPolice;
                App.gameManager.NoOfCitizens = NoOfCitizen;

                ////Generate new game 
                //App.gameManager.InitGame();
                //App.gameManager.GenerateGame();

                await Application.Current.MainPage.Navigation.PushModalAsync(new ResultView());
            }
        }


    }
}
