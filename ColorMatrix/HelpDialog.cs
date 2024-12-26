using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;        // help
using System.IO;

namespace ColorMatrix_ns
{
    /// <summary>
    /// Display embedded HTML help in .Net webbrowser window.
    /// Embedded html contains inlined base64 encoded images.
    /// 
    /// Author: Dennis Lang  2010     
    /// https://landenlabs.com
    /// 
    /// </summary>
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();

            // Attach the embedded html resource
            Assembly a = Assembly.GetExecutingAssembly();
            Stream htmlStream = a.GetManifestResourceStream("ColorMatrix_ns.colormatrix.html");
            this.webBrowser.DocumentStream = htmlStream;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
