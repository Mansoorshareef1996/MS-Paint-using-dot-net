
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 

namespace Paint
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Code to draw paint on startup of program
            Application.Run(new PaintForm());
        }
    }
}