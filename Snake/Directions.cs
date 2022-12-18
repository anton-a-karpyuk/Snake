using System.Drawing;

namespace Snake
{
    public static class Directions
    {
        public static readonly Point Up    = new Point( 0, -1 );
        public static readonly Point Down  = new Point( 0, 1 );
        public static readonly Point Left = new Point( -1, 0 );
        public static readonly Point Right = new Point( 1, 0 );

        public static readonly Point[] ChangeUp = new Point[] { Left, Right};
        public static readonly Point[] ChangeDown = new Point[] {Right, Left };
        public static readonly Point[] ChangeLeft = new Point[] {Down, Up };
        public static readonly Point[] ChangeRight = new Point[] { Up, Down };
    }
}
