using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake.Strategy
{
    class AvoidObstaclesStrategy : IStrategy
    {
        public Point? Deside(Snake snake, Field field, IReadOnlyCollection<Snake> snakes)
        {
            var pointAhead = snake.GetAhead();
            var notAvailableDirections = new List<Point>();

            try
            {
                if (field[pointAhead] == Snake.BodyValue || Snake.GetSnake(snakes, pointAhead)?.Id == snake.Id)
                    notAvailableDirections.Add(snake.Direction);
            }
            catch (Exception e)
            {

            }

            if (pointAhead.X < 0)
                notAvailableDirections.Add(Directions.Left);

            if (pointAhead.Y < 0)
                notAvailableDirections.Add(Directions.Up);

            if (pointAhead.X >= field.Width)
                notAvailableDirections.Add(Directions.Right);

            if (pointAhead.Y >= field.Height)
                notAvailableDirections.Add(Directions.Down);

            var directions = new List<Point> { snake.Direction };
            directions.AddRange(Directions.PossibleMoves[snake.Direction]);
            return directions
                .Where(d => !notAvailableDirections.Contains(d))
                .FirstOrDefault();
        }
    }
}
