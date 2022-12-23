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
            var directionsVote = Directions.Moves.ToDictionary(m => m, x => 0);
            foreach (var strategy in this.strategies)
            {
                var move = strategy.Deside(snake, field, snakes);
                if (move != null)
                    directionsVote[move.Value]++;
            }

            var direction = directionsVote
                .Where(v => Directions.PossibleMoves[snake.Direction].Contains(v.Key))
                .OrderByDescending(v => v.Value)
                .FirstOrDefault();

            if (direction.Equals(default(KeyValuePair<Point, int>)))
                return null;

            return direction.Key;
        }
    }
}
