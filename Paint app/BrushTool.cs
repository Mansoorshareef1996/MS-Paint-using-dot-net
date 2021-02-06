
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Windows.Forms;     // It contains classes for creating Windows-based applications that take full advantage of the rich user interface

namespace Paint
{
  // The class defines the functionality when the tool is selected and the private variables for it
  public class BrushTool : Tool
  {
    private bool drawing;
    private BrushToolType toolType;
    private Graphics bmpGraphics;
    private Point prevPoint;
    private Pen pen;

    // Diffrent cases for diffrent mouse click events are created and stored here
    public BrushTool(ToolArgs args, BrushToolType type)
      : base(args) {
      toolType = type;
      drawing = false;

      args.pictureBox.Cursor = Cursors.Cross;
      args.pictureBox.MouseDown += new MouseEventHandler(OnMouseDown);
      args.pictureBox.MouseMove += new MouseEventHandler(OnMouseMove);
      args.pictureBox.MouseUp += new MouseEventHandler(OnMouseUp);
    }
    
    // When the mouse is clicked up the following actions take place
    private void OnMouseUp(object sender, MouseEventArgs e) {
      drawing = false;
      args.pictureBox.Invalidate();

      pen.Dispose();  // Releases all resources used by this Pen.
      bmpGraphics.Dispose();  // To dispose the bmp graphics we use the bmp.dispose
      g.Dispose();
    }

    // Actions for the mousemove are defined here
    private void OnMouseMove(object sender, MouseEventArgs e) {
      Point curPoint = e.Location;
      if (drawing) {
        g.DrawLine(pen, prevPoint, curPoint);
        bmpGraphics.DrawLine(pen, prevPoint, curPoint);
        prevPoint = curPoint;
      }

      ShowPointInStatusBar(curPoint);
    }

    // The mouseDown actions are defined here
    private void OnMouseDown(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Left) {
        drawing = true;
        prevPoint = e.Location;

        if (toolType == BrushToolType.FreeBrush)
          pen = new Pen(GetBrush(false), args.settings.Width);
        else
          pen = new Pen(args.settings.SecondaryColor, args.settings.Width);

        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

        g = args.pictureBox.CreateGraphics();
        bmpGraphics = Graphics.FromImage(args.bitmap);
      }
    }

    // Actions for the unload tools are defined here
    public override void UnloadTool() {
      args.pictureBox.Cursor = Cursors.Default;
      args.pictureBox.MouseDown -= new MouseEventHandler(OnMouseDown);
      args.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
      args.pictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
    }
  }
}
