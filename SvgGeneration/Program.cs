using System;
using System.Collections.Generic;
using Rulyotano.Math;
using SvgManipulationLogic;

namespace SvgGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            var p0 = new point() { X = 60, Y = 10 };
            var p1 = new point() { X = 60, Y = 110 };
            var p2 = new point() { X = 60, Y = 110 };
            var p3 = new point() { X = 60, Y = 210 };
            var p4 = new point() { X = 80, Y = 110 };*/
            //test1();
            test2();
        }
        static void test2() {
            Generator g = new Generator();
            g.GenerateTemplate1(new TemplateRequest(){ MaxWidth=160, WidthAtLow= 120, WidthAtHigh= 140, HandHeight= 250,CondutorRegions= new List<Region>() {
                new Region(new Rulyotano.Math.Geometry.Point(40,60),60,90,0,3,1),
                new Region(new Rulyotano.Math.Geometry.Point(120,140),40,60,0,2,1)

            }});
        }
        static void test() {
            List<Rulyotano.Math.Geometry.Point> points = new List<Rulyotano.Math.Geometry.Point>() {
               
                new Rulyotano.Math.Geometry.Point (60,10),
                 new Rulyotano.Math.Geometry.Point (80,60),
                new Rulyotano.Math.Geometry.Point (60,210)
            };
            var x= Interpolation.PointsToBezierCurves(points, false);
            var p=x.BezierToPath();
            Console.WriteLine(p);
        }
        static void test1() {
            List<Rulyotano.Math.Geometry.Point> points = new List<Rulyotano.Math.Geometry.Point>() {

                new Rulyotano.Math.Geometry.Point (30,10),
                 new Rulyotano.Math.Geometry.Point (80,10),
                new Rulyotano.Math.Geometry.Point (100,60),
                 new Rulyotano.Math.Geometry.Point (80,210),
                new Rulyotano.Math.Geometry.Point (30,210),
                new Rulyotano.Math.Geometry.Point (10,60)
            };
            SvgManipulationLogic.GeneratePathDValues x = new GeneratePathDValues();
            var c=x.generateOuterBorderD1(points);
            SvgManipulationLogic.SVGData svgData = new SVGData();
            string template = @"C:\Users\arpan_svgrhg2\OneDrive\Desktop\Project\POC\CSharp\GenerateFiles\template.svg";
            string output = @"C:\Users\arpan_svgrhg2\OneDrive\Desktop\Project\POC\CSharp\GenerateFiles\output.svg";
            svgData.generateSVGFile(template,"g1", "path1", c);
            c = x.generateGrid(210,300,10);
            svgData.generateSVGFile(output,"grid1", "refGrid", c);
            c = x.genrateWire(new List<Rulyotano.Math.Geometry.Point>() { new Rulyotano.Math.Geometry.Point(35, 110),
                new Rulyotano.Math.Geometry.Point(35, 120),
                new Rulyotano.Math.Geometry.Point(40, 130),
                new Rulyotano.Math.Geometry.Point(45, 140),
                new Rulyotano.Math.Geometry.Point(70,150) }, 10);
            svgData.generateSVGFile(output, "g1", "path2", c);
            Conductors con = new Conductors();
            var d = con.generateElement(new Rulyotano.Math.Geometry.Point(65, 80), 2);
            svgData.generateSVGFileWithData(output, "g1", d);
            d = con.generateElement(new Rulyotano.Math.Geometry.Point(65, 120), 1.5);
            svgData.generateSVGFileWithData(output, "g1", d);
            d = con.generateElement(new Rulyotano.Math.Geometry.Point(65, 200), .5);
            svgData.generateSVGFileWithData(output, "g1", d);
            d = con.conductorAreas(new Rulyotano.Math.Geometry.Point(45, 75), 50,100);
            svgData.generateSVGFileWithData(output, "g1", d);
            Console.WriteLine(c);
        }
    }
    class point {
        public double X { get; set; }
        public double Y { get; set; }
        public List<point> calculateRoots(point p_0,point p_1, point p_2, point p_3) {
            var bx = -2 * p_0.X + 6 * (p_2.X - p_1.X);
            var ax = p_0.X - 6 * (p_2.X - p_1.X) + 3 * (p_3.X - p_2.X);
            var cx = p_0.X;

            var by = -2 * p_0.Y + 6 * (p_2.Y - p_1.Y);
            var ay = p_0.Y - 6 * (p_2.Y - p_1.Y) + 3 * (p_3.Y - p_2.Y);
            var cy = p_0.Y;

            if (bx * bx - 4 * ax * cx >= 0 && by * by - 4 * ay * cy >= 0) 
            {
                double root1X = (-1 * (bx) + Math.Sqrt(bx * bx - 4 * ax * cx)) / (2 * ax);
                double root2X = (-1 * (bx) - Math.Sqrt(bx * bx - 4 * ax * cx)) / (2 * ax);
                
                double root1Y = (-1 * (by) + Math.Sqrt(by * by - 4 * ay * cy)) / (2 * ay);
                double root2Y = (-1 * (by) - Math.Sqrt(by * by - 4 * ay * cy)) / (2 * ay);
                return new List<point>() { new point() { X = root1X, Y = root1Y }, new point() { X = root2X, Y = root2Y } };
            }
            return null;
        }
        public void calculatePoint(point p_4) { 

        }
    }
}
