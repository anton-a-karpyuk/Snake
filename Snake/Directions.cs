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
        public static readonly Point Nowhere = new Point(0, 0);

        public static Dictionary<Point, Point[]> PossibleMoves { get; }
        public static Dictionary<Point, Point[]> PossibleTurns { get; }

        public static Point[] Moves { get; }

        static Directions()
        {
            Moves = new[]{Up, Down, Left, Right};

            PossibleTurns = new Dictionary<Point, Point[]>
            {
                { Up, new Point[] { Left, Right } },
                { Down, new Point[] { Right, Left } },
                { Left, new Point[] { Down, Up } },
                { Right, new Point[] { Up, Down } },
                { Nowhere, Moves},
            };

            PossibleMoves = new Dictionary<Point, Point[]>
            {
                { Up, new Point[] { Up, Left, Right } },
                { Down, new Point[] { Down, Right, Left } },
                { Left, new Point[] { Left, Down, Up } },
                { Right, new Point[] { Right, Up, Down } },
                { Nowhere, Moves},
            };

        }


    }
}
