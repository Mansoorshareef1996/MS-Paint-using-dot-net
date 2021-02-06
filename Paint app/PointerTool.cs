
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 

namespace Paint
{
  public class PointerTool : Tool
  {
    public PointerTool(ToolArgs args)
      : base(args) {
      args.pictureBox.Cursor = Cursors.Arrow;
      args.pictureBox.MouseMove += new MouseEventHandler(OnMouseMove);
    }

    private void OnMouseMove(object sender, MouseEventArgs e) {
      // show cursor location in status bar
      ShowPointInStatusBar(e.Location);
    }

    public override void UnloadTool() {
      // remove event handlers
      args.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
    }
  }
}
