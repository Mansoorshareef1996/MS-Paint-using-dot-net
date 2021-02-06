
using System;
using System.Drawing; //Provides access to  basic graphics functionality.
using System.Windows.Forms; // Contains classes for creating Windows-based applications 

namespace Paint
{
    //This is class to support texttool inherited from parent tool abstract class 
    public class TextTool : Tool
    {
        public TextTool(ToolArgs args)
            : base(args)
        {
            args.pictureBox.Cursor = Cursors.Cross;
            args.pictureBox.MouseUp += new MouseEventHandler(OnMouseUp);
            args.pictureBox.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        //Method to handle mouse move event 
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            args.panel1.Text = e.Location.ToString();
            args.panel2.Text = "";
        }

        //Method to handle mouse up event
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            TextDialog textDlg = new TextDialog();
            if (textDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Graphics g = Graphics.FromImage(args.bitmap);
                g.DrawString(textDlg.ReturnText, textDlg.TextFont, GetBrush(false), e.Location);
                args.pictureBox.Invalidate();
            }
        }
        //Overiding parent method  to unload tools
        public override void UnloadTool()
        {
            args.pictureBox.Cursor = Cursors.Default;
            args.pictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
            args.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
        }
    }
}
