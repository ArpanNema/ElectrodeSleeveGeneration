using System;
using System.Collections.Generic;
using System.Text;

namespace SvgManipulationLogic
{
    public class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool SingleValue { get; set; }
        public string GeneratePointText() {
            if (SingleValue) {
                return String.Format("{0}", X.ToString());
            }
            return String.Format("{0},{1}", X.ToString(), Y.ToString());
        }
        public static Point2D GeneratePointText(string data) {
            var dataArray=data.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (dataArray.Length == 2)
            {
                return new Point2D()
                {
                    X = double.Parse(dataArray[0]),
                    Y = double.Parse(dataArray[1]),
                    SingleValue = false
                };
            }
            else if (dataArray.Length == 1)
            {
                return new Point2D()
                {
                    X = double.Parse(dataArray[0]),
                    SingleValue = true
                };
            }
            else {
                return new Point2D() { X=0,Y=0,SingleValue=false};
            }
            
        }
    }
}
