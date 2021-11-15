
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVGGenerateWindowsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewHand());
        }
        /*public static void Main(string[] args)
        {
            Label l = new Label();
            LeapControl lc = new LeapControl(l);
            Console.WriteLine("test started");
            lc.startLeapObjects();
            lc.stopLeapObject();
        }*/
    }
}
