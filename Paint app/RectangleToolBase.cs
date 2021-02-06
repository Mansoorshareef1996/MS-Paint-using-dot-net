
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D; // It provides advanced functionality to the Graphics
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 


namespace Paint
{
    public abstract class RectangleToolBase : Tool
    {
        protected bool drawing;
        protected Point sPoint;

        public RectangleToolBase(ToolArgs args)
            : base(args)
        {
            drawing = false;
            args.pictureBox.Cursor = Cursors.Cross;
            args.pictureBox.MouseDown += new MouseEventHandler(OnMouseDown);
            args.pictureBox.MouseMove += new MouseEventHandler(OnMouseMove);
            args.pictureBox.MouseUp += new MouseEventHandler(OnMouseUp);
        }

        protected abstract void OnMouseUp(object sender, MouseEventArgs e);
        protected abstract void OnMouseDown(object sender, MouseEventArgs e);
        protected abstract void OnMouseMove(object sender, MouseEventArgs e);

        public override void UnloadTool()
        {
            args.pictureBox.Cursor = Cursors.Arrow;
            args.pictureBox.MouseDown -= new MouseEventHandler(OnMouseDown);
            args.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
            args.pictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
        }
    }
}
