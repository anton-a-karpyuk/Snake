using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.Strategy
{
    class RandomStrategy : IStrategy
    {
        private readonly static Random rnd = new Random();

        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes)
        {
            if (rnd.Next(0, 5) <= 1)
            {
                return Directions.PossibleMoves[snake.Direction][rnd.Next(0, 1)];
            }
            return snake.Direction;
        }
    }
}
