using Mafia_GameManager.Core;
using Mafia_GameManager.Notifications;
using Plugin.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mafia_GameManager.ViewModel
{
    public class ResultVM : BaseViewModel
    {
        public bool SendOnWhatsapp { get; set; } = false;
        public ICommand Notify { private set; get; }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                SetProperty(ref _players, value)
;
            }
        }

        public ResultVM()
        {
            Notify = new Command(execute: NotifyAll);

            LoadData();
        }

        private void LoadData()
        {
            App.gameManager.InitGame();
            App.gameManager.GenerateGame();

            var playersList = App.gameManager.CurrentGameData.Players.OrderBy(x => x.GameCharacter.Name).ToList();
            Players = new ObservableCollection<Player>(playersList);
        }
        private async void NotifyAll(object obj)
        {
            bool canSendSMS = await CheckAndRequestSMSPermission();

            await Application.Current.MainPage.DisplaySnackBarAsync(GetSnackBar("Sending Notifications!"));

            foreach (var player in Players)
            {
                if (!string.IsNullOrEmpty(player.Phone))
                {
                    if (SendOnWhatsapp)
                    {
                        var ToNumber = "+91" + RemoveSpecialChars(player.Phone);
                       //var ToNumber = "+919049160129";
                        TwillioNotifier.Instance().SendMessage(ToNumber, MessageType.Whatsapp, GetMessageText(player));
                    }
                    else if (canSendSMS)
                    {
                        await SendSMS(player.Phone, GetMessageText(player));
                    }
                }
            }
 
            await Application.Current.MainPage.DisplaySnackBarAsync(GetSnackBar("Notification Sent!"));
        }

        private string GetMessageText(Player player)
        {
            string messageText = string.Format("Hey {0}, You are a {1} in this game and your job is to {2}. Good luck!",
              player.Name, player.GameCharacter.Name, player.GameCharacter.Description);
            return messageText;
        }

        private async Task<bool> CheckAndRequestSMSPermission()
        {
            bool success = false;

            try
            {
                var smsPermission = await Permissions.CheckStatusAsync<Permissions.Sms>();
                if (smsPermission != PermissionStatus.Granted)
                {
                    smsPermission = await Permissions.RequestAsync<Permissions.Sms>();
                }

                if (smsPermission == PermissionStatus.Granted)
                {
                    success = true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Permission Error", "Please grant SMS Permission to send SMS in background", "OK");
                }
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Exception Occurred", ex.Message, "OK");
            }

            return success;
        }

        private async Task<bool> SendSMS(string toNumber, string msg)
        {
            bool success = false;

            try
            {
                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                if (smsMessenger.CanSendSmsInBackground)
                {
                    smsMessenger.SendSmsInBackground(toNumber, msg);
                }
                else
                {
                    smsMessenger.SendSms(toNumber, msg);
                }
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Exception Occurred", ex.Message, "OK");
            }

            return success;
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

        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }
    }
}
