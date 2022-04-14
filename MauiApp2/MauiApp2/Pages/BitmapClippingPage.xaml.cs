using System;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Reflection;
using System.IO;

namespace MauiApp2.Pages
{
    public partial class BitmapClippingPage : ContentPage
    {
        private SKPath keyholePath = SKPath.ParseSvgPathData(
        "M 300 130 L 250 350 L 450 350 L 400 130 A 70 70 0 1 0 300 130 Z");
       
        public BitmapClippingPage()
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

                SKRect bounds;
                keyholePath.GetTightBounds(out bounds);

                canvas.Translate(info.Width / 2, info.Height / 2);
                canvas.Scale(0.98f * info.Height / bounds.Height);
                canvas.Translate(-bounds.MidX, -bounds.MidY);

                canvas.ClipPath(keyholePath);
                
                canvas.ResetMatrix();

                canvas.DrawBitmap(bmp, rect);
            }

        }
    }
}