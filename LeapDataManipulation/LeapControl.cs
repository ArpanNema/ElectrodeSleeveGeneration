using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;


namespace LeapDataManipulation
{
    public class LeapControl
    {

        public Listener Listener { get; set; }
        public Controller Control { get; set; }
        public LeapControl()
        {
            
            Listener = new CustomListener();
            Control = new Leap.Controller(Listener);
            
        }
        public void stopLeapObject()
        {
            Control.RemoveListener(Listener);
            Control.Dispose();
        }

    }
    public class CustomListener : Listener
    {
        private Object thisLock = new Object();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
            //Console.WriteLine(line);
        }
        public override void OnConnect(Controller arg0)
        {
            
            SafeWriteLine("Connection established");
            base.OnConnect(arg0);
        }
        public override void OnFrame(Controller arg0)
        {
            
            if (arg0.IsConnected)
            {
                // Get the most recent frame and report some basic information
                Frame frame = arg0.Frame();
                SafeWriteLine(string.Format("Frame id: {0}, timestamp: {1}, hands: {2}",
                  frame.Id, frame.Timestamp, frame.Hands.Count));
                /*Console.WriteLine(
                  "Frame id: {0}, timestamp: {1}, hands: {2}",
                  frame.Id, frame.Timestamp, frame.Hands.Count
                );*/
                foreach (Hand hand in frame.Hands)
                {
                    
                    
                    SafeWriteLine(string.Format("  Hand id: {0}, palm position: {1}, fingers: {2}",
                      hand.Id, hand.PalmPosition, hand.Fingers.Count));
                    /*Console.WriteLine("  Hand id: {0}, palm position: {1}, fingers: {2}",
                      hand.Id, hand.PalmPosition, hand.Fingers.Count);*/
                    // Get the hand's normal vector and direction
                    
                    Vector normal = hand.PalmNormal;
                    Vector direction = hand.Direction;
                    // Calculate the hand's pitch, roll, and yaw angles
                    SafeWriteLine(string.Format("  Hand pitch: {0} degrees, roll: {1} degrees, yaw: {2} degrees",
                      direction.Pitch * 180.0f / (float)Math.PI,
                      normal.Roll * 180.0f / (float)Math.PI,
                      direction.Yaw * 180.0f / (float)Math.PI));
                    /*Console.WriteLine(
                      "  Hand pitch: {0} degrees, roll: {1} degrees, yaw: {2} degrees",
                      direction.Pitch * 180.0f / (float)Math.PI,
                      normal.Roll * 180.0f / (float)Math.PI,
                      direction.Yaw * 180.0f / (float)Math.PI
                    );*/
                }
            }
            base.OnFrame(arg0);

        }
    }
}
