using System;
using System.Drawing;

namespace Snake
{
    class Field
    {
        const int randomTriesLimit = 1000000;
        public int Height { get; }
        public int Width { get; }
        private int[,] area;
        public int this[int x, int y]
        {
            get { return GetField(x,y); }
            set { SetField(x, y, value); }
        }

        public int this[Point p]
        {
            get { return GetField(p); }
            set { SetField(p, value); }
        }

        public Field(int width, int height)
        {
            Height = height;
            Width = width;
            area = new int[Width, Height];
        }

        public Point GetRandom()
        {
            Random rnd = new Random();
            Point pos;
            int counter = 0;
            do
            {
                pos = new Point(rnd.Next(0, Width), rnd.Next(0, Height));
                counter++;
            }
            //Алгоритм можно улучшить
            while (this[pos] != 0 && counter < randomTriesLimit);
            if (counter >= randomTriesLimit)
                throw new Exception("Не могу найти свободное поле");
            return pos;
        }

        public void CreateMushroom()
        {
            try
            {
                this[GetRandom()] = -1;
            } catch (Exception e)
            {

            }
            
        }

        private int GetField(int x, int y)
        {
            return area[x, y];
        }

        private int GetField(Point point)
        {
            return area[point.X, point.Y];
        }

        private void SetField(int x, int y, int value)
        {
            area[x, y] = value;
        }

        private void SetField(Point point, int value)
        {
            area[point.X, point.Y] = value;
        }
    }
}
