namespace CanvasDrawer
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

        
            int widthInts = width / 32;
            this.pixels = new uint[height][];
            for (int row = 0; row < height; row++)
            {
                this.pixels[row] = new uint[widthInts];
            }

           
            this.FillAllPixels(CanvasColor.White);
        }

        public int Width => this.pixels[0].Length * 32;

        public int Height => this.pixels.Length;

        public int RowCount => this.pixels.Length;

        public int ColCount => this.pixels[0].Length;

        public void FillAllPixels(CanvasColor color)
        {
            
            uint mask = (color == CanvasColor.White) ? 0xFFFFFFFF : 0x00000000;

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    pixels[row][col] = mask;
                }
            }
        }

        public void InvertAllPixels()
        {
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    pixels[row][col] = ~pixels[row][col]; 
                }
            }
        }


        public CanvasColor GetPixel(int row, int col)
        {
            CheckBounds(row, col);

            uint[] targetRow = pixels[row];
            int targetCol = col / 32;
            uint targetInt = targetRow[targetCol];
            int bitIndex = col % 32;
            int bit = (int)((targetInt >> bitIndex) & 1);

            return (CanvasColor)bit;
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
            for (int col = startCol; col <= endCol; col++)
            {
                SetPixel(startRow, col, color);
                SetPixel(endRow, col, color);
            }

            for (int row = startRow; row <= endRow; row++)
            {
                SetPixel(row, startCol, color);
                SetPixel(row, endCol, color);
            }
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
