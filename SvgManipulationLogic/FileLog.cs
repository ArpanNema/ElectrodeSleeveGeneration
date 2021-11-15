using System;
using System.Collections.Generic;
using System.Text;

namespace SvgManipulationLogic
{
    public enum LogType { 
    DEBUG,
    ERROR,
    WARNING,
    INFO
    }
    public class FileLog
    {
        private static string logFolder = "./logs";
        private static string errorFileName=logFolder+"/error.log";
        private static string debugFileName = logFolder + "/debug.log";
        private static string warningFileName = logFolder + "/warning.log";
        private static string infoFileName = logFolder + "/info.log";
        public static void log(LogType type, string messge, string stackTrace="NA") {
            try
            {
                string msg = "\n==========================={0}================================\n\n";
                msg += "Message:\n\n{1}\n\n";
                msg += "Stack trace or Other Info:\n\n{2}\n\n";
                msg += "==============================End========================================\n";
                if (!System.IO.Directory.Exists(logFolder)) {
                    System.IO.Directory.CreateDirectory(logFolder);
                }
                if (type == LogType.DEBUG)
                {
                    msg=String.Format(msg, "Debug Log", messge, stackTrace);
                    System.IO.File.AppendAllText(debugFileName, msg);
                }
                else if (type == LogType.ERROR)
                {
                    msg = String.Format(msg, "Error Log", messge, stackTrace);
                    System.IO.File.AppendAllText(errorFileName, msg);
                }
                else if (type == LogType.WARNING)
                {
                    msg = String.Format(msg, "Warning Log", messge, stackTrace);
                    System.IO.File.AppendAllText(warningFileName, msg);
                }
                else
                {
                    msg = String.Format(msg, "Info Log", messge, stackTrace);
                    System.IO.File.AppendAllText(infoFileName, msg);
                }

            }
            catch (Exception e)
            {

            }
            
        }
    }
}
