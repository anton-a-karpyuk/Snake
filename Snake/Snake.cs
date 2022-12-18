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

        public Point Direction { get; set; } = Directions.Right;
        public Point NextDirection { get; set; } = Directions.Right;

        private Field field;

        private int length;
        private bool active = true;
        private LinkedList<Point> movements;

        public Snake(Field field, Point head, int length = 3)
        {
            this.Id = Guid.NewGuid();
            this.length = length;
            this.field = field;
            this.head = head;
            field.Area[head.X, head.Y] = HeadValue;
            this.tail = head;
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

            var tryValue = field.Area[tryHead.X, tryHead.Y];
            
            //Впереди тело, не укусить
            if (tryValue == BodyValue)
                return;

            if (tryValue == TailValue || tryValue == HeadValue)
            {
                var otherSnake = GetSnake(snakes, tryHead);
                if (otherSnake == null || otherSnake.Id == Id)
                    return;

                Bite(otherSnake);
            }

            if (tryValue < 0)
                Bite();

            movements.AddLast(Direction);
            field.Area[this.head.X, this.head.Y] = BodyValue;
            this.head = tryHead;
            field.Area[this.head.X, this.head.Y] = HeadValue;
            if (tryValue < TailValue)
                MoveTail();
            field.Area[tryHead.X, tryHead.Y] = 3;
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
            active = false;
        }

        private void Bite(Snake snake = null)
        {
            snake?.GotBitten();
            this.length++;
        }

        private Snake GetSnake(IEnumerable<Snake> snakes, Point end)
        {
            return snakes.FirstOrDefault(s => s.tail == end);
        }

        private void MoveTail()
        {
            if (movements.Count >= length-1)
            {
                if (movements.Count > length-1)
                {
                    field.Area[this.tail.X, this.tail.Y] = 0;
                    this.tail.Offset(movements.First.Value);
                    movements.RemoveFirst();
                }
                field.Area[this.tail.X, this.tail.Y] = length > 1 ? TailValue : HeadValue;
            }
        }

        public void ChangeDirection()
        {
            Random rnd = new Random();
            if (rnd.Next(0, 5) <= 1)
            {
                var newDir = rnd.Next(0, 1);
                if (Direction.Equals(Directions.Up))
                    NextDirection = Directions.ChangeUp[newDir];
                if (Direction.Equals(Directions.Down))
                    NextDirection = Directions.ChangeDown[newDir];
                if (Direction.Equals(Directions.Left))
                    NextDirection = Directions.ChangeLeft[newDir];
                if (Direction.Equals(Directions.Right))
                    NextDirection = Directions.ChangeRight[newDir];
            }
        }
    }
}
