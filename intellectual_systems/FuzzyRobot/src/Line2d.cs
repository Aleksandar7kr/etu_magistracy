using System;
using System.Drawing;
using FuzzyRobot.Annotations;

namespace FuzzyRobot
{
    public class Line2D
    {
        [NotNull]
        public PointF A { get; set; }

        [NotNull]
        public PointF B { get; set; }

        public Line2D(PointF a, PointF b)
        {
            A = a;
            B = b;
        }

        public static PointPosition PointOnLine(PointF point, Line2D line)
        {
            PointF a = new PointF(line.B.X - line.A.X, line.B.Y - line.A.Y);
            PointF b = new PointF(point.X - line.A.X, point.Y - line.A.Y);

            double sa = a.X * b.Y - b.X * a.Y;
            if (sa > 0.0)
                return PointPosition.LEFT;
            if (sa < 0.0)
                return PointPosition.RIGHT;
            if ((a.X * b.X < 0.0) || (a.Y * b.Y < 0.0))
                return PointPosition.BEHIND;
            if (Length(a) < Length(b))
                return PointPosition.BEYOND;
            if (Math.Abs(line.A.X - point.X) < Constants.EPS && Math.Abs(line.A.Y - point.Y) < Constants.EPS)
                return PointPosition.ORIGIN;
            if (Math.Abs(line.B.X - point.X) < Constants.EPS && Math.Abs(line.B.Y - point.Y) < Constants.EPS)
                return PointPosition.DESTINATION;
            return PointPosition.BETWEEN;
        }

        private static double Length(PointF point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }
    }
}
