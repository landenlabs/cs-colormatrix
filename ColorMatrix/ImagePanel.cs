using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ColorMatrix_ns {
	/// <summary>
	/// Custom User Control which presents an image with several quick load buttons and either
	/// an image Load or Save button. Use the Buttons property to set the panel as a Load or Save panel.
	/// 
	/// Renders image over a checkerboard background to make transparency obvious.
	/// 
	/// Author: Dennis Lang  2010     
	/// https://landenlabs.com
	/// </summary>
	public partial class ImagePanel : UserControl {
		public ImagePanel() {
			InitializeComponent();
		}

		public enum ButtonSet { Load, Save };

		private ButtonSet buttons = ButtonSet.Load;
		[Category("Behavior")]
		[DefaultValue(ButtonSet.Load)]
		[Description("Sets which buttons are visible (Load or Save).")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ButtonSet Buttons {
			get => buttons;
			set {
				buttons = value;
				switch (value) {
					case ButtonSet.Load:
						this.button1.Visible = this.button2.Visible = this.button3.Visible = true;
						this.button4.Visible = true;
						this.button4.Text = "Load...";
						toolTip.SetToolTip(this.button4, "Load image from disk");
						break;
					case ButtonSet.Save:
						this.button1.Visible = this.button2.Visible = this.button3.Visible = false;
						this.button4.Visible = true;
						this.button4.Text = "Save...";
						toolTip.SetToolTip(this.button4, "Save image to disk");
						break;
				}
			}
		}

		public bool ShouldSerializeButtons() => buttons != ButtonSet.Load;
		public void ResetButtons() => Buttons = ButtonSet.Load;

		Image m_image;
		public class EventClick : EventArgs {
			string button;
			public EventClick(string b) {
				button = b;
			}
			public string Button {
				get { return button; }
				set { button = value; }
			}
		};

		/// <summary>
		/// Render image over checkerboard.
		/// </summary>
		private Image ImageCheckerboard(Image image) {
			Image bgImage = global::ColorMatrix_ns.Properties.Resources.background;
			if (image != null) {
				Graphics g = Graphics.FromImage(bgImage);
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				Rectangle rect = new Rectangle(Point.Empty, bgImage.Size);
				g.DrawImage(image, rect);
			}
			return bgImage;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Image {
			get { return m_image; }
			set {
				m_image = value;
				panel.BackgroundImage = ImageCheckerboard(m_image);
			}
		}

		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[Description("Text displayed on the panel label.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Label {
			get { return label.Text; }
			set { label.Text = value; }
		}

		public bool ShouldSerializeLabel() {
			return label != null && !string.IsNullOrEmpty(label.Text);
		}

		public void ResetLabel() {
			if (label != null) label.Text = string.Empty;
		}

		public void SetButtonImage(int button, Image image) {
			switch (button) {
				case 1: button1.BackgroundImage = image; break;
				case 2: button2.BackgroundImage = image; break;
				case 3: button3.BackgroundImage = image; break;
			}
		}

		public EventHandler buttonClick;

		private void button_Click(object sender, EventArgs e) {
			buttonClick?.Invoke(this, new EventClick((string)((Button)sender).Tag));
		}

		public EventHandler loadClick;
		private void loadBtn_Click(object sender, EventArgs e) {
			loadClick?.Invoke(this, new EventArgs());
		}
	}
}
