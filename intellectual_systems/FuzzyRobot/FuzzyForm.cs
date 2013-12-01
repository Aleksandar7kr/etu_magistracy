using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FuzzyRobot;


namespace FuzzyRobot
{
    public partial class FuzzyForm : Form
    {
        private MapModel mapModel;
        Tracks T;
        private int[,] map;
        private DistancesSensor sensor;
        private FuzzyLogic L;
        double t = 4.0;

        public FuzzyForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (T == null)
            {

                float lx = (float)(-1.75 * Constants.CellSize),
                    ly = (float)2 * (float)Constants.CellSize,

                    rx = (float)(-1.25 * Constants.CellSize),
                    ry = (float)1.9 * (float)Constants.CellSize;
                T = new Tracks(lx, ly, rx, ry);
            }

            if (L == null)
            {
                double ll = Constants.CellSize;
                double m1 = 0.8*Constants.CellSize;
                double m2 = 2.1*Constants.CellSize;
                double h = 1.8*Constants.CellSize;
                L = new FuzzyLogic(ll, m1, m2, h);
            }

            ReadMap();
            DrawMap();
            DrawRobot();
        }

        private void ReadMap()
        {
            string[] lines = mapTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            map = new int[lines[0].Length, lines.Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = int.Parse(lines[j][i].ToString());
                }
            }

            mapModel = new MapModel(map);
        }

        private void DrawMap()
        {
            mapPictureBox.Image = new Bitmap(mapModel.GetWidth() * Constants.CellSize, mapModel.GetHeight() * Constants.CellSize);
            mapPictureBox.Size = mapPictureBox.Image.Size;

            for (int i = 0; i < mapModel.GetWidth(); i++)
            {
                for (int j = 0; j < mapModel.GetHeight(); j++)
                {
                    using (var graphics = Graphics.FromImage(mapPictureBox.Image))
                    {
                        if (map[i, j] == 1)
                        {
                            graphics.FillRectangle(Brushes.Brown, i * Constants.CellSize, j * Constants.CellSize, Constants.CellSize, Constants.CellSize);
                            graphics.DrawRectangle(new Pen(Brushes.WhiteSmoke), i * Constants.CellSize, j * Constants.CellSize, Constants.CellSize, Constants.CellSize);
                        }
                    }
                }
            }
        }

        private void DrawWalls()
        {
            foreach (var line in mapModel.Walls)
            {
                using (var graphics = Graphics.FromImage(mapPictureBox.Image))
                {
                    graphics.DrawLine(new Pen(Brushes.Chartreuse), line.A.X, line.A.Y, line.B.X, line.B.Y);
                }
            }
        }

        private void DrawRobot()
        {
            using (var graphics = Graphics.FromImage(mapPictureBox.Image))
            {
                graphics.DrawLine(new Pen(Brushes.Yellow), (float) (-T.lx), (float) T.ly, (float) (-T.rx), (float) T.ry);
            }
        }

        private void DrawRays()
        {
            using (var graphics = Graphics.FromImage(mapPictureBox.Image))
            {
                graphics.DrawLine(new Pen(Brushes.Crimson), (float)-T.lx, (float)T.ly, sensor.LeftPoint.X, sensor.LeftPoint.Y);
                graphics.DrawLine(new Pen(Brushes.Crimson), (float)-T.rx, (float)T.ry, sensor.RightPoint.X, sensor.RightPoint.Y);
            }
        }


        private void stepButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                DrawMap();
                DrawWalls();
                DrawRobot();
                Step();
                DrawRays();
                this.Refresh();
            }
        }
        
        public void Step()
        {
            float l = (float)(Math.Sqrt(Math.Pow((T.lx - T.rx), 2) + Math.Pow((T.ly - T.ry), 2)) / 2);

            bool rayDirection;

            if ((Math.Abs((float)T.lx - T.rx) < Constants.EPS) && (T.ly < T.ry))
            {
                rayDirection = true;
            }
            else
            {
                rayDirection = false;
            }

            if (T.lx > T.rx)
            {
                rayDirection = true;
            }
            else
            {
                rayDirection = false;
            }


            float angleA;

            if (Math.Abs(T.lx - T.rx) < 0.00001)
            {
                angleA = (float)(Math.PI / 2.0);
            }
            else
            {
                angleA = (float)Math.Atan((T.ly - T.ry) / (T.lx - T.rx));
                if (angleA <= 0.0) angleA = (float)(Math.PI + angleA);

            }

            double c = 0.0; // new angle

            if (rayDirection) l = -l;

            if (angleA >= (Math.PI / 2.0))
            {
                c = angleA - Math.PI / 2.0;
            }
            else c = Math.PI / 2.0 + angleA;

            float lxNew = (float)(T.lx + l * Math.Cos(c));
            float lyNew = (float)(T.ly + l * Math.Sin(c));
            float rxNew = (float)(T.rx + l * Math.Cos(c));
            float ryNew = (float)(T.ry + l * Math.Sin(c));

            sensor = new DistancesSensor();
            sensor.LeftEquation = new LineEquation(new PointF((float)(-T.lx), (float)T.ly), new PointF(-lxNew, lyNew));
            sensor.RightEquation = new LineEquation(new PointF((float)(-T.rx), (float)T.ry), new PointF(-rxNew, ryNew));
            sensor.PlatrormLine = new Line2D(new PointF((float)-T.lx, (float)T.ly), new PointF((float)-T.rx, (float)T.ry));
            sensor.Scan(mapModel.WallsEquations, mapModel.Walls);
            PointF rD = sensor.RightPoint;
            PointF lD = sensor.LeftPoint;

            T = L.Movement(T, L.LogicMethod(sensor.LeftDistance, sensor.RightDistance), t);
        }
    }
}
