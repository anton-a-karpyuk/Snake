using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.Strategy
{
    class RandomStrategy : IStrategy
    {
        private const int Chance = 20;
        private readonly static Random rnd = new Random();
        
        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes)
        {
            if (rnd.Next(0, Chance) < 1)
            {
                return Directions.PossibleTurns[snake.Direction][rnd.Next(0, 1)];
            }
            return null;
        }
    }
}
