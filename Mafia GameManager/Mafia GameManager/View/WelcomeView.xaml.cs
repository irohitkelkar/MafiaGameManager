﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mafia_GameManager.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeView : ContentPage
    {
        public WelcomeView()
        {
            InitializeComponent();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}