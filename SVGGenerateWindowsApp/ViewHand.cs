using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leap2DataManipulation;

namespace SVGGenerateWindowsApp
{
    public partial class ViewHand : Form
    {
        Leap2DataManipulation.LeapControl control;
        public ViewHand()
        {
            InitializeComponent();
            control = new LeapControl();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            control.startLeap();
            /*Bitmap bitmap = new Bitmap("Grapes.jpg");
            leapOutput.CreateGraphics();
            ((PaintEventArgs)e).Graphics.DrawImage(bitmap, 60, 10);*/
            if (control.getImage() != null) 
            {
                //leapOutput.Image = Image.FromStream(new MemoryStream());
                leapOutput.Image = control.getImage();
            }
            OutputLabel.Text = control.getMessage();
            
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            control.stopLeap();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            control.Dispose();
            Application.Exit();
        }
    }
}
