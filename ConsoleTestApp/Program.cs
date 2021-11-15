using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();
            test2();
        }
        static void test1() {
            LeapDataManipulation.LeapControl lc = new LeapDataManipulation.LeapControl();

            Console.WriteLine("Please press enter to finish");
            Console.ReadLine();
            lc.stopLeapObject();
        }
        static void test2()
        {
            Leap2DataManipulation.LeapControl lc = new Leap2DataManipulation.LeapControl();
            lc.startLeap();
            Console.WriteLine("Please press enter to finish");
            Console.ReadLine();
            lc.stopLeap();
        }
    }
}
