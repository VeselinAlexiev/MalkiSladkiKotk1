﻿namespace CanvasDrawer
{
    public class DrawingCanvas : ICloneable
    {
        private uint[][] pixels;

        public DrawingCanvas(int width, int height)
        {
            if (width < 32 || width > 1024 || width % 32 != 0)
            {
                throw new ArgumentException("Invalid width! Width should be in range [32 ... 1024] and should be divisible by 32.");
            }

            if (height < 32 || height > 1024)
            {
                throw new ArgumentException("Invalid height! Height should be in range [32 ... 1024].");
            }

            // Allocate the pixels in the image
            int widthInts = width / 32;
            this.pixels = new uint[height][];
            for (int row = 0; row < height; row++)
            {
                this.pixels[row] = new uint[widthInts];
            }

            // Uncomment when implemented
            //this.FillAllPixels(CanvasColor.White);
        }

        public int Width => this.pixels[0].Length * 32;

        public int Height => this.pixels.Length;

        public int RowCount => this.pixels.Length;

        public int ColCount => this.pixels[0].Length;

        public void FillAllPixels(CanvasColor color)
        {
            throw new NotImplementedException();
        }

        public void InvertAllPixels()
        {
            throw new NotImplementedException();
        }

        public CanvasColor GetPixel(int row, int col)
        {
            throw new NotImplementedException();
        }

        public void SetPixel(int row, int col, CanvasColor color)
        {
            if (row < 0 || row >= Height || col < 0 || col >= Width)
                throw new ArgumentOutOfRangeException(nameof(row), "Pixel coordinates are out of bounds.");

            int byteIndex = col / 8;
            int bitIndex = 7 - (col % 8);
            byte mask = (byte)(1 << bitIndex);

            if (color == CanvasColor.White)
                pixels[row][byteIndex] |= mask; 
            else
                pixels[row][byteIndex] ^= mask;
        }

        public void DrawHorizontalLine(int row, int startCol, int endCol, 
            CanvasColor color)
        {
            for (int col = startCol; col <= endCol; col++)
            {
                SetPixel(row, col, color);
            }
        }

        public void DrawVerticalLine(int col, int startRow, int endRow, 
            CanvasColor color)
        {
            for (int row = startRow; row <= endRow; row++)
            {
                SetPixel(row, col, color);
            }
        }

        public void DrawRectangle(int startRow, int startCol, int endRow, int endCol, 
            CanvasColor color)
        {
            throw new NotImplementedException();
        }

        private void CheckBounds(int height, int width)
        {
            int maxWidth = this.Width - 1;
            if (width < 0 || width > maxWidth)
            {
                throw new ArgumentException($"Invalid width! Width should be in range [0 ... {maxWidth}].");
            }

            int maxHeight = this.Height - 1;
            if (height < 0 || height > maxHeight)
            {
                throw new ArgumentException($"Invalid height! Height should be in range [0 ... {maxHeight}].");
            }
        }

        public object Clone()
        {
            DrawingCanvas clone = new DrawingCanvas(this.Width, this.Height);

            for (int row = 0; row < this.RowCount; row++)
            {
                Array.Copy(this.pixels[row], clone.pixels[row], this.pixels[row].Length);
            }

            return clone;
        }
    }
}
