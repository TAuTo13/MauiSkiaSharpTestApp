using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using MauiApp2.Skia;
using MauiApp2.Items;

namespace MauiApp2.Pages
{
	public partial class BasicShapesPage : ContentPage
	{
        public BasicShapesPage()
		{
            Debug.WriteLine("CircleSetup");
            InitializeComponent();
		}

        private BasicShapes shapeVal { get; set; }
        private void OnShapeSelectionChanged(object sender, EventArgs e)
        {
            shapeVal = (BasicShapes)((Picker)sender).SelectedIndex;
            if (skiaView != null)
            {
                skiaView.InvalidateSurface();
            }
        }


        private void OnSKCanvasPaint(object sender,SKPaintSurfaceEventArgs e)
        {
            //SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            SKPaint shapePaint = SKPaints.RedPaint;
            switch (shapeVal)
            {
                case BasicShapes.Circle:
                    canvas.DrawCircle(110, 110, 100, shapePaint);
                    break;
                case BasicShapes.Rectangle:
                    canvas.DrawRect(10, 10, 200, 200, shapePaint);
                    break;
                case BasicShapes.RoundedRectangle:
                    canvas.DrawRoundRect(10, 10, 200, 200, 10, 10, shapePaint);
                    break;
                case BasicShapes.Oval:
                    canvas.DrawOval(150, 110, 120, 100, shapePaint);
                    break;
                case BasicShapes.Line:
                    canvas.DrawLine(10, 10, 200, 200, shapePaint);
                    canvas.DrawLine(10, 200, 200, 10, shapePaint);
                    break;
                default:
                    canvas.Clear();
                    break;
            }
        }
        
    }
}
