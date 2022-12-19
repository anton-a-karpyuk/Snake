using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    public static class Directions
    {
        public static readonly Point Up    = new Point( 0, -1 );
        public static readonly Point Down  = new Point( 0, 1 );
        public static readonly Point Left  = new Point( -1, 0 );
        public static readonly Point Right = new Point( 1, 0 );

        public static Dictionary<Point, Point[]> PossibleMoves { get; }

        static Directions()
        {
            PossibleMoves = new Dictionary<Point, Point[]>
            {
                { Up, new Point[] { Left, Right } },
                { Down, new Point[] { Right, Left } },
                { Left, new Point[] { Down, Up } },
                { Right, new Point[] { Up, Down } },
            };
        }


    }
}
