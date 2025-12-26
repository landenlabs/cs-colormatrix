using System;
using System.IO;
using System.Reflection;        // help
using System.Windows.Forms;

namespace ColorMatrix_ns {
	/// <summary>
	/// Display embedded HTML help in .Net webbrowser window.
	/// Embedded html contains inlined base64 encoded images.
	/// 
	/// Author: Dennis Lang  2010     
	/// https://landenlabs.com
	/// 
	/// </summary>
	public partial class HelpDialog : Form {
		public HelpDialog() {
			InitializeComponent();

			// Attach the embedded html resource
			Assembly a = Assembly.GetExecutingAssembly();
			Stream htmlStream = a.GetManifestResourceStream("ColorMatrix_ns.colormatrix.html");
			this.webBrowser.DocumentStream = htmlStream;
		}

		private void closeBtn_Click(object sender, EventArgs e) {
			this.Close();
		}

		// Replace obsolete OnClosing with OnFormClosing
		protected override void OnFormClosing(FormClosingEventArgs e) {
			// keep previous behavior: hide and cancel close
			this.Visible = false;
			e.Cancel = true;
			base.OnFormClosing(e);
		}
	}
}
