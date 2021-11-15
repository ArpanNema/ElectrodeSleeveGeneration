using System;
using System.Collections.Generic;
using System.Text;


namespace SvgManipulationLogic
{
    public class CubicBezierCurvesCalculation
    {
        public static Nullable<double> slopAngleAtPoint(List<Rulyotano.Math.Geometry.Point> plst, int index) {
            if (index < 1 || index > plst.Count - 2) 
            {
                return null;
            }
            double d = 0;
            double c = 0;
            double b = 0;
            double a = 0;
            var roots=MathNet.Numerics.FindRoots.Cubic(d,c,b,a);
            
            var root1=roots.Item1;
            var cur = Rulyotano.Math.Interpolation.PointsToBezierCurves(new List<Rulyotano.Math.Geometry.Point>() {plst[index-1],plst[index],plst[index+1]},false,0.1);
            //B(t)=(1-t)^3*P0+3*(1-t)^2*t*P1+3*(1-t)*t^2*P2+t^3*P3
            //t3(p3−3p2+3p1−p0)+t23(p2−2p1+p0)+t3(p1−p0)+p0−p=0
            //B'(t)=3*Math.Pow(1-t,2)*(p1-p0)+6*(1-t)*t*(p2-p1)+3*Math.Pow(t,2)*(p3-p2)
            //B'(t)=3*(1-t)^2*(p1-p0)+6*(1-t)*t*(p2-p1)+3*t^2*(p3-p2)
            //b=3*(-p1+p0)*2+ 6*(p2-p1) (cofficient of t)
            //a=(p1-p0)*3-6*(p2-p1) (cofficient of t square)
            //c=(p1-p0)*3 (constant part)
            var p0 = cur[0].StartPoint;
            var p1 = new Rulyotano.Math.Geometry.Point((cur[0].FirstControlPoint.X+ cur[1].FirstControlPoint.X)/2, (cur[0].FirstControlPoint.Y + cur[1].FirstControlPoint.Y) / 2);
            var p2 = new Rulyotano.Math.Geometry.Point((cur[0].SecondControlPoint.X + cur[1].SecondControlPoint.X) / 2, (cur[0].SecondControlPoint.Y + cur[1].SecondControlPoint.Y) / 2);
            var p3 = cur[1].EndPoint;
            //using (-b+-Sqrt(b^2-4ac))/2a to find roots of quadratic equation.
            var ax = (p1.X - p0.X) * 3 - 6 * (p2.X - p1.X);
            var ay= (p1.Y - p0.Y) * 3 - 6 * (p2.Y - p1.Y);

            var bx = 3 * (p0.X - p1.X) * 2 + 6 * (p2.X - p1.X);
            var by = 3 * (p0.Y - p1.Y) * 2 + 6 * (p2.Y - p1.Y);

            var cx = (p1.X - p0.X) * 3;
            var cy = (p1.Y - p0.Y) * 3;

            if (Math.Pow(bx, 2) - 4 * ax * cx < 0) 
            {
                return null;
            }
            var rootXt1 = (-1 * bx + Math.Sqrt(Math.Pow(bx, 2) - 4 * ax * cx)) / (2 * ax);
            var rootXt2 = (-1 * bx - Math.Sqrt(Math.Pow(bx, 2) - 4 * ax * cx)) / (2 * ax);
            var rootYt1 = (-1 * by + Math.Sqrt(Math.Pow(by, 2) - 4 * ay * cy)) / (2 * ay);
            var rootYt2 = (-1 * by - Math.Sqrt(Math.Pow(by, 2) - 4 * ay * cy)) / (2 * ay);

            Func<double,double> funX = (double t) => { return Math.Pow(1 - t,3) * p0.X + 3 * Math.Pow(1 - t,2)* t * p1.X + 3 * (1 - t) * Math.Pow(t,2) * p2.X + Math.Pow(t,3) * p3.X; };
            Func<double, double> funY = (double t) => { return Math.Pow(1 - t, 3) * p0.Y + 3 * Math.Pow(1 - t, 2) * t * p1.Y + 3 * (1 - t) * Math.Pow(t, 2) * p2.Y + Math.Pow(t, 3) * p3.Y; };
            Func<double, double> funXDir = (double t) => { return 3 * Math.Pow(1 - t, 2) * (p1.X - p0.X) + 6 * (1 - t) * t * (p2.X - p1.X) + 3 * Math.Pow(t, 2) * (p3.X - p2.X); };
            Func<double, double> funYDir = (double t) => { return 3 * Math.Pow(1 - t, 2) * (p1.Y - p0.Y) + 6 * (1 - t) * t * (p2.Y - p1.Y) + 3 * Math.Pow(t, 2) * (p3.Y - p2.Y); };
            double xtVal = 0;
            double ytVal = 0;
            if (funX(rootXt1) == plst[index].X)
            {
                xtVal = rootXt1;
            }
            else if(funX(rootXt2) == plst[index].X) 
            {
                xtVal = rootXt2;
            }
            if (funY(rootYt1) == plst[index].Y)
            {
                ytVal = rootYt1;
            }
            else if (funY(rootYt2) == plst[index].Y)
            {
                ytVal = rootYt2;
            }
            //slop is tanTheta 
            var tanTheta = -1*funXDir(xtVal) / funYDir(ytVal);
            var theta=Math.Atan(tanTheta);
            tanTheta = (plst[index-1].Y - plst[index+1].Y) / (plst[index+1].X-plst[index-1].X );
            theta = Math.Atan(tanTheta);
            return theta;
        }
        public static double slopeLinear(Rulyotano.Math.Geometry.Point p0, Rulyotano.Math.Geometry.Point pn) 
        {
            var tanTheta = (pn.Y-p0.Y) / (pn.X - p0.X);
            var theta = Math.Atan(tanTheta);
            return theta;
        }
    }
}
