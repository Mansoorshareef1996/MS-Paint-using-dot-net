
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D; // It provides advanced functionality to the Graphics
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 
using System.Drawing.Imaging; // Provides advanced GDI+ imaging functionality. Basic graphics functionality is provided by the System.Drawing namespace.

namespace Paint
{
    // The Clipboardtool class defines diffrent private variables that will be used in the paint class
  public class ClipboardTool : RectangleToolBase
  {
    private ClipboardAction action;
    private Rectangle prevRect;
    private Rectangle rect;
    private Pen delPen;
    private Pen pen;
    private Point curPoint;

    // When the clipboard tool is selected with the mouse click event a new mouse click event is created in the picture box
    public ClipboardTool(ToolArgs args, ClipboardAction action)
      : base(args) {
      this.action = action;
      args.pictureBox.MouseClick += new MouseEventHandler(OnMouseClick);
    }

    // When the right mouse is clicked and if there is an image in the picture box the image is pasted in it or it is considred to be invalid
    private void OnMouseClick(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) {
        if (Clipboard.ContainsImage()) {
          PasteImage(curPoint);
          args.pictureBox.Invalidate();
        }
      }
    }
    
    // The following is the condition which is perfomed when the mouse down is clicked in the canvas or the picturebox
    protected override void OnMouseDown(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Left) {
        drawing = true;
        sPoint = e.Location;
        g = args.pictureBox.CreateGraphics();
        pen = Pens.Black;
        delPen = new Pen(new TextureBrush(args.bitmap), 1);
      }
    }
    
    // The following is the condition which is perfomed when the mouse is moved in the picturebox
    protected override void OnMouseMove(object sender, MouseEventArgs e) {
      curPoint = e.Location;
      if (drawing) {
        // delete old
        g.DrawRectangle(delPen, prevRect);
        // draw the new rectangle
        rect = GetRectangleFromPoints(sPoint, curPoint);
        g.DrawRectangle(pen, rect);

        prevRect = rect;

        ShowPointInStatusBar(sPoint, e.Location);
      } else {
        ShowPointInStatusBar(e.Location);
      }
    }

    // The following is the condition which is perfomed when the mouse up is performedd in the canvas or the picturebox
    protected override void OnMouseUp(object sender, MouseEventArgs e) {
      drawing = false;
      if (e.Button == MouseButtons.Left) {
        if ((action == ClipboardAction.Copy) || (action == ClipboardAction.Cut)) 
                {
          // copy rectangle
          if( rect.X > 0 && rect.Y >0)
                    {
                        Bitmap copiedBmp = args.bitmap.Clone(rect, args.bitmap.PixelFormat);
                        Clipboard.SetImage(copiedBmp);
                        if (action == ClipboardAction.Cut)
                        {
                            // delete copied rectangle
                            Graphics g = Graphics.FromImage(args.bitmap);
                            g.FillRectangle(new SolidBrush(args.settings.SecondaryColor), rect);
                        }
                    }

                }
           else if (action == ClipboardAction.Paste) {
          if (Clipboard.ContainsImage())
            PasteImage(rect);
        }
        args.pictureBox.Invalidate();
      }
    }

    // The actions for the image pasting 
    private void PasteImage(Rectangle rect) {
      Graphics gc = Graphics.FromImage(args.bitmap);
      gc.DrawImage(Clipboard.GetImage(), rect);
    }

    private void PasteImage(Point p) {
      Graphics gc = Graphics.FromImage(args.bitmap);
      gc.DrawImage(Clipboard.GetImage(), p);
    }

    // The actions for the Unload tool is defined which is used in the paintform when cut or paste is called
    public override void UnloadTool() {
      base.UnloadTool();
      args.pictureBox.MouseClick -= new MouseEventHandler(OnMouseClick);
    }
  }
}
