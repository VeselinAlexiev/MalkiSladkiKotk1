﻿namespace ColoredCanvasDrawer
{
    public class DrawingCanvas : ICloneable
    {
        private byte[][] pixels;

        public DrawingCanvas(int width, int height)
        {
            if (width < 32 || width > 1024)
            {
                throw new ArgumentException("Invalid width! Width should be in range [32 ... 1024].");
            }

            if (height < 32 || height > 1024)
            {
                throw new ArgumentException("Invalid height! Height should be in range [32 ... 1024].");
            }

            int widthColor = width * 3;
            this.pixels = new byte[height][];
            for (int row = 0; row < height; row++)
            {
                this.pixels[row] = new byte[widthColor];
            }

            this.FillAllPixels();
        }

        public int Height => this.pixels.Length;

        public int Width => this.pixels[0].Length;

        public void FillAllPixels()
        {
            byte mask = 0;
            mask = (byte)~mask;

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    this.pixels[row][col] = mask;
                }
            }
        }

        public void InvertAllPixels()
        {
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    this.pixels[row][col] = (byte)~this.pixels[row][col];
                }
            }
        }

        public Color GetPixel(int row, int col)
        {
            if (row < 0 || row >= this.Height || col < 0 || col >= this.Width)
            {
                throw new ArgumentOutOfRangeException("Pixel coordinates are out of bounds.");
            }

            int offset = col * 3;
            return Color.FromArgb(pixels[row][offset], pixels[row][offset + 1], pixels[row][offset + 2]);
        }

        public void SetPixel(int row, int col, Color color)
        {        
            if (row < 0 || row >= this.Height || col < 0 || col >= this.Width)
            {
                throw new ArgumentOutOfRangeException("Pixel coordinates are out of bounds.");
            }     
            int offset = col * 3;        
            this.pixels[row][offset] = color.R;     
            this.pixels[row][offset + 1] = color.G;   
            this.pixels[row][offset + 2] = color.B;   
        }


        public void DrawHorizontalLine(int row, int startCol, int endCol, Color color)
        {
            for (int col = startCol; col < endCol; col++)
            {
                this.SetPixel(row, col, color);
            }
        }

        public void DrawVerticalLine(int col, int startRow, int endRow, Color color)
        {
            for (int row = startRow; row < endRow; row++)
            {
                this.SetPixel(row, col, color);
            }
        }

        public void DrawDiagonalLine(int startCol, int startRow, int endCol,
            int endRow, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(int startRow, int startCol, int endRow, int endCol, Color color)
        {
            this.DrawHorizontalLine(startRow, startCol, endCol, color);
            this.DrawHorizontalLine(endRow, startCol, endCol, color);
            this.DrawVerticalLine(startCol, startRow, endRow, color);
            this.DrawVerticalLine(endCol, startRow, endRow, color);
        }

        public void DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            plotLine(x1, y1, x2, y2);

            plotLine(x2, y2, x3, y3);

            plotLine(x3, y3, x1, y1);
        }

        private void PlotLineLow(int startCol, int startRow, int endCol, 
            int endRow, Color color)
        {
            throw new NotImplementedException();
        }

        private void PlotLineHigh(int startCol, int startRow, int endCol, 
            int endRow, Color color)
        {
            throw new NotImplementedException();
        }

        private void CheckBounds(int height, int width)
        {
            int maxWidth = this.Width * 3 - 1;
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
            DrawingCanvas clone = new DrawingCanvas(this.Width / 3, this.Height);

            for (int row = 0; row < this.Height; row++)
            {
                Array.Copy(this.pixels[row], clone.pixels[row], this.pixels[row].Length);
            }

            return clone;
        }
    }
}
