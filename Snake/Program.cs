﻿using System;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var field = new Field(20, 2);
            var length = 5;
            var snakes = new[] { 
                new Snake(field, field.GetRandom(), length),
                new Snake(field, field.GetRandom(), length),
                /*new Snake(field, field.GetRandom(), length),
                new Snake(field, field.GetRandom(), length),
                new Snake(field, field.GetRandom(), length),*/
            };


            Draw(field);
            while (true)
            {
                Thread.Sleep(300);
                foreach (var snake in snakes) {
                    snake.ChangeDirection();
                    snake.Move(snakes);
                }
                /*if (rnd.Next(0, 10) == 0)
                    field.CreateMushroom();*/
                Draw(field);
            }
        }

        static void Draw(Field field)
        {
            Console.SetCursorPosition(0, 0);
            var row = new StringBuilder(field.Width+2);
            row.Append("+");
            row.Append(new String('=', field.Width));
            row.Append("+");
            Console.WriteLine(row.ToString());

            for (var y = 0; y < field.Height; y++)
            {
                row.Clear();
                row.Append("|");
                for (var x = 0; x < field.Width; x++)
                {
                    row.Append(DisplayPoint(field.Area[x, y]));
                }
                row.Append("|");
                Console.WriteLine(row.ToString());
            }

            row.Clear();
            row.Append("+");
            row.Append(new String('=', field.Width));
            row.Append("+");
            Console.WriteLine(row.ToString());
        }

        static string DisplayPoint(int fieldValue)
        {
            //return fieldValue.ToString();
            return fieldValue switch
            {
                0 => " ",
                Snake.TailValue => "o",
                Snake.BodyValue => "O",
                Snake.HeadValue => "@",
                -1 => "?"
            };
        }
    }
}
