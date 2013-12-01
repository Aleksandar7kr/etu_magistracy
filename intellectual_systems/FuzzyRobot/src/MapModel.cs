using System.Collections.Generic;
using System.Drawing;

namespace FuzzyRobot
{
    class MapModel
    {
        private int[,] map;
        private List<Line2D> walls;
        private List<LineEquation> wallsEquations; 

        public MapModel(int[,] map)
        {
            this.map = map;
            walls = new List<Line2D>();
            wallsEquations = new List<LineEquation>();
            
            float I = GetWidth()*Constants.CellSize;
            float J = GetHeight()*Constants.CellSize;

            // top
            walls.Add(new Line2D(new PointF(Constants.CellSize, Constants.CellSize), new PointF((float) (I-Constants.CellSize), Constants.CellSize)));
            // bottom
            walls.Add(new Line2D(new PointF(Constants.CellSize, (float) (J-Constants.CellSize)), new PointF((float) (I-Constants.CellSize), (float) (J-Constants.CellSize))));
            // left
            walls.Add(new Line2D(new PointF(Constants.CellSize, Constants.CellSize), new PointF(Constants.CellSize, (float) (J-Constants.CellSize))));
            // right
            walls.Add(new Line2D(new PointF((float) (I-Constants.CellSize), Constants.CellSize), new PointF((float) (I-Constants.CellSize), (float) (J - Constants.CellSize))));

            // top
            wallsEquations.Add(new LineEquation(new PointF(Constants.CellSize, Constants.CellSize), new PointF((float) (I - Constants.CellSize), Constants.CellSize)));
            // bottom
            wallsEquations.Add(new LineEquation(new PointF(Constants.CellSize, (float) (J - Constants.CellSize)), new PointF((float) (I - Constants.CellSize), (float) (J - Constants.CellSize))));
            // left
            wallsEquations.Add(new LineEquation(new PointF(Constants.CellSize, Constants.CellSize), new PointF(Constants.CellSize, (float) (J - Constants.CellSize))));
            // right
            wallsEquations.Add(new LineEquation(new PointF((float) (I - Constants.CellSize), Constants.CellSize), new PointF((float) (I - Constants.CellSize), J - Constants.CellSize)));

            // add squares
            for (int i = 1; i < GetWidth() - 1; i++)
            {
                for (int j = 1; j < GetHeight() - 1; j++)
                {
                    if (map[i, j] == 1)
                    {
                        I = i*Constants.CellSize;
                        J = j*Constants.CellSize;
                        // top
                        walls.Add(new Line2D(new PointF(I,J), new PointF((float) (I+Constants.CellSize),J)));
                        // bottom
                        walls.Add(new Line2D(new PointF(I,J+Constants.CellSize), new PointF(I+Constants.CellSize,J+Constants.CellSize)));
                        // left
                        walls.Add(new Line2D(new PointF(I, J), new PointF(I, J+Constants.CellSize)));
                        // rIght
                        walls.Add(new Line2D(new PointF(I+Constants.CellSize, J), new PointF(I+Constants.CellSize, J + Constants.CellSize)));


                        wallsEquations.Add(new LineEquation(new PointF(I, J), new PointF(I + Constants.CellSize, J)));
                        wallsEquations.Add(new LineEquation(new PointF(I, J + Constants.CellSize), new PointF(I + Constants.CellSize, J + Constants.CellSize)));
                        wallsEquations.Add(new LineEquation(new PointF(I, J), new PointF(I, J + Constants.CellSize)));
                        wallsEquations.Add(new LineEquation(new PointF(I + Constants.CellSize, J), new PointF(I + Constants.CellSize, J + Constants.CellSize)));
                    }
                }
            }
        }

        public int Get(int x, int y)
        {
            return map[x, y];
        }

        public int GetWidth()
        {
            return map.GetLength(0);
        }

        public int GetHeight()
        {
            return map.GetLength(1);
        }

        public List<Line2D> Walls
        {
            get { return walls; }
        }

        public List<LineEquation> WallsEquations
        {
            get { return wallsEquations; }
        }
    }
}
