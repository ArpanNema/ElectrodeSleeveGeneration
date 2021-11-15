using System;
using System.Collections.Generic;
using System.Text;

namespace SvgManipulationLogic
{
    public class Dimensions
    {
        private List<char> PossibleParameters =new List<char>
        { 'm', 'M', 
            'L', 'l', 'H', 'h', 'V', 'v', 
            'C', 'c', 'S', 's', 
            'Q', 'q', 'T', 't', 
            'A', 'a', 
            'Z', 'z' };
        public Dictionary<char,List<Point2D>> AllComponents { get; set; }
        public Dimensions()
        {
            AllComponents = new Dictionary<char, List<Point2D>>();
        }
        public static Dimensions GenerateDimensionObject(string data) {
            Dimensions result = new Dimensions();
            try
            {
                var components=data.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                char currentKey = ' ';
                foreach (var i in components)
                {
                    if (result.PossibleParameters.Contains(i[0]))
                    {
                        result.AllComponents.Add(i[0], new List<Point2D>());
                        currentKey = i[0];
                    }
                    else {
                        result.AllComponents[currentKey].Add(Point2D.GeneratePointText(i));
                    }
                }
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
        public string GenerateDimensionText() {
            String result = String.Empty;
            try
            {
                foreach (var i in AllComponents)
                {
                    result += i.Key + " ";
                    foreach (var p in i.Value) {
                        result += p.GeneratePointText()+" ";
                    }
                }
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result.Trim();
        }
    }
}
