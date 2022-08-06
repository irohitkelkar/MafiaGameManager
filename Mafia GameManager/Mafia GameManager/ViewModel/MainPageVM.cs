using Mafia_GameManager.Core;
using Mafia_GameManager.Notifications;
using Mafia_GameManager.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mafia_GameManager.ViewModel
{
    internal class MainPageVM : BaseViewModel
    {
        public ICommand MyCommand { private set; get; }
        public ICommand SelectPlayers { private set; get; }

        public ICommand RemovePlayer { private set; get; }
        public ICommand NextClickedCommand { private set; get; }

        public ObservableCollection<Player> Players { get; set; }

        public MainPageVM()
        {
            Title = "Select Players";

            MyCommand = new Command(execute: ExecuteMyCommand);
            SelectPlayers = new Command(execute: SelectPlayer);
            RemovePlayer = new Command(execute: RemovePlayerFromGame);
            NextClickedCommand = new Command(execute: NextClicked);

            Players = new ObservableCollection<Player>();
        }

        private void SendDummyMessage()
        {
            TwillioNotifier.Instance().SendMessage("+919049160129", MessageType.Whatsapp, "FIRST FROM CODE");
        }

        private async void NextClicked(object obj)
        {
            if(Players.Count < 5)
            {
                await Application.Current.MainPage.DisplaySnackBarAsync(GetSnackBar("Please select at least 5 player to start the game"));
                return;
            }
            else
            {
                App.gameManager.ClearPlayerList();
                App.gameManager.AddPlayers(Players.ToList());

                await Application.Current.MainPage.Navigation.PushAsync(new SelectCharactersView());
            }
        }

        private void RemovePlayerFromGame(object obj)
        {
            var player = obj as Player;
            if (Players.Contains(player))
                Players.Remove(player);
        }

        private async void SelectPlayer(object obj)
        {
            try
            {
                var contact = await Contacts.PickContactAsync();

                if (contact != null)
                {
                    var pl = Players.Where(x => x.Name.ToLower() == contact.DisplayName.ToLower()).FirstOrDefault();

                    if (pl == null)
                    {
                        Players.Add(new Player()
                        {
                            Name = contact.DisplayName,
                            Phone = contact.Phones[0].PhoneNumber
                        });
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplaySnackBarAsync(GetSnackBar("Player already added to the game"));
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Contact", "Please select a valid contact", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Permission Error", "Could not read contacts", "Try Again");
            }
        }

        private void ExecuteMyCommand(object obj)
        {
            StartGame();
        }

        private void StartGame()
        {

            GameManager gm = new GameManager()
            {
                NoOfMafia = 1,
                NoOfDoctor = 1,
                NoOfPolice = 1,
                NoOfCitizens = 3
            };

            gm.AddPlayer(new Player()
            {
                Name = "ROHIT",
                Phone = "9049160129",
            });

            gm.AddPlayer(new Player()
            {
                Name = "PRATIKSHA",
                Phone = "9049160129",
            });

            gm.AddPlayer(new Player()
            {
                Name = "MOHIT",
                Phone = "9049160129",
            });

            gm.AddPlayer(new Player()
            {
                Name = "SHIVAM",
                Phone = "9049160129",
            });

            gm.AddPlayer(new Player()
            {
                Name = "HARSH",
                Phone = "9049160129",
            });

            gm.AddPlayer(new Player()
            {
                Name = "SHEKHAR",
                Phone = "9049160129",
            });

            gm.InitGame();

            GameData data = gm.GenerateGame();

            if (data.Players != null)
            {
                foreach (var player in data.Players)
                {
                    Console.WriteLine(String.Format("{0} is {1}", player.Name, player.GameCharacter.Name));
                }
            }
            Console.WriteLine("****************************");
        }

        private SnackBarOptions GetSnackBar(string message)
        {
            return new SnackBarOptions()
            {
                MessageOptions = new MessageOptions()
                {
                    Message = message,
                    Foreground = Color.White,
                },
                BackgroundColor = (Color)Application.Current.Resources["PrimaryButton"],
                Duration = TimeSpan.FromSeconds(3),
            };
        }
    }
}
