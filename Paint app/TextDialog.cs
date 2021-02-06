
/************************************************************************
 *                                                                      *
 *  CSCI 504                Assignment 4                     Fall 2020  *
 *                                                                      *
 *     Class Name:  ODE To Paint                                        *   
 *                                                                      *
 *   Developer(s): 1. Palak Jalota ( Z1884936 )                         *
 *                 2. Mohammed Mansoor Shareef ( Z1874994 )             *
 *                 3. Bhavya Sai Nalluri  ( Z1875803 )                  *  
 *                                                                      *
 *         Due Date: 29 October 2020                                    *
 *                                                                      *
 *        Purpose: An application that resembles the MS-Paint which is  *
 *                 heavily dependent on the mouse events and also have  * 
 *                 all the features of MS-Paint                         *
 *                                                                      *
 ************************************************************************/
using System;  // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing; //Provides access to  basic graphics functionality.
using System.Windows.Forms; // Contains classes for creating Windows-based applications 

namespace Paint
{
    //This is textdialog form
    public partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();
        }

        //Form button click event
        private void fontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.Font = textBox.Font;

            if (fontDlg.ShowDialog() == DialogResult.OK)
                textBox.Font = fontDlg.Font;
        }

        public Font TextFont
        {
            get { return textBox.Font; }
        }

        public string ReturnText
        {
            get { return textBox.Text; }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}