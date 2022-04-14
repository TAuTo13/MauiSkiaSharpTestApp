using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public class HomePageBase : ContentPage
    {
        public ICommand NavigateCommand { private set; get; }

        public HomePageBase()
        {
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                //Type pageType=Type.GetType(str);
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            });

            BindingContext = this;
        }

    }
}
