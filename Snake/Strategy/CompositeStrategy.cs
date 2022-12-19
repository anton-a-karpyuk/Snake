using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake.Strategy
{
    class CompositeStrategy : IStrategy
    {
        private readonly IReadOnlyCollection<IStrategy> strategies;

        public CompositeStrategy(IReadOnlyCollection<IStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes)
        {
            var directionsVote = new Dictionary<Point, int>
            {
                { Directions.Up, 0},
                { Directions.Down, 0},
                { Directions.Left, 0},
                { Directions.Right, 0},
            };

            foreach (var strategy in this.strategies)
            {
                var move = strategy.Deside(snake, field, snakes);
                if (move != null)
                    directionsVote[move.Value]++;
            }

            var direction = directionsVote
                .Where(v => v.Key == snake.Direction || Directions.PossibleMoves[snake.Direction].Contains(v.Key))
                .OrderByDescending(v => v.Value)
                .FirstOrDefault();

            if (direction.Equals(default(KeyValuePair<Point, int>)))
                return snake.Direction;

            return direction.Key;
        }
    }
}
