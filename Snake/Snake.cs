using Snake.Strategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake
{
    class Snake
    {
        public const int HeadValue = 3;
        public const int BodyValue = 2;
        public const int TailValue = 1;

        public Guid Id { get; set; }

        public Point tail;
        private Point head;

        public Point Direction { get; set; }
        public Point NextDirection { get; set; }

        private readonly IStrategy strategy;
        private readonly Field field;

        private int length;
        private bool active = true;
        private readonly LinkedList<Point> movements;

        public Snake(IStrategy strategy, Field field, Point head, Point direction, int length = 3)
        {
            this.Id = Guid.NewGuid();
            this.length = length;
            this.strategy = strategy;
            this.field = field;
            this.head = head;
            this.tail = head;
            this.NextDirection = direction;
            field[head] = HeadValue;
            movements = new LinkedList<Point>();
        }

        public void Move(Snake[] snakes)
        {
            if (!this.active)
                return;
            Direction = NextDirection;
            var tryHead = new Point(this.head.X, this.head.Y);
            tryHead.Offset(Direction);
            if (tryHead.X < 0 || tryHead.X >= field.Width)
                return;
            if (tryHead.Y < 0 || tryHead.Y >= field.Height)
                return;

            var tryValue = field[tryHead];
            
            //Впереди тело, не укусить
            if (tryValue == BodyValue)
                return;

            if (tryValue == TailValue || tryValue == HeadValue)
            {
                var otherSnake = GetSnakeByTail(snakes, tryHead);
                if (otherSnake == null || otherSnake.Id == Id)
                    return;

                Bite(otherSnake);
            }

            if (tryValue < 0)
                Bite();

            movements.AddLast(Direction);
            field[this.head] = BodyValue;
            this.head = tryHead;
            if (tryValue < TailValue)
                MoveTail();
            field[this.head] = HeadValue;
        }

        public Point GetFieldAhead()
        {
            var tryHead = new Point(this.head.X, this.head.Y);
            tryHead.Offset(this.Direction);
            return tryHead;
        }

        /// <summary>
        /// Укуситься
        /// </summary>
        /// <param name="snake"></param>
        public void GotBitten()
        {
            this.length--;
            if (length > 0)
                MoveTail();
            else
                active = false;
        }

        public static Snake GetSnakeByTail(IEnumerable<Snake> snakes, Point tail)
        {
            return snakes.FirstOrDefault(s => s.tail == tail);
        }


        private void Bite(Snake snake = null)
        {
            snake?.GotBitten();
            this.length++;
        }


        private void MoveTail()
        {
            if (movements.Count >= length-1)
            {
                if (movements.Count > length-1)
                {
                    field[this.tail] = 0;
                    this.tail.Offset(movements.First.Value);
                    movements.RemoveFirst();
                }
                field[this.tail] = length > 1 ? TailValue : HeadValue;
            }
        }

        public void ChooseDirection(IReadOnlyCollection<Snake> snakes)
        {
            if (!this.active)
                return;

            var newDirection = this.strategy.Deside(this, field, snakes);
            if (newDirection != null && newDirection != this.Direction)
                NextDirection = newDirection.Value;
        }
    }
}
