﻿using AppTesteBinding.Models;
using AppTesteBinding.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Maragogi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePraiasDetails : ContentPage
    {
        public PagePraiasDetails(Praias praias)
        {
            InitializeComponent();

            BindingContext = new PraiasDetailsViewModel(praias);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}