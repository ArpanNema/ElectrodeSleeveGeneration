using System;
using System.Collections.Generic;
using System.Text;

namespace SvgManipulationLogic
{
    public class Style
    {
        public Dictionary<string,string> StyleComponents { get; set; }
        public Style()
        {
            StyleComponents = new Dictionary<string, string>();
        }
        public string GenerateStyleText() {
            string result = String.Empty;
            try
            {
                foreach (var i in StyleComponents)
                {
                    result+= string.Format("{0}:{1};", i.Key,i.Value);
                }
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
                result = String.Empty;
            }
            return result;
        }
        public static Style GenerateStyleObject(string styledata) {
            Style result = new Style();
            try
            {
                var allComponents=styledata.Split(';');
                foreach (var s in allComponents)
                {
                    var dataValue=s.Split(':');
                    if (result.StyleComponents.ContainsKey(dataValue[0]))
                    {
                        result.StyleComponents[dataValue[0]] = dataValue[1];
                    }
                    else {
                        result.StyleComponents.Add(dataValue[0], dataValue[1]);
                    }
                }
            }
            catch (Exception e)
            {
                FileLog.log(LogType.ERROR, e.Message, e.StackTrace);
            }
            return result;
        }
    }
}
