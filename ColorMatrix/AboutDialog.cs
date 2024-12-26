using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ColorMatrix_ns
{
    /// <summary>
    /// Show program "About" information, such as version# and author.
    /// 
    /// Add some eye candy and spin box in and out on Show and Close.
    /// This code has one close spin effect and two open spin effects. 
    /// 
    /// All of the effects follow the following flow:
    /// 1. Grab screen shot image of dialog (works even if dialog is not visible)
    /// 2. Create dummy frameless dialog to attach screen shot image to.
    /// 3. Use timer to advance effect (rotate, scale and move image & dialog)
    /// 4. When effect is done, stop timer
    /// 5. If close mode, hide everything, if open mode display real dialog and hide dummy.
    /// 
    /// Author: Dennis Lang  2010     
    /// https://landenlabs.com
    /// 
    /// </summary>
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            this.Text = 
                this.label1.Text = 
                string.Format("ColorMatrix v{0}\nBy:  Dennis Lang  2010", Application.ProductVersion);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            CloseSpin();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OpenEffect1();      // See OpenEffect2() for another choice.
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            System.Diagnostics.Process.Start(linkLabel.Text);
        }

        /// Following methods allow the frameless dialog to be moved around the screen.
        #region ==== Move Drag

        private void MouseLeave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void MouseEnter_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Cross;
            lastLoc = Point.Empty;
        }

        Point lastLoc = Point.Empty;
        private void MouseMove_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouseLoc = System.Windows.Forms.Control.MousePosition;

                if (lastLoc != Point.Empty)
                {
                    Point delta = new Point(mouseLoc.X - lastLoc.X, mouseLoc.Y - lastLoc.Y);
                    if (Math.Abs(delta.X) < 5 && Math.Abs(delta.Y) < 5)
                        return;
                    this.Location = new Point(Location.X + delta.X, Location.Y + delta.Y);
                }
                lastLoc = mouseLoc;
                return;
            }

            lastLoc = Point.Empty;
        }
        #endregion

        /// All of the effects follow the following flow:
        /// 1. Grab screen shot image of dialog (works even if dialog is not visible)
        /// 2. Create dummy frameless dialog to attach screen shot image to.
        /// 3. Use timer to advance effect (rotate, scale and move image & dialog)
        /// 4. When effect is done, stop timer
        /// 5. If close mode, hide everything, if open mode display real dialog and hide dummy.
        #region ==== Spin effect
        /// <summary>
        /// Capture image of dialog (screen shot)
        /// </summary>
        /// <returns></returns>
        public Bitmap MakeScreenImage()
        {
            Bitmap image = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(image, new Rectangle(Point.Empty, image.Size));
            image.MakeTransparent(this.TransparencyKey);
            return image;
        }


        Form spinForm;
        Bitmap image;
        float rotateAngle = 0.5f;
        Rectangle screenRect;

        #region ==== Spin Close effect

        private void CloseSpin() 
        {
            screenRect = Screen.GetWorkingArea(this);
            image = MakeScreenImage();
            spinForm = new Form();
            // spinForm.DoubleBuffered = true;
            spinForm.AllowTransparency = true;
            spinForm.Opacity = 1.0;
            spinForm.FormBorderStyle = FormBorderStyle.None;
            spinForm.StartPosition = FormStartPosition.Manual;

            int maxDim = Math.Max(image.Width, image.Height);
            int bigDim = (int)(maxDim * 1.5);
            this.Visible = false;
            scale = 1.0f;
            Size delta = new Size(bigDim - image.Width, bigDim - image.Height);

            spinForm.Location = new Point(this.Location.X - delta.Width / 2, this.Location.Y - delta.Height / 2);
            spinForm.Show();
            spinForm.Size = new Size(bigDim, bigDim);
            spinForm.BackColor = Color.DarkGreen;
            spinForm.TransparencyKey = spinForm.BackColor;

            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer1.Start();
        }

        const int speed = 5;
        int xDir = speed;
        int yDir = speed;
        float scale = 1.0f;

        private void timer1_Tick(object sender, EventArgs e)
        {
            spinForm.BackgroundImage = rotateImage(image, spinForm.Size, rotateAngle, scale, null);

            rotateAngle += 5.0f;
            scale -= 0.01f;
            if (scale < 0.1)
            {
                timer1.Stop();
                this.timer1.Tick -= new System.EventHandler(this.timer1_Tick);
                spinForm.Visible = false;
                // this.Visible = true;
                return;
            }

            Rectangle formRect = new Rectangle(spinForm.Location, spinForm.Size);
            if (screenRect.Contains(formRect) == false)
            {
                if (screenRect.Right - formRect.Right < speed)
                    xDir = -speed;

                if (formRect.Left - screenRect.Left < speed)
                    xDir = speed;

                if (formRect.Top - screenRect.Top < speed)
                    yDir = speed;

                if (screenRect.Bottom - formRect.Bottom < speed)
                    yDir = -speed;

                spinForm.Location = new Point(spinForm.Location.X + xDir, spinForm.Location.Y + yDir);
            }
            spinForm.Location = new Point(spinForm.Location.X + xDir, spinForm.Location.Y + yDir);
        }
        #endregion

        #region ==== Spin open effect#1
        private void OpenEffect1()
        {
            this.Visible = false;
            this.Size = new Size(350, 300);
            image = MakeScreenImage();
            spinForm = new Form();
            spinForm.AllowTransparency = true;
            spinForm.Opacity = 1.0;
            spinForm.FormBorderStyle = FormBorderStyle.None;
            spinForm.StartPosition = FormStartPosition.CenterParent;

            int maxDim = Math.Max(image.Width, image.Height);
            int bigDim = (int)(maxDim * 1.5);
            scale = 0;
            rotateAngle = 0;
            Size delta = new Size(bigDim - image.Width, bigDim - image.Height);

            // spinForm.Location = new Point(this.Location.X - delta.Width / 2, this.Location.Y - delta.Height / 2);
            spinForm.Size = new Size(bigDim, bigDim);
            spinForm.BackColor = Color.DarkGreen;
            spinForm.TransparencyKey = spinForm.BackColor;
            spinForm.Show();
            this.Visible = false;
            this.timer1.Tick += new System.EventHandler(this.timer2_Tick);
            this.timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            float scaleStep = (5 / 360.0f);
            if (scale >= 1)
            {
                timer1.Stop();
                this.timer1.Tick -= new System.EventHandler(this.timer2_Tick);

                this.Location = new Point(spinForm.Location.X + (spinForm.Width - image.Width) / 2,
                    spinForm.Location.Y + (spinForm.Height - image.Height) / 2);

                spinForm.Close();
                this.Visible = true;
            }
            else
                scale += scaleStep;
            spinForm.BackgroundImage = rotateImage(image, spinForm.Size, rotateAngle, scale, null);
            rotateAngle += 5.0f;
        }
        #endregion

        #region ==== Spin open effect#2
        private void OpenEffect2()
        {
            // Grab screen shot of dialog (view, snap, hide)
            image = MakeScreenImage();

            // Build dummy form to hold spinning image
            spinForm = new Form();
            spinForm.AllowTransparency = true;
            spinForm.Opacity = 1.0;
            spinForm.FormBorderStyle = FormBorderStyle.None;

            // Make form larger so spinning image is not clipped
            int maxDim = Math.Max(image.Width, image.Height);
            int bigDim = (int)(maxDim * 1.5);

            Size delta = new Size(bigDim - image.Width, bigDim - image.Height);

            spinForm.StartPosition = FormStartPosition.CenterParent;
            // spinForm.Location = new Point(this.Location.X - delta.Width / 2, this.Location.Y - delta.Height / 2);
            spinForm.Size = new Size(bigDim, bigDim);
            spinForm.BackColor = Color.DarkGreen;
            spinForm.TransparencyKey = spinForm.BackColor;
            spinForm.BackgroundImage = new Bitmap(image.Width, image.Height, image.PixelFormat);
            spinForm.Show();

            scale = 0;
            rotateAngle = 0;

            this.timer1.Tick += new System.EventHandler(this.timer3_Tick);
            this.timer1.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (rotateAngle >= 360)
            {
                // Full size - stop timer, hide dummy form, make real form visible.
                timer1.Stop();
                this.timer1.Tick -= new System.EventHandler(this.timer3_Tick);
                spinForm.Close();
                this.Visible = true;
            }
            else
                scale += (float)(5 / 360.0);

            spinForm.BackgroundImage = rotateImage(image, spinForm.Size, rotateAngle, scale, spinForm.BackgroundImage);
            rotateAngle += 5.0f;
        }
        #endregion

        private Bitmap rotateImage(Bitmap b, Size size, float angle, float scale, Image prevImage)
        {
            // Create a new empty bitmap to hold rotated image
            Bitmap returnBitmap;
            if (prevImage == null)
                returnBitmap = new Bitmap(size.Width, size.Height);
            else
                returnBitmap = new Bitmap(prevImage, size.Width, size.Height);

            // Make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            if (prevImage == null)
                g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighSpeed;

            Size deltaSize = size - b.Size;
            g.TranslateTransform((float)deltaSize.Width / 2, (float)deltaSize.Height / 2);

            // Move rotation point to center of image
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            // Rotate
            g.RotateTransform(angle);
            // Move origin back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            // Draw passed in image onto graphics object
            g.ScaleTransform(scale, scale);
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }
        #endregion 

    
    }
}
