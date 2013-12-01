using System;
using System.Collections.Generic;

namespace FuzzyRobot
{
    class Voltages // (Velocities)
    {
        public double lV;
        public double rV;

        public Voltages(double lV, double rV)
        {
            this.lV = lV;
            this.rV = rV;
        }
    }

    class Tracks
    { // coordinates of the robot tracks
        public double lx;
        public double ly;
        public double rx;
        public double ry;

        public Tracks(double lx, double ly, double rx, double ry)
        {
            this.lx = lx;
            this.ly = ly;
            this.rx = rx;
            this.ry = ry;
        }
    }

    class FuzzyLogic
    {

        // Fuzzy logic distance options
        public double l; // low limit
        public double m1; // medium limits
        public double m2;
        public double h; // high limit

        // Fuzzy logic robot options
        double Umax; // Voltage (Velocity) limit
        double k; // coefficient 0 < k < 1

        public FuzzyLogic(double l, double m1, double m2, double h, double Umax, double k)
        {
            this.l = l;
            this.m1 = m1;
            this.m2 = m2;
            this.h = h;
            this.Umax = Umax;
            if ((k > 0) && (k < 1)) this.k = k;
            else this.k = 1.0 / 3.0;
            Console.WriteLine(this.k + "\n");
        }

        public FuzzyLogic(double l, double m1, double m2, double h)
        {
            this.l = l;
            this.m1 = m1;
            this.m2 = m2;
            this.h = h;
            this.Umax = 1;
            this.k = 1.0 / 3.0;
        }

        // Characteristic functions
        private double low(double d)
        {
            if (d <= this.m1) return 1.0;
            else
                if (d >= this.l) return 0.0;
                else
                    return d / (this.m1 - this.l) + this.l / (this.l - this.m1);
        }

        private double medium(double d)
        {
            if ((d <= this.m1) || (d >= this.m2)) return 0.0;
            else
                if ((d >= this.l) && (d <= this.h)) return 1.0;
                else
                    if ((d > this.m1) && (d < this.l)) return d / (this.l - this.m1) + this.m1 / (this.m1 - this.l);
                    else
                        return d / (this.h - this.m2) + this.m2 / (this.m2 - this.h);
        }

        private double high(double d)
        {
            if (d <= this.h) return 0.0;
            else
                if (d >= this.m2) return 1.0;
                else
                    return d / (this.m2 - this.h) + this.h / (this.h - this.m2);
        }

        // Sugeno-Type Fuzzy Inference
        public Voltages LogicMethod(double ld, double rd)
        { // ld, rd - distances from sensors

            //Fuzzification
            List<Voltages> V = new List<Voltages>();
            List<double> m = new List<double>();

            // Rules
            if ((high(ld) > 0) && (high(rd) > 0))
            {
                Voltages U1 = new Voltages(Umax, Umax);
                V.Add(U1);

                if (high(ld) < high(rd)) m.Add(high(ld));
                else m.Add(high(rd));
            }

            if ((medium(ld) > 0) && (high(rd) > 0))
            {
                Voltages U2 = new Voltages(Umax, k * Umax);
                V.Add(U2);

                if (medium(ld) < high(rd)) m.Add(medium(ld));
                else m.Add(high(rd));
            }

            if ((high(ld) > 0) && (medium(rd) > 0))
            {
                Voltages U3 = new Voltages(k * Umax, Umax);
                V.Add(U3);

                if (high(ld) < medium(rd)) m.Add(high(ld));
                else m.Add(medium(rd));
            }

            if ((medium(ld) > 0) && (medium(rd) > 0))
            {
                Voltages U4;
                if (ld <= rd)
                    U4 = new Voltages(Umax, 0);
                else
                    U4 = new Voltages(0, Umax);
                V.Add(U4);

                if (medium(ld) < medium(rd)) m.Add(medium(ld));
                else m.Add(medium(rd));
            }

            if ((low(ld) > 0) && (high(rd) > 0))
            {
                Voltages U5 = new Voltages(Umax, 0 - k * Umax);
                V.Add(U5);

                if (low(ld) < high(rd)) m.Add(low(ld));
                else m.Add(high(rd));
            }

            if ((high(ld) > 0) && (low(rd) > 0))
            {
                Voltages U6 = new Voltages(0 - k * Umax, Umax);
                V.Add(U6);

                if (high(ld) < low(rd)) m.Add(high(ld));
                else m.Add(low(rd));
            }

            if ((low(ld) > 0) && (medium(rd) > 0))
            {
                Voltages U7 = new Voltages(Umax, 0 - Umax);
                V.Add(U7);

                if (low(ld) < medium(rd)) m.Add(low(ld));
                else m.Add(medium(rd));
            }

            if ((medium(ld) > 0) && (low(rd) > 0))
            {
                Voltages U8 = new Voltages(0 - k*Umax, Umax);
                V.Add(U8);

                if (medium(ld) < low(rd)) m.Add(medium(ld));
                else m.Add(low(rd));
            }

            if ((low(ld) > 0) && (low(rd) > 0))
            {
                Voltages U9;
                //if (ld <= rd)
                    U9 = new Voltages(Umax, 0 - Umax);
                //else
                    //U9 = new Voltages(0 - Umax, Umax);
                V.Add(U9);

                if (low(ld) < low(rd)) m.Add(low(ld));
                else m.Add(low(rd));
            }

            // Defuzzification
            double lV = 0.0;
            double rV = 0.0;
            double M = 0.0;

            for (int i = 0; i < V.Count; i++)
            {
                lV = lV + m[i] * V[i].lV;
                rV = rV + m[i] * V[i].rV;
                M = M + m[i];
            }
            lV = lV / M;
            rV = rV / M;

            return new Voltages(lV, rV);
        }

        public double GetPolarR(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        public double GetPolarF(double x, double y)
        {
            if ((x > 0) && (y >= 0)) return Math.Atan(y / x);
            if ((x > 0) && (y < 0)) return Math.Atan(y / x) + 2.0 * Math.PI;
            if (x < 0) return Math.Atan(y / x) + Math.PI;
            if ((x == 0) && (y > 0)) return Math.PI / 2.0;
            if ((x == 0) && (y < 0)) return 3.0 * Math.PI / 2.0;
            else return 0.0;
        }

        public double GetX(double r, double f)
        {
            return r * Math.Cos(f);
        }

        public double GetY(double r, double f)
        {
            return r * Math.Sin(f);
        }

        public Tracks Movement(Tracks ip, Voltages V, double t)
        { // ip - initial position of tracks, V - Velocities, t - time
            double k = 0.001;
            double a = 0.0;
            double b = 0.0;
            double l = 0.0;
            double lx = 0.0;
            double ly = 0.0;
            double rx = 0.0;
            double ry = 0.0;

            if (Math.Abs(ip.lx - ip.rx) < k)
            {
                a = Math.PI / 2.0;
                b = Math.PI - a;
                l = Math.Abs((ip.ly - ip.ry) / 2.0);
            }
            else
            {
                a = Math.Atan((ip.ly - ip.ry) / (ip.lx - ip.rx));
                if (a <= 0.0) a = Math.PI + a;
                b = Math.PI - a;
                l = Math.Abs((ip.lx - ip.rx) / (2.0 * Math.Cos(b)));
            }

            if (Math.Abs(V.lV - V.rV) < k)
            {
                double S = 0.0;
                double c = 0.0;
                S = ((V.lV + V.rV) / 2.0) * t;

                if (ip.lx > ip.rx) S = -S;
                else if ((Math.Abs(ip.lx - ip.rx) < k) && (ip.ly < ip.ry)) S = -S;
                if (a >= (Math.PI / 2.0)) c = a - Math.PI / 2.0;
                else c = Math.PI / 2.0 + a;

                lx = ip.lx + S * Math.Cos(c);
                ly = ip.ly + S * Math.Sin(c);
                rx = ip.rx + S * Math.Cos(c);
                ry = ip.ry + S * Math.Sin(c);
            }
            else
            {
                double R = 0.0;
                double c = 0.0;
                double Rx = 0.0;
                double Ry = 0.0;
                double lr = 0.0;
                double rr = 0.0;
                double ld = 0.0;
                double rd = 0.0;

                R = ((V.lV + V.rV) / (V.lV - V.rV)) * l;
                if (Math.Abs(R - l) < k) c = (V.lV / (R + l)) * t;
                else c = (V.rV / (R - l)) * t;

                rx = ip.rx - ip.lx;
                ry = ip.ry - ip.ly;

                Rx = GetX(GetPolarR(rx, ry) + (R - l), GetPolarF(rx, ry)) + ip.lx;
                Ry = GetY(GetPolarR(rx, ry) + (R - l), GetPolarF(rx, ry)) + ip.ly;

                lx = ip.lx - Rx;
                ly = ip.ly - Ry;
                rx = ip.rx - Rx;
                ry = ip.ry - Ry;

                lr = GetPolarR(lx, ly);
                rr = GetPolarR(rx, ry);

                ld = GetPolarF(lx, ly) - c;
                while (ld < 0) ld = 2.0 * Math.PI + ld;
                while (ld > 2.0 * Math.PI) ld = ld - 2.0 * Math.PI;

                rd = GetPolarF(rx, ry) - c;
                while (rd < 0) rd = 2.0 * Math.PI + rd;
                while (rd > 2.0 * Math.PI) rd = rd - 2.0 * Math.PI;

                lx = GetX(lr, ld) + Rx;
                ly = GetY(lr, ld) + Ry;
                rx = GetX(rr, rd) + Rx;
                ry = GetY(rr, rd) + Ry;
            }
            return new Tracks(lx, ly, rx, ry);
        }
    }
}
