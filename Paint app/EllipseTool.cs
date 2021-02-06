
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D; // It provides advanced functionality to the Graphics
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 

namespace Paint
{
    // The class Ellipse tool is used to define the functionality of the rectangle class which have various cases defined in it
  public class EllipseTool : RectangleTool
  {
    public EllipseTool(ToolArgs args)
      : base(args) {
    }
    
     // The draw rectangle class is used to define the rectab]ngle drawn with what pen style and brush style with its height and widt in consideration
    protected override void DrawRectangle(Pen outlinePen, Brush fillBrush) {
      if (fillBrush is LinearGradientBrush) {
        if ((rect.Width > 0) && (rect.Height > 0)) {
          fillBrush = new LinearGradientBrush(rect,
                              args.settings.PrimaryColor,
                              args.settings.SecondaryColor,
                              args.settings.GradiantStyle);
        }
      }

      // Diffrent modes for the rectangle drawen are mentioned so when the selects any of the following they are done accordingly
      switch (args.settings.DrawMode) {
        // Outine mode is defined if selected by the user
        case DrawMode.Outline:
          g.DrawEllipse(outlinePen, rect);
          break;
        // Filled mode is defined if selected by the user
        case DrawMode.Filled:
          g.FillEllipse(fillBrush, rect);
          break;
        // Mixed mode is defined if selected by the user
        case DrawMode.Mixed:
          g.FillEllipse(fillBrush, rect);
          g.DrawEllipse(outlinePen, rect);
          break;
         // Mixed with solid out line mode is defined if selected by the user
        case DrawMode.MixedWithSolidOutline:
          g.FillEllipse(fillBrush, rect);
          g.DrawEllipse(outlinePen, rect);
          break;
      }
    }
  }
}
