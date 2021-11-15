using System;
using System.Collections.Generic;
using System.Text;

namespace SvgManipulationLogic
{
    public class Region {
        public Rulyotano.Math.Geometry.Point Location { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int TemplateType { get; set; }
        public double ConductorScale { get; set; }
        public int ConductorShape { get; set; }
        public int WireArrangement { get; set; }
        public Region(Rulyotano.Math.Geometry.Point location,float width,float height,int templateType,double conductorScale,int wireArrangement)
        {
            Location = location;
            Width = width;
            Height = height;
            TemplateType = templateType;
            ConductorScale = conductorScale;
            WireArrangement = wireArrangement;

        }
    }
    public class TemplateRequest {
        public float MaxWidth { get; set; }
        public float WidthAtLow { get; set; }
        public float WidthAtHigh { get; set; }
        public float HandHeight { get; set; }
        public List<Region> CondutorRegions { get; set; }
    }
    public class Generator
    {
        string template = @"<SVG Template path>";
        string output = @"SVG output path";
        float xPadding = 10;
        float yPadding = 40;
        int conductorCount = 0;
        float wireGap = 5;
        float wireWidth = 5;
        public GeneratePathDValues PathGenerator { get; set; }
        public SvgManipulationLogic.SVGData SvgData { get; set; }
        public Conductors Conductor { get; set; }
        public Generator()
        {
            PathGenerator = new GeneratePathDValues();
            SvgData = new SVGData();
            Conductor = new Conductors();
        }
        public void GenerateTemplate1(TemplateRequest requestObj) 
        {
            var topValue = (requestObj.MaxWidth - requestObj.WidthAtHigh) / 2;
            var lowValue = (requestObj.MaxWidth - requestObj.WidthAtLow) / 2;
            List<Rulyotano.Math.Geometry.Point> points = new List<Rulyotano.Math.Geometry.Point>() {

                new Rulyotano.Math.Geometry.Point (xPadding+topValue,yPadding),
                 new Rulyotano.Math.Geometry.Point (xPadding+topValue+requestObj.WidthAtHigh,yPadding),
                new Rulyotano.Math.Geometry.Point (xPadding+requestObj.MaxWidth,yPadding+requestObj.HandHeight/3),
                 new Rulyotano.Math.Geometry.Point (xPadding+lowValue+requestObj.WidthAtLow,requestObj.HandHeight+yPadding),
                new Rulyotano.Math.Geometry.Point (xPadding+lowValue,requestObj.HandHeight+yPadding),
                new Rulyotano.Math.Geometry.Point (xPadding,yPadding+requestObj.HandHeight/3)
            };
            var c = PathGenerator.generateOuterBorderD1(points);
            SvgData.OutputPath = output;
            SvgData.generateSVGFile(template, "g1", "path1", c);
            //c = PathGenerator.generateGrid(210, 300, 10);
            //SvgData.generateSVGFile(output, "grid1", "refGrid", c);
            
            conductorCount = 0;
            foreach (var region in requestObj.CondutorRegions)
            {
                string d = Conductor.conductorAreas(region.Location, region.Width, region.Height);
                SvgData.generateSVGFileWithData(output, "g1", d);
                if (region.TemplateType == 0)
                {
                    generateConductorsForRegion(region, xPadding + topValue, requestObj.WidthAtHigh);
                }
                else if (region.TemplateType == 1) {
                    generateConductorsForRegion1(region, xPadding + topValue, requestObj.WidthAtHigh);
                }
                else
                {
                    generateConductorsForRegion2(region, xPadding + topValue, requestObj.WidthAtHigh);
                }

            }
            Console.WriteLine(c);
        }
        
        private void generateConductorsForRegion1(Region r, float wireStartingValue,float topLenth) {
            string result = string.Empty;
            double padding = r.ConductorScale * 5;
            double twicePadding = 2 * padding;
            var baseLoactionX = r.Location.X + padding;
            var baseLocationY = r.Location.Y + padding;
            var widthLoopCount = (int)(r.Width / twicePadding);
            var extraXPadding = (r.Width - twicePadding * widthLoopCount) / (widthLoopCount * 2);
            var heightLoopCount = (int)(r.Height / (twicePadding*3/4));
            var extraYPadding = (r.Height - twicePadding * heightLoopCount) / (heightLoopCount * 2);
            var conductorsPList = new List<Rulyotano.Math.Geometry.Point>();
            for (int j = heightLoopCount - 1; j >= 0; j--)
            {
                for (int i = widthLoopCount - 1; i >= 0; i--)
                {
                    if (j % 2 == 0) {
                        
                        var conductorPointX = baseLoactionX + i * twicePadding + (i * 2 + 1) * extraXPadding;
                        var conductorPointY = baseLocationY + j * twicePadding + (j * 2 + 1) * extraYPadding;
                        conductorsPList.Add(new Rulyotano.Math.Geometry.Point(conductorPointX, conductorPointY));
                        result = Conductor.generateElement(conductorsPList[conductorsPList.Count - 1], r.ConductorScale, r.ConductorShape);
                        SvgData.generateSVGFileWithData(output, "g1", result);
                    }
                    else {
                        if (i == 0)
                        {
                            break;
                        }
                        var conductorPointX = (baseLoactionX+i * twicePadding + (i * 2 + 1) * extraXPadding)-padding- extraXPadding;
                        var conductorPointY = baseLocationY + j * twicePadding + (j * 2 + 1) * extraYPadding;
                        conductorsPList.Add(new Rulyotano.Math.Geometry.Point(conductorPointX, conductorPointY));
                        result = Conductor.generateElement(conductorsPList[conductorsPList.Count - 1], r.ConductorScale, r.ConductorShape);
                        SvgData.generateSVGFileWithData(output, "g1", result);
                    }
                    
                }

            }
            if (r.WireArrangement == 1)
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 2)
            {
                generateWire1(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 3)
            {
                generateWire2(conductorsPList, wireStartingValue, topLenth);
            }
            else
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }

        }

        private void generateConductorsForRegion2(Region r, float wireStartingValue, float topLenth)
        {
            string result = string.Empty;
            double padding = r.ConductorScale * 5;
            double twicePadding = 2 * padding;

            var baseLoactionX = r.Location.X + padding;
            var baseLocationY = r.Location.Y + padding;
            var widthLoopCount = (int)(r.Width / twicePadding);
            double extraXPadding = (r.Width - twicePadding * widthLoopCount) / (widthLoopCount * 2);
            var heightLoopCount = (int)(r.Height / (twicePadding * 3 / 4));
            double extraYPadding = (r.Height - twicePadding * heightLoopCount) / (heightLoopCount * 2);
            var conductorsPList = new List<Rulyotano.Math.Geometry.Point>();
            double extraScale = (2 * extraYPadding * extraYPadding + twicePadding) / twicePadding;
            if (extraYPadding > -1.5)
            {
                extraScale = 1.4;
            }
            else if (extraYPadding > -2.5) {
                extraScale = 1.3;
            }
            else if (extraYPadding > -3)
            {
                extraScale = 1.2;
            }
            else
            {
                extraScale = 1.1;
            }

            for (int j = heightLoopCount - 1; j >= 0; j--)
            {
                for (int i = widthLoopCount - 1; i >= 0; i--)
                {
                    if (j % 2 == 0)
                    {

                        var conductorPointX = baseLoactionX + i * twicePadding + (i * 2 + 1) * extraXPadding;
                        var conductorPointY = baseLocationY + j * twicePadding + (j * 2 + 1) * extraYPadding;
                        conductorsPList.Add(new Rulyotano.Math.Geometry.Point(conductorPointX, conductorPointY));
                        result = Conductor.generateElement(conductorsPList[conductorsPList.Count - 1], r.ConductorScale, r.ConductorShape);
                        SvgData.generateSVGFileWithData(output, "g1", result);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            break;
                        }
                        var conductorPointX = (baseLoactionX + i * twicePadding + (i * 2 + 1) * extraXPadding) - padding - extraXPadding;
                        var conductorPointY = baseLocationY + j * twicePadding + (j * 2 + 1) * extraYPadding;
                        conductorsPList.Add(new Rulyotano.Math.Geometry.Point(conductorPointX, conductorPointY));
                        result = Conductor.generateElement(conductorsPList[conductorsPList.Count - 1], r.ConductorScale * extraScale, r.ConductorShape);
                        SvgData.generateSVGFileWithData(output, "g1", result);
                    }

                }

            }
            if (r.WireArrangement == 1)
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 2)
            {
                generateWire1(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 3)
            {
                generateWire2(conductorsPList, wireStartingValue, topLenth);
            }
            else
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }

        }

        private void generateConductorsForRegion(Region r, float wireStartingValue, float topLenth )
        {
            string result = string.Empty;
            double padding = r.ConductorScale * 5;
            double twicePadding = 2 * padding;
            var baseLoactionX = r.Location.X + padding;
            var baseLocationY = r.Location.Y + padding;
            var widthLoopCount = (int)(r.Width / twicePadding);
            var extraXPadding = (r.Width - twicePadding * widthLoopCount) / (widthLoopCount * 2);
            var heightLoopCount = (int)(r.Height / twicePadding);
            var extraYPadding = (r.Height - twicePadding * heightLoopCount) / (heightLoopCount * 2);
            var conductorsPList = new List<Rulyotano.Math.Geometry.Point>();
            for (int j = heightLoopCount - 1; j >= 0; j--)
            {
                for (int i = widthLoopCount - 1; i >= 0; i--)
                {
                    var conductorPointX = baseLoactionX + i * twicePadding + (i * 2 + 1) * extraXPadding;
                    var conductorPointY = baseLocationY + j * twicePadding + (j * 2 + 1) * extraYPadding;
                    conductorsPList.Add(new Rulyotano.Math.Geometry.Point(conductorPointX, conductorPointY));
                    result = Conductor.generateElement(conductorsPList[conductorsPList.Count - 1], r.ConductorScale, r.ConductorShape);
                    SvgData.generateSVGFileWithData(output, "g1", result);
                }

            }

            if (r.WireArrangement == 1)
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 2)
            {
                generateWire1(conductorsPList, wireStartingValue, topLenth);
            }
            else if (r.WireArrangement == 3)
            {
                generateWire2(conductorsPList, wireStartingValue, topLenth);
            }
            else
            {
                generateWire(conductorsPList, wireStartingValue, topLenth);
            }
            //generateWire(conductorsPList, wireStartingValue, topLenth);

        }
        private void generateWire1(List<Rulyotano.Math.Geometry.Point> conductorsPList, float wireStartingValue, float topLenth) {
            string result = string.Empty;
            Boolean toggle1 = false;
            double preY = conductorsPList[conductorsPList.Count - 1].Y;
            int count = 0;
            int firstRowCount = 0;
            int secondRowCount = 0;
            for (int i = conductorsPList.Count - 1; i >= 0; i--)
            {
                if (conductorsPList[i].Y != preY)
                {
                    if (toggle1)
                    {
                        secondRowCount = count;
                        break;
                    }
                    preY = conductorsPList[i].Y;
                    toggle1 = true;
                    firstRowCount = count;
                    count = 1;
                }
                else
                {
                    count++;
                }
            }
            int totalNumberOfRows = 0;
            int totalrowCount = 0;
            if (firstRowCount == secondRowCount)
            {
                totalNumberOfRows = conductorsPList.Count / firstRowCount;
                totalrowCount = firstRowCount;
            }
            else {
                totalNumberOfRows = conductorsPList.Count * 2 / (firstRowCount + secondRowCount);
            }
            for (int i = totalNumberOfRows; i > 0; i--)
            {
                if (firstRowCount != secondRowCount && i % 2 == 0)
                {
                    totalrowCount = firstRowCount;
                }
                else if(firstRowCount!=secondRowCount){
                    totalrowCount = secondRowCount;
                }
                for (int j = 1; j <= totalrowCount; j++) {
                    var x1 = wireStartingValue + wireGap* (2 * (j - 1) + 1) ; 
                    if (j > totalrowCount / 2)
                    {
                        x1 = wireStartingValue + topLenth - (2*(j-1)+1)*wireGap;
                    }
                    result = Conductor.designWire(new List<Rulyotano.Math.Geometry.Point>() {
                        new Rulyotano.Math.Geometry.Point(x1,yPadding-5),
                        new Rulyotano.Math.Geometry.Point(x1,yPadding+(conductorsPList[i*totalrowCount-j].Y-yPadding)/2),
                        conductorsPList[i*totalrowCount-j]
                        }, wireWidth);
                    SvgData.generateSVGFileWithData(output, "g1", result);

                }
                
            }
        }
        private void generateWire2(List<Rulyotano.Math.Geometry.Point> conductorsPList, float wireStartingValue, float topLenth) {
            string result = string.Empty;
            for (int i = conductorsPList.Count - 1; i >= 0; i--)
            {
                var x1 = conductorsPList[i].X;
                result = Conductor.designWire(new List<Rulyotano.Math.Geometry.Point>() {
                        new Rulyotano.Math.Geometry.Point(x1,yPadding-5),
                        new Rulyotano.Math.Geometry.Point(x1,yPadding+(conductorsPList[i].Y-yPadding)/2),
                        conductorsPList[i]
                    }, wireWidth);
                SvgData.generateSVGFileWithData(output, "g1", result);
            }
        }
        private void generateWire(List<Rulyotano.Math.Geometry.Point> conductorsPList, float wireStartingValue, float topLenth)
        {
            string result = string.Empty;
            for (int i = conductorsPList.Count - 1; i >= 0; i--)
            {
                var x1 = wireStartingValue + wireGap * (conductorCount * 2 + 1);
                conductorCount++;
                if (x1 >= wireStartingValue + topLenth)
                {
                    conductorCount = 0;
                    x1 = wireStartingValue + wireGap * (conductorCount * 2 + 1);
                    conductorCount++;
                }

                result = Conductor.designWire(new List<Rulyotano.Math.Geometry.Point>() {
                        new Rulyotano.Math.Geometry.Point(x1,yPadding-5),
                        new Rulyotano.Math.Geometry.Point(x1,yPadding+(conductorsPList[i].Y-yPadding)/2),
                        conductorsPList[i]
                    }, wireWidth);
                SvgData.generateSVGFileWithData(output, "g1", result);
            }
        }

        private void generateConductorsForRegion2(Region r, Conductors con, SVGData svgData, float wireStartingValue)
        {

        }

    }
}
