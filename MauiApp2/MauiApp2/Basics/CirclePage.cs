using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace MauiApp2.Basics
{
    public class CirclePage : ContentPage
    {
        public CirclePage()
        {
            Title = "CirclePage";
            
        }

        private void Initialize()
        {
            GraphicsPlatform.RegisterGlobalService(SkiaGraphicsService.Instance);

        }

    }
}