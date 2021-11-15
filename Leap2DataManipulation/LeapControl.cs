using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace Leap2DataManipulation
{
    public class LeapControl:IDisposable
    {

        public CustomListener Listener { get; set; }
        public Controller Control { get; set; }
        private bool Status { get; set; }
        public LeapControl()
        {
            
            Control = new Leap.Controller();
            Control.SetPolicy(Controller.PolicyFlag.POLICYDEFAULT);
            Control.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
            Control.SetPolicyFlags(Controller.PolicyFlag.POLICYDEFAULT);
            Control.SetPolicyFlags(Controller.PolicyFlag.POLICY_IMAGES);

            Listener = new CustomListener();
            Status = false;
            
        }
        public void startLeap() {
            if (!Status) 
            {
                Control.SetPolicy(Controller.PolicyFlag.POLICYDEFAULT);
                Control.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
                Control.SetPolicyFlags(Controller.PolicyFlag.POLICYDEFAULT);
                Control.SetPolicyFlags(Controller.PolicyFlag.POLICY_IMAGES);
                Control.AddListener(Listener);
                
                Status = true;
            }
            
        }
        public void stopLeap() 
        {
            if (Status) 
            {
                Status = false;
                Control.RemoveListener(Listener);
            }
            
        } 
        public Bitmap getImage() {
            if (Control.Images[0].IsValid) 
            {
                Leap.Image image = Control.Images[0];
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //set palette
                ColorPalette grayscale = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                }
                bitmap.Palette = grayscale;
                Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = image.Data;
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, image.Width * image.Height);
                bitmap.UnlockBits(bitmapData);
                return bitmap;
            }
            return null;
            
        }
        public string getMessage()
        {
            return this.Listener.output;
        }

        public void Dispose()
        {
            Control.Dispose();
        }
    }
    public class CustomListener : Listener
    {
        public string output = "";
        private Object thisLock = new Object();
        public Bitmap ImageArray { get; set; }
        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
            if (output.Length > 1000) 
            {
                output = "";
            }
            output += "\n"+line;
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
                Leap.Image image = null;
                foreach (var i in frame.Images)
                {
                    SafeWriteLine("Got a Image");
                    if (i.IsValid) 
                    {
                        image = i;
                        break;
                    }
                }
                if (image!=null) 
                {
                    Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    //set palette
                    ColorPalette grayscale = bitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                    }
                    bitmap.Palette = grayscale;
                    Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    BitmapData bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    byte[] rawImageData = image.Data;
                    System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, image.Width * image.Height);
                    bitmap.UnlockBits(bitmapData);
                    ImageArray = bitmap;
                }
                image = frame.Images[0];
                if (image.IsValid && image.Format == Leap.Image.FormatType.INFRARED)
                {
                    Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    //set palette
                    ColorPalette grayscale = bitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                    }
                    bitmap.Palette = grayscale;
                    ImageArray = bitmap;
                }

                foreach (Hand hand in frame.Hands)
                {


                    /*SafeWriteLine(string.Format("  Hand id: {0}, palm position: {1}, fingers: {2}",
                      hand.Id, hand.PalmPosition, hand.Fingers.Count));*/
                    var length = hand.Arm.WristPosition.DistanceTo(hand.Arm.ElbowPosition);
                    
                    //hand.Arm.Basis
                    SafeWriteLine(JsonConvert.SerializeObject(hand.Arm.ElbowPosition.Magnitude));
                    /*SafeWriteLine(JsonConvert.SerializeObject(hand.Arm));*/
                    SafeWriteLine(string.Format("  Hand id: {0}, palm position: {1}, Arpan Arm Width: {2}, Arpan Arm Lenght {3}",
                      hand.Id, hand.PalmPosition, hand.Arm.Width,length));
                    /*Console.WriteLine("  Hand id: {0}, palm position: {1}, fingers: {2}",
                      hand.Id, hand.PalmPosition, hand.Fingers.Count);*/
                    // Get the hand's normal vector and direction

                    /*Vector normal = hand.PalmNormal;
                    Vector direction = hand.Direction;
                    // Calculate the hand's pitch, roll, and yaw angles
                    SafeWriteLine(string.Format("  Hand pitch: {0} degrees, roll: {1} degrees, yaw: {2} degrees",
                      direction.Pitch * 180.0f / (float)Math.PI,
                      normal.Roll * 180.0f / (float)Math.PI,
                      direction.Yaw * 180.0f / (float)Math.PI));*/
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
        public override void OnDisconnect(Controller arg0)
        {
            SafeWriteLine("Disconnected");
            base.OnDisconnect(arg0);
        }
        public override void OnExit(Controller arg0)
        {
            SafeWriteLine("Exit is called");
            base.OnExit(arg0);
        }
        public override void OnServiceConnect(Controller arg0)
        {
            SafeWriteLine("Service connected");
            if (arg0.IsPolicySet(Controller.PolicyFlag.POLICY_IMAGES)) 
            {
                SafeWriteLine("Policy set successfully");
            }
            base.OnServiceConnect(arg0);
        }
        public override void OnFocusGained(Controller arg0)
        {
            SafeWriteLine("Focus gained");
            base.OnFocusGained(arg0);
        }
        public override void OnFocusLost(Controller arg0)
        {
            SafeWriteLine("Focus lost");
            base.OnFocusLost(arg0);
        }
        public override void OnDeviceChange(Controller arg0)
        {
            SafeWriteLine("Device changed");
            base.OnDeviceChange(arg0);
        }
        public override void OnServiceDisconnect(Controller arg0)
        {
            SafeWriteLine("Service disconnected");
            base.OnServiceDisconnect(arg0);
        }
        public override void OnInit(Controller arg0)
        {
            SafeWriteLine("INIT is called");
            base.OnInit(arg0);
        }
        public override void OnImages(Controller arg0)
        {
            SafeWriteLine("On image called");
            var frame = arg0.Frame();

            Leap.Image image = null;
            foreach (var i in frame.Images)
            {
                if (i.IsValid)
                {
                    image = i;
                    break;
                }
            }
            if (image != null)
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //set palette
                ColorPalette grayscale = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                }
                bitmap.Palette = grayscale;
                Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = image.Data;
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, image.Width * image.Height);
                bitmap.UnlockBits(bitmapData);
                ImageArray = bitmap;
            }
            image = frame.Images[0];
            if (image.IsValid && image.Format == Leap.Image.FormatType.INFRARED)
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //set palette
                ColorPalette grayscale = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                }
                bitmap.Palette = grayscale;
                ImageArray = bitmap;
            }


            var Images = arg0.Images;
            image = null;
            foreach (var i in Images)
            {
                if (i.IsValid)
                {
                    image = i;
                    break;
                }
            }
            if (image != null)
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //set palette
                ColorPalette grayscale = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                }
                bitmap.Palette = grayscale;
                Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                byte[] rawImageData = image.Data;
                System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, image.Width * image.Height);
                bitmap.UnlockBits(bitmapData);
                ImageArray = bitmap;
            }
            image = frame.Images[0];
            if (image.IsValid && image.Format == Leap.Image.FormatType.INFRARED)
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //set palette
                ColorPalette grayscale = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                }
                bitmap.Palette = grayscale;
                ImageArray = bitmap;
            }



            base.OnImages(arg0);
        }
    }
}
