using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SvgManipulationLogic
{
    public class Path
    {
        private static string LineFormat = "<path " +
            "style=\"{0}\" d=\"{1}\" id=\"{2}\" "+
            "sodipodi:nodetypes=\"ccc\"></path>";
        public Style Style { get; set; }
        public Dimensions D { get; set; }
        public String ID { get; set; }
        public Path(string  id)
        {
            ID = id;
        }
        public string GenerateLineText() {
            string result = String.Empty;
            try
            {
                result=String.Format(Path.LineFormat, Style.GenerateStyleText(),D.GenerateDimensionText(),ID); 
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
                result = String.Empty;
            }
            return result;
        }
        public static Path GenerateLineObject(XElement lineData) {
            Path result = new Path(lineData.Attribute("id").Value);
            try
            {
                result.Style=Style.GenerateStyleObject(lineData.Attribute("style").Value);
                result.D = Dimensions.GenerateDimensionObject(lineData.Attribute("d").Value);
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
    }

}
