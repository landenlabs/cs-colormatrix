using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;

namespace ColorMatrix_ns {
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
	public partial class AboutDialog : Form {
		private bool animClosing = false; // track when we're letting the form actually close after animation

		public AboutDialog() {
			InitializeComponent();
			/*
			// Ensure standard dialog border and control box are visible
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ControlBox = true;
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.ShowIcon = true;
			this.StartPosition = FormStartPosition.CenterParent;
			// Disable transparency key for the real dialog so borders aren't accidentally made transparent
			this.TransparencyKey = Color.Empty;
			*/

			this.Text =
				this.label1.Text =
				string.Format("ColorMatrix v{0}\nBy:  Dennis Lang  2025", Application.ProductVersion);
		}

		private void closeBtn_Click(object sender, EventArgs e) {
			this.Close();
		}

		protected override void OnFormClosing(FormClosingEventArgs e) {
			// If we haven't started the close animation, cancel the close and run it.
			if (!animClosing) {
				e.Cancel = true;
				CloseSpin();
				return;
			}

			base.OnFormClosing(e);
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			// OpenEffect1();      // See OpenEffect2() for another choice.
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			LinkLabel linkLabel = sender as LinkLabel;
			System.Diagnostics.Process.Start(linkLabel.Text);
		}

		/// Following methods allow the frameless dialog to be moved around the screen.
		#region ==== Move Drag

		private void MouseLeave_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.Default;
		}

		private void MouseEnter_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.Cross;
			lastLoc = Point.Empty;
		}

		Point lastLoc = Point.Empty;
		private void MouseMove_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Point mouseLoc = System.Windows.Forms.Control.MousePosition;

				if (lastLoc != Point.Empty) {
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
		public Bitmap MakeScreenImage() {
			Bitmap image = new Bitmap(this.Width, this.Height);
			this.DrawToBitmap(image, new Rectangle(Point.Empty, image.Size));
			image.MakeTransparent(this.TransparencyKey);
			return image;
		}

		// --- animation fields (improved for smoothing) ---
		Form spinForm;
		Bitmap image;
		float rotateAngle = 0.0f;
		Rectangle screenRect;

		Stopwatch animStopwatch = new Stopwatch();
		float animDurationSeconds = 2.0f;      // total close animation duration
		float rotateSpeedDegreesPerSec = 720f; // rotation speed
		int frameIntervalMs = 15;              // ~60 FPS

		// movement
		const int speed = 5;
		int xDir = speed;
		int yDir = speed;

		// Small subclass with double buffering enabled for smooth painting
		private class SpinForm : Form {
			public SpinForm() {
				this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
				this.DoubleBuffered = true;
				this.FormBorderStyle = FormBorderStyle.None;
			}
		}

		#region ==== Spin Close effect

		private void CloseSpin() {
			// Ensure timer has no leftover handlers
			try {
				timer1.Stop();
				timer1.Tick -= new System.EventHandler(this.timer1_Tick);
				timer1.Tick -= new System.EventHandler(this.timerOpen1_Tick);
				timer1.Tick -= new System.EventHandler(this.timerOpen2_Tick);
			} catch { }

			screenRect = Screen.GetWorkingArea(this);
			image = MakeScreenImage();
			spinForm = new SpinForm();
			spinForm.AllowTransparency = true;
			spinForm.Opacity = 1.0;
			spinForm.StartPosition = FormStartPosition.Manual;

			int maxDim = Math.Max(image.Width, image.Height);
			int bigDim = (int)(maxDim * 1.5);
			this.Visible = false;

			Size delta = new Size(bigDim - image.Width, bigDim - image.Height);
			spinForm.Location = new Point(this.Location.X - delta.Width / 2, this.Location.Y - delta.Height / 2);
			spinForm.Size = new Size(bigDim, bigDim);
			spinForm.BackColor = Color.DarkGreen;
			spinForm.TransparencyKey = spinForm.BackColor;
			spinForm.Show();

			// reset animation state
			animStopwatch.Restart();
			rotateAngle = 0f;

			// run timer for smooth frame rate
			timer1.Interval = frameIntervalMs;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			this.timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e) {
			float elapsed = animStopwatch.ElapsedMilliseconds / 1000f;
			float progress = Math.Min(1f, elapsed / animDurationSeconds);

			// time-based values
			rotateAngle = rotateSpeedDegreesPerSec * elapsed;
			float scale = 1f - progress; // shrink from 1 -> 0
			scale = Math.Max(0.01f, scale);

			// create new frame
			Bitmap frame = rotateImage(image, spinForm.Size, rotateAngle, scale, null);

			// swap background image and dispose previous
			Image prev = spinForm.BackgroundImage;
			spinForm.BackgroundImage = frame;
			if (prev != null && prev != frame) {
				try { prev.Dispose(); } catch { }
			}

			// finish condition
			if (progress >= 1f) {
				timer1.Stop();
				this.timer1.Tick -= new System.EventHandler(this.timer1_Tick);
				try { spinForm.Close(); } catch { }
				animClosing = true;
				this.Close();
				return;
			}

			// gentle movement to keep inside screen bounds
			int moveStep = 3;
			Rectangle formRect = new Rectangle(spinForm.Location, spinForm.Size);
			if (!screenRect.Contains(formRect)) {
				if (screenRect.Right - formRect.Right < moveStep) xDir = -moveStep;
				if (formRect.Left - screenRect.Left < moveStep) xDir = moveStep;
				if (formRect.Top - screenRect.Top < moveStep) yDir = moveStep;
				if (screenRect.Bottom - formRect.Bottom < moveStep) yDir = -moveStep;
			}
			spinForm.Location = new Point(spinForm.Location.X + xDir, spinForm.Location.Y + yDir);
		}
		#endregion

		#region ==== Spin open effect#1
		private float open1DurationSeconds = 0.8f;
		private float open2DurationSeconds = 1.0f;

		private void OpenEffect1() {
			// Ensure timer has no leftover handlers
			try {
				timer1.Stop();
				timer1.Tick -= new System.EventHandler(this.timer1_Tick);
				timer1.Tick -= new System.EventHandler(this.timerOpen1_Tick);
				// timer1.Tick -= new System.EventHandler(this.timerOpen2_Tick);
			} catch { }

			this.Visible = false;
			this.Size = new Size(350, 300);
			image = MakeScreenImage();
			spinForm = new SpinForm();
			spinForm.AllowTransparency = true;
			spinForm.Opacity = 1.0;
			spinForm.StartPosition = FormStartPosition.CenterParent;

			int maxDim = Math.Max(image.Width, image.Height);
			int bigDim = (int)(maxDim * 1.5);
			Size delta = new Size(bigDim - image.Width, bigDim - image.Height);

			spinForm.Size = new Size(bigDim, bigDim);
			spinForm.BackColor = Color.DarkGreen;
			spinForm.TransparencyKey = spinForm.BackColor;
			spinForm.Show();

			// reset animation state
			animStopwatch.Restart();
			rotateAngle = 0f;

			// run timer for smooth frame rate
			timer1.Interval = frameIntervalMs;
			this.timer1.Tick += new System.EventHandler(this.timerOpen1_Tick);
			this.timer1.Start();
		}

		private void timerOpen1_Tick(object sender, EventArgs e) {
			float elapsed = animStopwatch.ElapsedMilliseconds / 1000f;
			float progress = Math.Min(1f, elapsed / open1DurationSeconds);

			rotateAngle = rotateSpeedDegreesPerSec * elapsed;
			float localScale = progress; // 0 -> 1
			localScale = Math.Max(0.01f, localScale);

			// create new frame
			Bitmap frame = rotateImage(image, spinForm.Size, rotateAngle, localScale, null);

			// swap background image and dispose previous
			Image prev = spinForm.BackgroundImage;
			spinForm.BackgroundImage = frame;
			if (prev != null && prev != frame) {
				try { prev.Dispose(); } catch { }
			}

			if (progress >= 1f) {
				timer1.Stop();
				this.timer1.Tick -= new System.EventHandler(this.timerOpen1_Tick);

				// center real form on spinForm and show
				this.Location = new Point(spinForm.Location.X + (spinForm.Width - image.Width) / 2,
					spinForm.Location.Y + (spinForm.Height - image.Height) / 2);

				try { spinForm.Close(); } catch { }
				this.Visible = true;
				return;
			}
		}
		#endregion

		#region ==== Spin open effect#2
		private void OpenEffect2() {
			// Ensure timer has no leftover handlers
			try {
				timer1.Stop();
				timer1.Tick -= new System.EventHandler(this.timer1_Tick);
				timer1.Tick -= new System.EventHandler(this.timerOpen1_Tick);
				//timer1.Tick -= new System.EventHandler(this.timerOpen2_Tick);
			} catch { }

			image = MakeScreenImage();

			// Build dummy form to hold spinning image
			spinForm = new SpinForm();
			spinForm.AllowTransparency = true;
			spinForm.Opacity = 1.0;
			spinForm.FormBorderStyle = FormBorderStyle.None;

			// Make form larger so spinning image is not clipped
			int maxDim = Math.Max(image.Width, image.Height);
			int bigDim = (int)(maxDim * 1.5);

			spinForm.StartPosition = FormStartPosition.CenterParent;
			spinForm.Size = new Size(bigDim, bigDim);
			spinForm.BackColor = Color.DarkGreen;
			spinForm.TransparencyKey = spinForm.BackColor;
			spinForm.Show();

			// reset animation state
			animStopwatch.Restart();
			rotateAngle = 0f;
			// run timer
			timer1.Interval = frameIntervalMs;
			this.timer1.Tick += new System.EventHandler(this.timerOpen2_Tick);
			this.timer1.Start();
		}

		private void timerOpen2_Tick(object sender, EventArgs e) {
			float elapsed = animStopwatch.ElapsedMilliseconds / 1000f;
			float progress = Math.Min(1f, elapsed / open2DurationSeconds);

			rotateAngle = rotateSpeedDegreesPerSec * elapsed;
			float localScale = progress; // 0 -> 1
			localScale = Math.Max(0.01f, localScale);

			Bitmap frame = rotateImage(image, spinForm.Size, rotateAngle, localScale, spinForm.BackgroundImage);

			Image prev = spinForm.BackgroundImage;
			spinForm.BackgroundImage = frame;
			if (prev != null && prev != frame) {
				try { prev.Dispose(); } catch { }
			}

			if (progress >= 1f) {
				timer1.Stop();
				this.timer1.Tick -= new System.EventHandler(this.timerOpen2_Tick);
				this.Location = new Point(spinForm.Location.X + (spinForm.Width - image.Width) / 2,
					spinForm.Location.Y + (spinForm.Height - image.Height) / 2);
				try { spinForm.Close(); } catch { }
				this.Visible = true;
				return;
			}
		}
		#endregion

		private Bitmap rotateImage(Bitmap b, Size size, float angle, float scale, Image prevImage) {
			// Create a new empty bitmap to hold rotated image
			Bitmap returnBitmap = new Bitmap(size.Width, size.Height);

			using (Graphics g = Graphics.FromImage(returnBitmap)) {
				g.Clear(Color.Transparent);
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.CompositingQuality = CompositingQuality.HighQuality;

				Size deltaSize = size - b.Size;
				g.TranslateTransform((float)deltaSize.Width / 2, (float)deltaSize.Height / 2);

				// Move rotation point to center of image
				g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
				// Rotate
				g.RotateTransform(angle);
				// Move origin back
				g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
				// Draw passed in image onto graphics object
				g.ScaleTransform(Math.Max(0.01f, scale), Math.Max(0.01f, scale));
				g.DrawImage(b, new Point(0, 0));
			}

			return returnBitmap;
		}
		#endregion


	}
}
