using System;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using MauiApp2.Skia;
using MauiApp2.Items;
using System.Diagnostics;
using System.Text;

namespace MauiApp2.Pages
{
    public partial class PolygonPage : ContentPage
    {
       
        private DrawPolygonMethods selectedMethod { get; set; }

        private Boolean FillPolygon { get; set; }

        public PolygonPage()
        {
            InitializeComponent();
        }

        private void OnDrawButtonPressed(object sender,EventArgs e)
        {
            if (skiaView != null)
            {
                skiaView.InvalidateSurface();
            }
        }

        private void OnSelectionChanged(object sender,EventArgs e)
        {
            selectedMethod = (DrawPolygonMethods)((Picker)sender).SelectedIndex;

            if (FillCheckBox != null && FillCheckLabel != null)
            {
                if (selectedMethod == DrawPolygonMethods.DrawPath)
                {
                    FillCheckLabel.IsVisible = true;
                    FillCheckBox.IsVisible = true;
                    FillCheckBox.IsEnabled = true;
                }
                else
                {
                    FillCheckLabel.IsVisible = false;
                    FillCheckBox.IsVisible = false;
                    FillCheckBox.IsEnabled = false;
                }
            }
        }

        private void OnCheckChanged(object sender,EventArgs e)
        {
            FillPolygon = FillCheckBox.IsChecked;
        }

        private void OnSKCanvasPaint(object sender,SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            if (PolyNumEntry.Text != null && VertexNumEntry.Text != null)
            {
                TimeCountLabel.Text = "Drawing";

                int polyNum = Int32.Parse(PolyNumEntry.Text);
                int vertexNum = Int32.Parse(VertexNumEntry.Text);
    

                var paint = SKPaints.RedPaint;
                paint.Style = SKPaintStyle.StrokeAndFill;

                int ofs = 2;
                float width = info.Width - ofs * 2;
                float height = info.Height - ofs * 2;
                float right = width + ofs;
                float bottom = height + ofs;

                float n = MathF.Sqrt(width * height / polyNum);
                int w_num = (int)(width / n);
                int h_num = (int)(height / n);

                int k = 1;
                while (w_num * h_num < polyNum)
                {
                    n = width / (w_num + k);
                    w_num = (int)(width / n);
                    h_num = (int)(height / n);
                    k++;
                }

                float r = n / 2 - ofs;

                SKPoint[] centerPoints = new SKPoint[polyNum];

                k = 0;
                for (float x = ofs; x + n <= right && k < polyNum; x += n)
                {
                    for (float y = ofs; y + n <= bottom && k < polyNum; y += n)
                    {
                        SKPoint pt = new SKPoint(x + r, y + r);
                        centerPoints[k++] = pt;
                    }
                }

                Stopwatch sp=new Stopwatch();

                SKPoint[][] vertexPoints=new SKPoint[polyNum][];
                if (selectedMethod == DrawPolygonMethods.DrawPoints)
                {
                    for (int i = 0; i < polyNum; i++)
                    {
                        vertexPoints[i] = new SKPoint[vertexNum + 1];
                        float cx = centerPoints[i].X;
                        float cy = centerPoints[i].Y;

                        for (int j = 0; j < vertexNum; j++)
                        {
                            float px = r * MathF.Cos((2 * j * MathF.PI) / vertexNum) + cx;
                            float py = r * MathF.Sin((2 * j * MathF.PI) / vertexNum) + cy;
                            vertexPoints[i][j] = new SKPoint(px, py);
                        }
                        vertexPoints[i][vertexNum] = vertexPoints[i][0];
                    }

                    sp.Start();
                    for (int i = 0; i < polyNum; i++)
                    {
                        canvas.DrawPoints(SKPointMode.Polygon, vertexPoints[i], paint);
                    }
                    sp.Stop();
                }else if(selectedMethod == DrawPolygonMethods.DrawPath)
                {
                    for (int i = 0; i < polyNum; i++)
                    {
                        vertexPoints[i] = new SKPoint[vertexNum];
                        float cx = centerPoints[i].X;
                        float cy = centerPoints[i].Y;

                        for (int j = 0; j < vertexNum; j++)
                        {
                            float px = r * MathF.Cos((2 * j * MathF.PI) / vertexNum) + cx;
                            float py = r * MathF.Sin((2 * j * MathF.PI) / vertexNum) + cy;
                            vertexPoints[i][j] = new SKPoint(px, py);
                        }
                    }

                    SKPath[] paths = new SKPath[polyNum];
                    for(int i = 0;i < polyNum; i++)
                    {
                        paths[i] = new SKPath();
                        paths[i].MoveTo(vertexPoints[i][0]);
                        for (int j= 0;j < vertexNum-1; j++)
                        {
                            paths[i].LineTo(vertexPoints[i][j+1]);
                        }
                        paths[i].LineTo(vertexPoints[i][0]);
                        paths[i].Close();
                    }

                    if (FillPolygon)
                    {
                        paint = SKPaints.BlueFill;
                    }
                    else
                    {
                        paint = SKPaints.BluePaint;
                    }
                    sp.Start();
                    for (int i = 0; i < polyNum; i++)
                    {
                        canvas.DrawPath(paths[i], paint);
                    }
                    sp.Stop();
                }

                TimeSpan ts = sp.Elapsed;

                StringBuilder sb=new StringBuilder();

                sb.Append("Time:");
                sb.Append(ts.Minutes);
                sb.Append("m");
                sb.Append(ts.Seconds);
                sb.Append("s");
                sb.Append(ts.Milliseconds);
                sb.Append("ms");

                TimeCountLabel.Text = sb.ToString();
            }

        }
    }
}