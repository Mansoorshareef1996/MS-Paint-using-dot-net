
using System;          // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing; // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D; // It provides advanced functionality to the Graphics

namespace Paint
{
    // All the enum list of the Clipboard are defined here which perform the clipboard actions
  public enum ClipboardAction
  {
    Cut,
    Copy,
    Paste
  }

    // All the enum list of brush types are defined
  public enum BrushToolType
  {
    Eraser,
    FreeBrush
  }

    // All the enum list of Drawmode are defined here
    public enum DrawMode
  {
    Outline,
    Filled,
    Mixed,
    MixedWithSolidOutline
  }

    // All the enum list of Brushtype are defined here
    public enum BrushType
  {
    SolidBrush,
    TextureBrush,
    GradiantBrush,
    HatchBrush
  }
}