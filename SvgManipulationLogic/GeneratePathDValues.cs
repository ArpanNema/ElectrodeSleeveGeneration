using System;
using System.Collections.Generic;
using System.Text;
using Rulyotano.Math;

namespace SvgManipulationLogic
{
    public class GeneratePathDValues
    {
        private string addSpaces(string d) 
        {
            string result = String.Empty;
            try
            {
                foreach (char i in d) 
                {
                    if (char.IsLetter(i))
                    {
                        result += i + " ";
                    }
                    else {
                        result += i;
                    }
                }
                result=result.Trim();
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateCurve(Rulyotano.Math.Geometry.Point p1, Rulyotano.Math.Geometry.Point p2, Rulyotano.Math.Geometry.Point p3) 
        {
            string result = String.Empty;
            try
            {
                
                List<Rulyotano.Math.Geometry.Point> points = new List<Rulyotano.Math.Geometry.Point>() {

                new Rulyotano.Math.Geometry.Point (p1.X,p1.Y),
                 new Rulyotano.Math.Geometry.Point (p2.X,p2.Y),
                new Rulyotano.Math.Geometry.Point (p3.X,p3.Y)
            };
                var x = Interpolation.PointsToBezierCurves(points, false);
                result = x.BezierToPath();
                result = addSpaces(result);
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateStraightLine(Rulyotano.Math.Geometry.Point p1, Rulyotano.Math.Geometry.Point p2) {
            string result = String.Empty;
            try
            {
                result = String.Format("M {0},{1} {2},{3}",p1.X,p1.Y,p2.X,p2.Y);
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateOuterBorderD1(List<Rulyotano.Math.Geometry.Point> pList) 
        {
            string result = String.Empty;
            try
            {
                result += generateStraightLine(pList[0], pList[1]);
                result += " " + removeFirstMove(generateCurve(pList[1], pList[2], pList[3]));
                result += " " + generateStraightLine(pList[3], pList[4]);
                result += " " + removeFirstMove(generateCurve(pList[4], pList[5], pList[0]));
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateHexagon(double scale) 
        {
            string result = "M ";
            try
            {
                List<Rulyotano.Math.Geometry.Point> defaultPoints = new List<Rulyotano.Math.Geometry.Point>() {
                new Rulyotano.Math.Geometry.Point(0,-4.8),
                new Rulyotano.Math.Geometry.Point(4.14,-2.3),
                new Rulyotano.Math.Geometry.Point(4.14,2.3),
                new Rulyotano.Math.Geometry.Point(0,4.8),
                new Rulyotano.Math.Geometry.Point(-4.14,2.3),
                new Rulyotano.Math.Geometry.Point(-4.14,-2.3),
                new Rulyotano.Math.Geometry.Point(0,-4.8)
                };
                foreach (var p in defaultPoints)
                {
                    result += String.Format("{0},{1} ", p.X*scale, p.Y*scale);
                }
                result += " Z";
                result = result.Trim();
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
            
        }

        public string generateTriangle(double scale)
        {
            string result = "M ";
            try
            {
                //0,0 -5,8.66 5,8.66 0,0
                List<Rulyotano.Math.Geometry.Point> defaultPoints = new List<Rulyotano.Math.Geometry.Point>() {
                new Rulyotano.Math.Geometry.Point(-4.8,2.6),
                new Rulyotano.Math.Geometry.Point(4.8,2.6),
                new Rulyotano.Math.Geometry.Point(0,-5.5)
                };
                foreach (var p in defaultPoints)
                {
                    result += String.Format("{0},{1} ", p.X * scale, p.Y * scale);
                }
                result += " Z";
                result = result.Trim();
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;

        }
        public string generateCircle(double scale) {
            string result = "";
            try
            {
                result = String.Format("M 0,0 m -{0},0 a {0},{0} 0 1,0 {1},0 a {0},{0} 0 1,0 -{1},0", (4.8 * scale), (9.6*scale)); ;
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateSquare(double scale)
        {
            string result = "M ";
            try
            {
                List<Rulyotano.Math.Geometry.Point> defaultPoints = new List<Rulyotano.Math.Geometry.Point>() {
                new Rulyotano.Math.Geometry.Point(-4.8,-4.8),
                new Rulyotano.Math.Geometry.Point(4.8,-4.8),
                new Rulyotano.Math.Geometry.Point(4.8,4.8),
                new Rulyotano.Math.Geometry.Point(-4.8,4.8)
                };
                foreach (var p in defaultPoints)
                {
                    result += String.Format("{0},{1} ", p.X * scale, p.Y * scale);
                }
                result += " Z";
                result = result.Trim();
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;

        }


        public string generateConductorRegion(float width, float height) {
            string result = string.Empty;
            try
            {
                return String.Format("M 0,0 {0},0 {0},{1} 0,{1} Z",width.ToString(),height.ToString());
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string genrateWire(List<Rulyotano.Math.Geometry.Point> pList,double width) {
            string result = String.Empty;
            try
            {
                List<Rulyotano.Math.Geometry.Point> tempList = new List<Rulyotano.Math.Geometry.Point>();
                for (int i=pList.Count-1;i>=0;i--)
                {
                    if (i != 0)
                    {
                        tempList.Add(new Rulyotano.Math.Geometry.Point(pList[i].X + width / 2, pList[i].Y - width / 2));
                    }
                    else 
                    {
                        tempList.Add(new Rulyotano.Math.Geometry.Point(pList[i].X + width/2, pList[i].Y));
                    }
                    
                    /* if (i == 0 || i == pList.Count - 1)
                     {
                         tempList.Add(new Rulyotano.Math.Geometry.Point(pList[i].X + width, pList[i].Y));
                     }
                     else 
                     {
                         var theta = CubicBezierCurvesCalculation.slopAngleAtPoint(pList, i);
                         //var theta = CubicBezierCurvesCalculation.slopeLinear(pList[i + 1], pList[i - 1]);
                         if (theta != null) 
                         {
                             double xIncrement = (Math.Sin((double)theta) > 0 ? Math.Sin((double)theta) : -1 * Math.Sin((double)theta))*width;
                             double yIncrement = (Math.Cos((double)theta) > 0 ? Math.Cos((double)theta) : -1 * Math.Cos((double)theta)) * width;


                             tempList.Add(new Rulyotano.Math.Geometry.Point(pList[i].X + xIncrement, pList[i].Y+yIncrement));
                         }
                     }*/

                }
                //pList.AddRange(tempList);
                var x = Interpolation.PointsToBezierCurves(pList, false,0.2);
                result = x.BezierToPath();
                result = addSpaces(result);
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string generateGrid(double width, double height, double separation) 
        {
            string result = String.Empty;
            try
            {
                int count = (int)(width / separation);
                for (int i = 0; i < count; i++)
                {
                    result+=" "+generateStraightLine(new Rulyotano.Math.Geometry.Point(i*separation,0), new Rulyotano.Math.Geometry.Point(i*separation,height));
                }
                count = (int)(height / separation);
                for (int i = 0; i < count; i++)
                {
                    result += " " + generateStraightLine(new Rulyotano.Math.Geometry.Point(0, i * separation), new Rulyotano.Math.Geometry.Point(width, i * separation));
                }
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        private string removeFirstMove(string d) 
        {
            string result = String.Empty;
            try
            {
                bool mStatus = false;
                bool addStatus = false;
                var dArray=d.Split(' ');
                foreach (var i in dArray) 
                {
                    if (i == "M" || i == "m") 
                    {
                        mStatus = true;
                        continue;
                    }
                    if (mStatus) 
                    {
                        if (char.IsLetter(i[0]))
                        {
                            addStatus = true;
                        }
                        
                    }
                    if (addStatus) 
                    {
                        result += " " + i;
                    }
                }
                result = result.Trim();
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
    }
}
