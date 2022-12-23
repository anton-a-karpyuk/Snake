using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.Strategy
{
    class NoStrategy : IStrategy
    {
        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes)
        {
            return null;
        }
    }
}
