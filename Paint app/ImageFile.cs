
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality

namespace Paint
{
    // Image file class defines the functionalities when an image is loaded
    public class ImageFile
    {
        private string fileName; // The file name is defined as string
        private Bitmap bitmap;   // bitmap pixel is also defined

        // Image file has the variable size and the background colour as variables
        public ImageFile(Size size, Color backColor)
        {
            fileName = null;
            bitmap = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // Fill by background color
            Graphics g = Graphics.FromImage(Bitmap);
            g.Clear(backColor);
            g.Dispose();
        }

        // Disposing the image file when not needed
        ~ImageFile()
        {
            bitmap.Dispose();
        }

        // When an image file is opned the following action are done
        public bool Open(string file)
        {
            try
            {
                bitmap = new Bitmap(file); // A new file is opned and saved in the bitmap 
                fileName = file; // File is the one which have the name of the file
                return true; // if it is found it returns true
            }
            // If the image is not opned it returns a false 
            catch
            {
                return false;
            }
        }

        // Actions performed when an image file is saved is defined here
        public bool Save(string file)
        {
            try
            {
                bitmap.Save(file); // The file to be saved is passed to the bitmap and the name of the file is saved
                fileName = file;   // The file name has the name of the file
                return true;       //  If the image is saved it returns true
            }

            // If the image is not saved it returns a false 
            catch
            {
                return false;
            }
        }

        // It gives the file name which is to opned or saved
        public string FileName
        {
            get { return fileName; }
        }

        // It returns the pixels which is to used 
        public Bitmap Bitmap
        {
            get { return bitmap; }
        }
    }
}
