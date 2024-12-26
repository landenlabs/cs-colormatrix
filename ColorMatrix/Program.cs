using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ColorMatrix_ns
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// See MainForm.cs for the good stuff.
        /// 
        /// Author: Dennis Lang  2010     
        /// https://landenlabs.com
        /// 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
