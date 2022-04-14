using System;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using MauiApp2.Skia;
using System.Reflection;
using System.IO;

namespace MauiApp2.Pages
{
    public partial class BitmapPage : ContentPage
    {
        public BitmapPage()
        {
            InitializeComponent();
        }

        private void OnSKCanvasPaint(object sender,SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            string resourceID = "MauiApp2.Media.photo.jpg";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            SKBitmap bmp;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                bmp = SKBitmap.Decode(stream);
            }

            if (bmp != null)
            {
                float scale = Math.Min((float)info.Width / bmp.Width,
                                       (float)info.Height / bmp.Height);

                float left = (info.Width - scale * bmp.Width) / 2;
                float top = (info.Height - scale * bmp.Height) / 2;
                float right = left + scale * bmp.Width;
                float bottom = top + scale * bmp.Height;
                SKRect rect = new SKRect(left, top, right, bottom);

                canvas.DrawBitmap(bmp, rect);
            }

        }
    }
}