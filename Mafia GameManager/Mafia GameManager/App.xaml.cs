using Mafia_GameManager.Core;
using Mafia_GameManager.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mafia_GameManager
{
    public partial class App : Application
    {
        public static GameManager gameManager = new GameManager();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WelcomeView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
