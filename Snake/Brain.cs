using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Snake.Strategy;

namespace Snake
{
    static class Brain
    {
        public static Point Resolve(Field field, IReadOnlyCollection<Snake> snakes, Snake thisSnake)
        {
            var directionsVote = new Dictionary<Point, int>
            {
                { Directions.Up, 0},
                { Directions.Down, 0},
                { Directions.Left, 0},
                { Directions.Right, 0},
            };
            var otherSnakes = snakes.Where(s => s.Id != thisSnake.Id).ToArray();

            var move = new RandomStrategy().Deside(thisSnake, field, snakes);
            if (move != null)
                directionsVote[move.Value]++;


            var direction = directionsVote
                .Where(v => v.Key == thisSnake.Direction || Directions.PossibleMoves[thisSnake.Direction].Contains(v.Key))
                .OrderByDescending(v => v.Value)
                .FirstOrDefault();

            if (direction.Equals(default(KeyValuePair<Point, int>)))
                return thisSnake.Direction;

            return direction.Key;
        }

    }
}
