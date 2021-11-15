using System;
using System.Collections.Generic;
using System.Text;
using Rulyotano.Math.Geometry;

namespace SvgManipulationLogic
{
    public class Conductors
    {
        static int count = 0;
        public static int wireCount = 0;
        string defaultDesign = "<g xmlns=\"http://www.w3.org/2000/svg\" id=\"{0}\" transform=\"translate({1},{2})\">" +
            "<path d = \"{3}\" " +
            " style=\"fill:lightblue;fill-opacity:1;fill-rule:nonzero;stroke:none\" id=\"path{5}\" />" +
            "<circle cx = \"0\" cy=\"0\" r=\"{4}\"" +
            " style=\"fill:none;stroke:#2e3092;stroke-width:.25;stroke-linecap:butt;stroke-linejoin:miter;stroke-miterlimit:10;stroke-dasharray:none;stroke-opacity:1\"/></g>";
        string conductorRegion = "<g xmlns=\"http://www.w3.org/2000/svg\" id=\"{0}\" transform=\"translate({1},{2})\">" +
            "<path d = \"{3}\" " +
            " style=\"fill:none;stroke:purple;stroke-width:0.5;stroke-linecap:butt;stroke-linejoin:miter;stroke-miterlimit:10;stroke-dasharray:none;stroke-opacity:1\" id=\"path{4}\" />" +
            "</g>";
        string wireDefault = "<g xmlns=\"http://www.w3.org/2000/svg\" id=\"{0}\" transform=\"translate({1},{2})\">" +
            "<path d = \"{3}\" " +
            " style=\"fill:none;stroke:#2e3092;stroke-width:5;stroke-linecap:butt;stroke-linejoin:miter;stroke-miterlimit:10;stroke-dasharray:none;stroke-opacity:0.8\" id=\"path{4}\" />" +
            "</g>";
        public GeneratePathDValues PathGenerator { get; set; }
        public Conductors()
        {
            PathGenerator = new GeneratePathDValues();
        }
        public string generateElement(Point p, double scale,int shape=2) {

            String d = PathGenerator.generateHexagon(scale);
            if (shape == 2){
                d = PathGenerator.generateTriangle(scale);
            }
            else if (shape == 3) {
                d = PathGenerator.generateSquare(scale);
            }
            else if (shape == 4)
            {
                d = PathGenerator.generateCircle(scale);
            }
            Conductors.count++;
            if (scale * 5 - 1.5 > 5)  
            {
                return string.Format(defaultDesign, "cond" + count, p.X, p.Y, d, 5, "cond" + count);
            }
            return string.Format(defaultDesign, "cond" + count, p.X, p.Y, d, scale * 5-1.5, "cond" + count);

        }
        public string conductorAreas(Point p, float width, float height) 
        {
            var d = PathGenerator.generateConductorRegion(width, height);
            Conductors.count++;
            return string.Format(conductorRegion, "condRegion" + count, p.X, p.Y, d, "condRegion" + count);
        }
        public string designWire(List<Point> pList, float width)
        {
            var d = PathGenerator.genrateWire(pList, width);
            Conductors.wireCount++;
            return string.Format(wireDefault, "Wire" + wireCount, 0, 0, d, "Wire" + wireCount);
        }
    }
}
