

using System; // Allows to type method names of members of the System namespace without typing the word System every time
using System.Collections.Generic;  // Contains interfaces and classes that define generic collections
using System.Drawing; //Contains Drawing specifc libraries 
using System.Windows.Forms; // Contains classes to dynamically generate controls  

namespace Paint
{
    //This class is created to provide Undo Feature in project.
    public class UndoFeature
    {
        //Use stack for LIFO approach where latest added object will be omitted to redraw image

        public Stack<Bitmap> UndoStack = new Stack<Bitmap>();
        public Stack<Bitmap> RedoStack = new Stack<Bitmap>();
        Bitmap bitmapObj;
        Graphics graphObj;


        //This method will add current bitmap object clone to undo stack and clearup redo stack
        //This method gets called from all the mouse events (Line , Paint , Brush etc)
        public void AddToUndo(Bitmap bitmap)
        {
            this.UndoStack.Push((Bitmap)bitmap.Clone());
            this.RedoStack.Clear();
        }

        //This is actual implementation of reading values from stack and redrawing them on canvas
        public void GetUndo(Bitmap bitmap)
        {
            if (this.UndoStack.Count > 0)
            {
                this.RedoStack.Push((Bitmap)bitmap.Clone());
                bitmapObj = this.UndoStack.Pop();
                graphObj = Graphics.FromImage(bitmapObj);
            }
            else
            {
                MessageBox.Show("Please edit canvas before editing");
            }
        }
    }
}
