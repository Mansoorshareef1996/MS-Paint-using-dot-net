
using System;                       // Allows to type method names of members of the System namespace without typing the word System every time
using System.Collections.Generic;  // Contains interfaces and classes that define generic collections, which allow users to create strongly typed collections 
using System.ComponentModel;      // Provides classes that are used to implement the run-time and design-time behavior of components 
using System.Data;               // Provides access to classes that represent the ADO.NET architecture.
using System.Drawing;           // It Provides access to GDI+ basic graphics functionality
using System.Text;             // Contains classes that represent ASCII and Unicode character encodings
using System.Windows.Forms;   // It contains classes for creating Windows-based applications that take full advantage of the rich user interface

namespace Paint
{
    // The new dialouge form is used when the new file is to created
    public partial class NewDialog : Form
    {
        public NewDialog()
        {
            InitializeComponent();
        }

        // The color box class is used to ask the user for the background colour of the class that is to be created
        private void colorBox_Click(object sender, EventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;

            colorDlg.Color = colorBox.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                colorBox.BackColor = colorDlg.Color;
            }
        }

        // The size of the new window is specified by the user 
        public Size ImageSize
        {
            get { return new Size((int)widthBox.Value, (int)heightBox.Value); }
        }

        // the background colour is defined here
        public Color imageBackColor
        {
            get { return colorBox.BackColor; }
        }

        private void NewDialog_Load(object sender, EventArgs e)
        {

        }

        private void heightBox_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}