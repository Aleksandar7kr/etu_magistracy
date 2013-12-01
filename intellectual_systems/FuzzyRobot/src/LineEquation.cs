using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FuzzyRobot
{
    public class LineEquation
    {
        private double A { get; set; }
        private double B { get; set; }
        private double C { get; set; }

        public LineEquation(PointF start, PointF end)
        {
            A = end.Y - start.Y;
            B = start.X - end.X;
            C = start.X*(start.Y-end.Y)+start.Y*(end.X - start.X);
        }

        public static PointF? GetIntersection(LineEquation m, LineEquation n)
        {
            double zn = Det(m.A, m.B, n.A, n.B);
            if (Math.Abs(zn) < Constants.EPS)
                return null;

            double x = -Det(m.C, m.B, n.C, n.B) / zn;
            double y = -Det(m.A, m.C, n.A, n.C) / zn;

            return new PointF((float) x, (float) y);
        }

        private static double Det(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }
    }
}
