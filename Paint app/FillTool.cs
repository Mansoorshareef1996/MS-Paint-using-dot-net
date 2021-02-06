
using System;                      // Allows to type method names of members of the System namespace without typing the word System every time
using System.Windows.Forms;       // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Imaging;   // Provides advanced GDI+ imaging functionalit
using System.Runtime.InteropServices; // Provides a wide variety of members that support COM interop and platform invoke services
using System.Drawing.Drawing2D;       // It provides advanced functionality to the Graphics

namespace Paint
{
  // The class fill tool defines the pixel size and the bitmap data
  public class FillTool : Tool
  {
    private int pixelSize;
    private BitmapData bData;
    // The fill tool is used to define the diffrent mouse click events when the fill coulour is selected
    public FillTool(ToolArgs args)
      : base(args) {
      args.pictureBox.Cursor = Cursors.Cross;
      args.pictureBox.MouseClick += new MouseEventHandler(OnMouseClick);
      args.pictureBox.MouseMove += new MouseEventHandler(OnMouseMove);

      Rectangle bRect = new Rectangle(new Point(0, 0), args.bitmap.Size);
      bData = args.bitmap.LockBits(bRect, ImageLockMode.ReadWrite, args.bitmap.PixelFormat);
      // pixel size in bits = image width in bytes / image width in pixels
      pixelSize = bData.Stride / bData.Width;
    }
 
    // Set the pixel size based on pixel base address
    private unsafe Color GetPixel(int x, int y) {
      byte* pixelBaseAddress = (byte*)bData.Scan0 + (y * bData.Stride) + (x * pixelSize);
      
      // If the size of the pixel is 4 then then the pixel base address is stored in the byte form as follows
      if (pixelSize == 4) {
        byte b = *pixelBaseAddress++;
        byte g = *pixelBaseAddress++;
        byte r = *pixelBaseAddress++;
        byte a = *pixelBaseAddress;

        return Color.FromArgb(a, r, g, b);
      }
            // If the size of the pixel is 3 then then the pixel base address is stored in the byte form as follows
            else if (pixelSize == 3) {
        byte b = *pixelBaseAddress++;
        byte g = *pixelBaseAddress++;
        byte r = *pixelBaseAddress;

        return Color.FromArgb(r, g, b);
      }
      // If the pixel is of the following range return empty color
      return Color.Empty;
    }
 
    // Set the pixel size based on colour
    private unsafe void SetPixel(int x, int y, Color color) {
      byte* pixelBaseAddress = (byte*)bData.Scan0 + (y * bData.Stride) + (x * pixelSize);
      // If the size of the pixel is 4 then then the color is stored in the pixel base address form as follows
      if (pixelSize == 4) {
        *pixelBaseAddress++ = color.B;
        *pixelBaseAddress++ = color.G;
        *pixelBaseAddress++ = color.R;
        *pixelBaseAddress = color.A;
      } 
      // If the size of the pixel is 3 then then the color is stored in the pixel base address form as follows
      else if (pixelSize == 3) {
        *pixelBaseAddress++ = color.B;
        *pixelBaseAddress++ = color.G;
        *pixelBaseAddress = color.R;
      }
    }

    // When the mouse is clicked up the following actions take place
    private void OnMouseMove(object sender, MouseEventArgs e) {
      ShowPointInStatusBar(e.Location);
    }

    // When the mouse is clicked  the following actions take place
    private void OnMouseClick(object sender, MouseEventArgs e) {
      Rectangle bRect = new Rectangle(new Point(0, 0), args.bitmap.Size);
      if (bRect.Contains(e.Location)) {
        args.pictureBox.Cursor = Cursors.WaitCursor;

        Color oldColor = GetPixel(e.X, e.Y);
        try {
          FloodFillScanlineStack(e.X, e.Y, args.settings.PrimaryColor, oldColor);
        } catch (Exception ex) {
          MessageBox.Show(ex.Message);
        }

        args.pictureBox.Invalidate();
        args.pictureBox.Cursor = Cursors.Cross;
      }
    }

    // The unload tool actiona are defined here
    public override void UnloadTool() {
      args.bitmap.UnlockBits(bData);
      args.pictureBox.Cursor = Cursors.Default;
      args.pictureBox.MouseClick -= new MouseEventHandler(OnMouseClick);
      args.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
    }

    private void FloodFillScanlineStack(int x, int y, Color newColor, Color oldColor) {
      if (oldColor.ToArgb() == newColor.ToArgb())
        return;

      int w = args.bitmap.Width;
      int h = args.bitmap.Height;
      PixelStack stack = new PixelStack(w, h);

      int y1;
      bool spanLeft, spanRight;

      if (!stack.Push(x, y))
        return;

      while (stack.Pop(ref x, ref y)) {
        y1 = y;
        while (y1 >= 0 && GetPixel(x, y1) == oldColor) {
          y1--;
        }
        y1++;
        spanLeft = spanRight = false;
        while (y1 < h && GetPixel(x, y1) == oldColor) {
          SetPixel(x, y1, newColor);
          if (!spanLeft && x > 0 && GetPixel(x - 1, y1) == oldColor) {
            if (!stack.Push(x - 1, y1)) return;
            spanLeft = true; ;
          } else if (spanLeft && x > 0 && GetPixel(x - 1, y1) != oldColor) {
            spanLeft = false;
          }
          if (!spanRight && x < w - 1 && GetPixel(x + 1, y1) == oldColor) {
            if (!stack.Push(x + 1, y1)) return;
            spanRight = true;
          } else if (spanRight && x < w - 1 && x < w && GetPixel(x + 1, y1) != oldColor) {
            spanRight = false;
          }
          y1++;
        }
      }
    }


    // The pixel stack is used to stroe the pixel in form of stack with the following variables defined
    private class PixelStack
    {
      private int w;
      private int h;
      private int[] stack;
      private int stackSize;
      private int stackPointer;

      public PixelStack(int width, int height) {
        w = width;
        h = height;
        stackSize = w * h;
        stack = new int[stackSize];
      }

      public bool Pop(ref int x, ref int y) {
        if (stackPointer > 0) {
          int p = stack[stackPointer];
          x = p / h;
          y = p % h;
          stackPointer--;
          return true;
        } else {
          return false;
        }
      }

      public bool Push(int x, int y) {
        if (stackPointer < stackSize - 1) {
          stackPointer++;
          stack[stackPointer] = h * x + y;
          return true;
        } else {
          return false;
        }
      }
    }
  }
}
