using System;
using System.Drawing;

namespace Snake
{
    class Field
    {
        const int randomTriesLimit = 1000000;
        public int Height { get; }
        public int Width { get; }
        public int[,] Area { get; }

        public Field(int width, int height)
        {
            Height = height;
            Width = width;
            Area = new int[Width, Height];
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
            while (Area[pos.X, pos.Y] != 0 && counter < randomTriesLimit);
            if (counter >= randomTriesLimit)
                throw new Exception("Не могу найти свободное поле");
            return pos;
        }

        public void CreateMushroom()
        {
            try
            {
                var pos = GetRandom();
                Area[pos.X, pos.Y] = -1;
            } catch (Exception e)
            {

            }
            
        }
    }
}
