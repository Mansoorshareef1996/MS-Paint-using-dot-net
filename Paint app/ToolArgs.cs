/**

using System.Drawing; //Provides access to  basic graphics functionality.
using System.Windows.Forms; // Contains classes for creating Windows-based applications 

namespace Paint
{
    //This class is a utility class created for form
    public class ToolArgs
    {
        public Bitmap bitmap;
        public PictureBox pictureBox;
        public StatusBarPanel panel1;
        public StatusBarPanel panel2;
        public IPaintSettings settings;

        //Initalization of all properties 
        public ToolArgs(Bitmap bmp, PictureBox picBox, StatusBarPanel p1, StatusBarPanel p2, IPaintSettings settings)
        {
            bitmap = bmp;
            pictureBox = picBox;
            panel1 = p1;
            panel2 = p2;
            this.settings = settings;
        }
    }
}
