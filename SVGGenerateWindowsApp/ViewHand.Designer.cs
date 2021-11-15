
namespace SVGGenerateWindowsApp
{
    partial class ViewHand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.leapOutput = new System.Windows.Forms.PictureBox();
            this.Exit = new System.Windows.Forms.Button();
            this.OutputLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leapOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(159, 89);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(159, 149);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // leapOutput
            // 
            this.leapOutput.Location = new System.Drawing.Point(297, 12);
            this.leapOutput.Name = "leapOutput";
            this.leapOutput.Size = new System.Drawing.Size(491, 426);
            this.leapOutput.TabIndex = 2;
            this.leapOutput.TabStop = false;
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(159, 199);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(851, 28);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(46, 17);
            this.OutputLabel.TabIndex = 4;
            this.OutputLabel.Text = "label1";
            // 
            // ViewHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 445);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.leapOutput);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Name = "ViewHand";
            this.Text = "ViewHand";
            ((System.ComponentModel.ISupportInitialize)(this.leapOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.PictureBox leapOutput;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label OutputLabel;
    }
}