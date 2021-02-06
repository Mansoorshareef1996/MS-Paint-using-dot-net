
using System;                     // Allows to type method names of members of the System namespace without typing the word System every time
using System.Drawing;            // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D; // It provides advanced functionality to the Graphics
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface 

namespace Paint
{
    // Diffrent paint settings are defined here
    public interface IPaintSettings
    {
        // Drawmode variables are fethched here
        DrawMode DrawMode
        {
            get; // It defines an accessor method in a property or indexer that returns the property value or the indexer element
        }

        // The width variable are fetched here
        int Width
        {
            get;
        }

        // Gradient style mode is defined here
        LinearGradientMode GradiantStyle
        {
            get;
        }
        // Primary colour mode is defined here
        Color PrimaryColor
        {
            get;
        }

        // Secondary colour mode is defined here
        Color SecondaryColor
        {
            get;
        }
        // Brush type is defined here
        BrushType BrushType
        {
            get;
        }

        // Htach style is defined here
        HatchStyle HatchStyle
        {
            get;
        }

        // Line style is defined here
        DashStyle LineStyle
        {
            get;
        }

        Image TextureBrushImage
        {
            get;
        }
    }
}