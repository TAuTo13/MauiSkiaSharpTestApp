using System;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using MauiApp2.Skia;

namespace MauiApp2.Pages
{
    public partial class PathPage : ContentPage
    {
        public PathPage()
        {
            InitializeComponent();
        }

        private void OnSKCanvasPaint(object sender,SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            using (SKPath path = new SKPath())
            {
                path.LineTo(83, 75);
                path.ArcTo(100, 100, 0, SKPathArcSize.Large, SKPathDirection.CounterClockwise, 83, -75);
                path.LineTo(-83, 75);
                path.ArcTo(100, 100, 0, SKPathArcSize.Large, SKPathDirection.Clockwise, -83, -75);
                path.Close();

                SKRect pathBounds = path.Bounds;

                canvas.Translate(info.Width / 2, info.Height / 2);
                canvas.Scale(Math.Min(info.Width / pathBounds.Width,
                                      info.Height / pathBounds.Height));

                SKPaint paint = SKPaints.GreenPaint;
                paint.Style = SKPaintStyle.Stroke;
               
                canvas.DrawPath(path, paint);
                
            }
        }
    }
}