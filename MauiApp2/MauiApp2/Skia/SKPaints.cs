using SkiaSharp;

namespace MauiApp2.Skia
{
    internal class SKPaints
    {
        public static SKPaint BluePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4,
            IsAntialias = true,
            Color = SKColors.Blue
        };
        public static SKPaint GreenPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4,
            IsAntialias = true,
            Color = SKColors.Green
        };
        public static SKPaint RedPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4,
            IsAntialias = true,
            Color = SKColors.Red
        };
        public static SKPaint BlueFill = new SKPaint
        {
            Style= SKPaintStyle.Fill,
            Color = SKColors.Blue
        };
        public static SKPaint GreenFill = new SKPaint
        {
            Style=SKPaintStyle.Fill,
            Color = SKColors.Green
        };
        public static SKPaint RedFill = new SKPaint
        {
            Style=SKPaintStyle.Fill,
            Color = SKColors.Red
        };
    }
}
