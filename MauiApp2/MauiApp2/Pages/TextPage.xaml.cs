using System;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using MauiApp2.Skia;

namespace MauiApp2.Pages
{
    public partial class TextPage : ContentPage
    {
        public TextPage()
        {
            InitializeComponent();
        }

        private void OnSKCanvasPaint(object sender,SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            string text = "Hello! SkiaSharp MAUI";

            SKPaint textPaint = SKPaints.RedPaint;
            textPaint.Typeface = SKTypeface.FromFamilyName(
                "Arial",
                SKFontStyleWeight.Bold,
                SKFontStyleWidth.Normal,
                SKFontStyleSlant.Italic);

            float textWidth = textPaint.MeasureText(text);
            textPaint.TextSize = 0.9f * info.Width * textPaint.TextSize / textWidth;

            SKRect textBounds = new SKRect();

           

            textPaint.MeasureText(text,ref textBounds);

            float textX=info.Width/2-textBounds.MidX;
            float textY=info.Height/2-textBounds.MidY;

            canvas.DrawText(text,textX,textY,textPaint);

        }
    }
}