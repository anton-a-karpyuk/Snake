using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake.Strategy
{
    interface IStrategy
    {
        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes);
    }
}
