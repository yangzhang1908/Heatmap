﻿using System;
using System.Drawing;
using System.Numerics;

namespace Heatmap.Receivers
{
    public class BitmapSimpleReceiver : IReceiver, IDisposable
    {
        public Vector2 SampleSize { get; private set; }
        public Bitmap Result { get; private set; }

        private Vector2 BitmapSize;
        private Graphics Context;

        public BitmapSimpleReceiver(Size bitmapSize, Size sampleSize)
        {
            BitmapSize = new Vector2(bitmapSize.Width, bitmapSize.Height);
            Result = new Bitmap(bitmapSize.Width, bitmapSize.Height);

            Context = Graphics.FromImage(Result);
            Context.Clear(Color.Transparent);

            SampleSize = new Vector2(sampleSize.Width / (float)bitmapSize.Width, sampleSize.Height / (float)bitmapSize.Height);
        }

        public void Receive(Vector2 position, Vector2 size, Color color)
        {
            Vector2 bitmapPosition = position * BitmapSize;
            Vector2 bitmapSize = size * BitmapSize;

            using (Brush brush = new SolidBrush(color))
                Context.FillRectangle(brush, bitmapPosition.X, bitmapPosition.Y, bitmapSize.X, bitmapSize.Y);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
