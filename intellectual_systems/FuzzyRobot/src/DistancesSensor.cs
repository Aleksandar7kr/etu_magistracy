using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FuzzyRobot
{
    public class DistancesSensor
    {
        public double LeftDistance { get; set; }
        public double RightDistance { get; set; }
        public PointF LeftPoint { get; set; }
        public PointF RightPoint { get; set; }

        public Line2D PlatrormLine { get; set; }

        public LineEquation LeftEquation { get; set; }
        public LineEquation RightEquation { get; set; }

        public void Scan(List<LineEquation> wallsEquations, List<Line2D> walls) // find new distances & interstction points
        {
            var leftMeasurements = new List<PointF>();
            var rightMeasurements = new List<PointF>();
            LeftDistance = Single.MaxValue;
            RightDistance = Single.MaxValue;

            int i = 0;
            
            foreach (var line in wallsEquations)
            {
                // find all right intersections
                var pointR = LineEquation.GetIntersection(RightEquation, line);
                if (pointR != null)
                {
                    var intersection = (PointF)pointR;

                    if (
                        Line2D.PointOnLine(intersection, PlatrormLine) == PointPosition.RIGHT
                              &&
                              (Line2D.PointOnLine(intersection, walls[i]) == PointPosition.BETWEEN
                                || Line2D.PointOnLine(intersection, walls[i]) == PointPosition.ORIGIN
                                || Line2D.PointOnLine(intersection, walls[i]) == PointPosition.DESTINATION
                              )

                        )
                    {
                        rightMeasurements.Add(intersection);
                    }

                }

                // find all left intersections
                var pointL = LineEquation.GetIntersection(LeftEquation, line);
                if (pointL != null)
                {
                    var intersection = (PointF)pointL;

                    if (
                        Line2D.PointOnLine(intersection, PlatrormLine) == PointPosition.RIGHT
                              &&
                             (Line2D.PointOnLine(intersection, walls[i]) == PointPosition.BETWEEN
                                || Line2D.PointOnLine(intersection, walls[i]) == PointPosition.ORIGIN
                                || Line2D.PointOnLine(intersection, walls[i]) == PointPosition.DESTINATION
                              )
                        )
                    {
                        leftMeasurements.Add(intersection);
                    }
                }
                i++;
            }

            // find min rigth intersection
            foreach (var point in
                from point in rightMeasurements
                let temp = Math.Abs(Math.Sqrt(Math.Pow(point.X - PlatrormLine.B.X, 2) + Math.Pow(point.Y - PlatrormLine.B.Y, 2)))
                where temp <= RightDistance
                select point)
            {
                RightDistance = Math.Abs(Math.Sqrt(Math.Pow(point.X - PlatrormLine.B.X, 2) + Math.Pow(point.Y - PlatrormLine.B.Y, 2)));
                RightPoint = point;
            }

            // find min left intersection
            foreach (var point in
                from point in leftMeasurements
                let temp = Math.Abs(Math.Sqrt(Math.Pow(point.X - PlatrormLine.A.X, 2) + Math.Pow(point.Y - PlatrormLine.A.Y, 2)))
                where temp <= LeftDistance
                select point)
            {
                LeftDistance = Math.Abs(Math.Sqrt(Math.Pow(point.X - PlatrormLine.A.X, 2) + Math.Pow(point.Y - PlatrormLine.A.Y, 2)));
                LeftPoint = point;
            }
        }
    }
}
