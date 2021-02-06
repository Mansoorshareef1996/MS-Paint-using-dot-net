
using System;                         // Allows to type method names of members of the System namespace without typing the word System every time
using System.Collections.Generic;    // Contains interfaces and classes that define generic collections, which allow users to create strongly typed collections 
using System.ComponentModel;        // Provides classes that are used to implement the run-time and design-time behavior of components 
using System.Data;                 // Provides access to classes that represent the ADO.NET architecture.
using System.Drawing;             // It Provides access to GDI+ basic graphics functionality
using System.Drawing.Drawing2D;  // It provides advanced functionality to the Graphics
using System.Text;              // Contains classes that represent ASCII and Unicode character encodings
using System.Windows.Forms;    // It contains classes for creating Windows-based applications that take full advantage of the rich user interface
using System.IO;              // Contains types that allow reading and writing to files and data streams
using System.Drawing.Imaging;// Provides advanced GDI+ imaging functionality
using System.Linq;          // It provides classes and interfaces that support queries that use Language-Integrated Query (LINQ) 

namespace Paint
{
    public partial class PaintForm : Form, IPaintSettings
    {
        private ImageFile imageFile; // The filename is taken as variable
        private ToolArgs toolArgs;
        private Tool curTool;
        private IPaintSettings settings;  // the color setting

        MenuItem recent = new MenuItem("Recent"); // The menu item contains the recent file opened

        public readonly Stack<Bitmap> UndoStack = new Stack<Bitmap>(); // Stack is used for the undo
        public readonly Stack<Bitmap> RedoStack = new Stack<Bitmap>(); // Stack is used for the redo

        // The paint form which is opned 
        public PaintForm()
        {
            InitializeComponent();
            SetupRecentFeature();
            toolsBar.ImageList = imageList;
            settings = (IPaintSettings)this;
        }

        // When the button is clicked the following actions take place
        private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            curTool.UnloadTool();
            ToolBarButton curButton = e.Button;

            SetToolBarButtonsState(curButton);

            if (curButton == arrowBtn)
            {
                curTool = new PointerTool(toolArgs);
            }
            if (curButton == lineBtn)
            {
                curTool = new LineTool(toolArgs);
            }
            else if (curButton == rectangleBtn)
            {
                curTool = new RectangleTool(toolArgs);
            }
            else if (curButton == pencilBtn)
            {
                curTool = new PencilTool(toolArgs);
            }
            else if (curButton == brushBtn)
            {
                curTool = new BrushTool(toolArgs, BrushToolType.FreeBrush);
            }
            else if (curButton == ellipseBtn)
            {
                curTool = new EllipseTool(toolArgs);
            }
            else if (curButton == textBtn)
            {
                curTool = new TextTool(toolArgs);
            }
            else if (curButton == fillBtn)
            {
                curTool = new FillTool(toolArgs);
            }
            else if (curButton == eraserBtn)
            {
                curTool = new BrushTool(toolArgs, BrushToolType.Eraser);
            }
        }

        // The set tool bar status functionalities is defined here
        private void SetToolBarButtonsState(ToolBarButton curButton)
        {
            curButton.Pushed = true;
            foreach (ToolBarButton btn in toolsBar.Buttons)
            {
                if (btn != curButton)
                    btn.Pushed = false;
            }
        }

        // The image paint box is defined here which have it fucntionalities defined
        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            Rectangle clipRect = e.ClipRectangle;
            Bitmap b = toolArgs.bitmap.Clone(clipRect, toolArgs.bitmap.PixelFormat);
            e.Graphics.DrawImageUnscaledAndClipped(b, clipRect);
            b.Dispose();
        }

        private void PaintForm_Load(object sender, EventArgs e)
        {
            // fill (fill style) list
            for (int i = 0; i < 3; i++)
            {
                BrushType bt = (BrushType)i;
                fillStyleCombo.Items.Add(bt);
            }
            for (int i = 0; i < 53; i++)
            {
                HatchStyle hs = (HatchStyle)i;
                fillStyleCombo.Items.Add(hs);
            }
            fillStyleCombo.SelectedIndex = 0;

            // fill shape style list
            for (int i = 0; i < 4; i++)
            {
                DrawMode ss = (DrawMode)i;
                shapeStyleCombo.Items.Add(ss);
            }
            shapeStyleCombo.SelectedIndex = 0;

            // fill Width list
            for (int i = 1; i < 11; i++)
                widthCombo.Items.Add(i);
            for (int i = 15; i <= 60; i += 5)
                widthCombo.Items.Add(i);
            widthCombo.SelectedIndex = 0;

            // fill Gradiant style list
            for (int i = 0; i < 4; i++)
            {
                LinearGradientMode gm = (LinearGradientMode)i;
                gradiantStyleCombo.Items.Add(gm);
            }
            gradiantStyleCombo.SelectedIndex = 0;

            for (int i = 0; i < 4; i++)
            {
                DashStyle ds = (DashStyle)i;
                lineStyleCombo.Items.Add(ds.ToString());
            }
            lineStyleCombo.SelectedIndex = 0;

            // default texture brush image
            brushImageBox.Image = new Bitmap(20, 20);

            // default image
            imageFile = new ImageFile(new Size(500, 500), Color.White);
            ShowImage();
        }

        // The draw mode defines the paint settinf=g and return the selected index
        DrawMode IPaintSettings.DrawMode
        {
            get
            {
                return (DrawMode)shapeStyleCombo.SelectedIndex;
            }
        }

        int IPaintSettings.Width
        {
            get
            {
                return Int32.Parse(widthCombo.Text);
            }
        }

        LinearGradientMode IPaintSettings.GradiantStyle
        {
            get
            {
                return (LinearGradientMode)gradiantStyleCombo.SelectedIndex;
            }
        }

        Color IPaintSettings.PrimaryColor
        {
            get
            {
                return primColorBox.BackColor;
            }
        }

        Color IPaintSettings.SecondaryColor
        {
            get
            {
                return secColorBox.BackColor;
            }
        }

        BrushType IPaintSettings.BrushType
        {
            get
            {
                BrushType type;
                int selIndex = fillStyleCombo.SelectedIndex;
                switch (selIndex)
                {
                    case 0:
                    case 1:
                    case 2:
                        type = (BrushType)selIndex;
                        break;
                    default:
                        type = BrushType.HatchBrush;
                        break;
                }
                return type;
            }
        }

        HatchStyle IPaintSettings.HatchStyle
        {
            get
            {
                int index = fillStyleCombo.SelectedIndex;
                if (index < 3)
                    index = 0;
                else
                    index -= 3;

                return (HatchStyle)index;
            }
        }

        DashStyle IPaintSettings.LineStyle
        {
            get
            {
                return (DashStyle)lineStyleCombo.SelectedIndex;
            }
        }

        Image IPaintSettings.TextureBrushImage
        {
            get
            {
                return brushImageBox.Image;
            }
        }

        // When the color box is selected it performs the following actions
        private void ColorBox_Click(object sender, EventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;

            colorDlg.Color = picBox.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                picBox.BackColor = colorDlg.Color;
            }
        }

        // When inverse link is selcetd it performs the following functionalities
        private void inverseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Color temp = primColorBox.BackColor;
            primColorBox.BackColor = secColorBox.BackColor;
            secColorBox.BackColor = temp;
        }

        // When the clear image is selcted it performs the following
        private void imageClearMnu_Click(object sender, EventArgs e)
        {
            Graphics.FromImage(imageFile.Bitmap).Clear(settings.SecondaryColor);
            imageBox.Invalidate();
        }

        // Whne the brush image is selcetd it perfoms the following
        private void brushImageBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show(imgContainer.DisplayRectangle.ToString());
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Image Files .BMP .JPG .GIF .Png|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                brushImageBox.Image = Image.FromFile(openDlg.FileName);
            }
        }

        // When the edit option cut is selected it performs the following
        private void editCutMnu_Click(object sender, EventArgs e)
        {
            curTool.UnloadTool();
            curTool = new ClipboardTool(toolArgs, ClipboardAction.Cut);
            SetToolBarButtonsState(arrowBtn);
        }

        // // When the edit option copy is selected it performs the following
        private void editCopyMnu_Click(object sender, EventArgs e)
        {
            curTool.UnloadTool();
            curTool = new ClipboardTool(toolArgs, ClipboardAction.Copy);
            SetToolBarButtonsState(arrowBtn);
        }

        // // When the edit option paste is selected it performs the following
        private void editPasteMnu_Click(object sender, EventArgs e)
        {
            curTool.UnloadTool();
            curTool = new ClipboardTool(toolArgs, ClipboardAction.Paste);
            SetToolBarButtonsState(arrowBtn);
        }

        // When the file option click is selected it performs the following
        private void fileNewMnu_Click(object sender, EventArgs e)
        {
            if (imageFile.FileName != null)
            {
                NewDialog newDlg = new NewDialog();
                if (newDlg.ShowDialog() == DialogResult.OK)
                {
                    imageFile = new ImageFile(newDlg.ImageSize, newDlg.imageBackColor);
                    ShowImage();
                }
            }
            // If we want to open a new window it ask if we want to save the current work or not
            else
            {
                string message = "Do you want to save this image?";
                string title = "Confirmation";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    fileSaveAsMnu_Click(sender, e);
                }
                else
                {

                    NewDialog newDlg = new NewDialog();
                    if (newDlg.ShowDialog() == DialogResult.OK)
                    {
                        imageFile = new ImageFile(newDlg.ImageSize, newDlg.imageBackColor);
                        ShowImage();
                    }
                }
            }

        }

        // When the file option open is selected it opns the image in png format
        private void fileOpenMnu_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Image File .Png|*.PNG";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                if (imageFile.Open(openDlg.FileName))
                {
                    ShowImage();
                    AddRecentFileData(openDlg.FileName);
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        // // When the file option save is selected it saves the file
        private void fileSaveMnu_Click(object sender, EventArgs e)
        {
            if (imageFile.FileName != null)
            {
                if (!imageFile.Save(imageFile.FileName))
                    MessageBox.Show("Error");
                else
                    ShowImage();
                AddRecentFileData(imageFile.FileName);
            }
            else
            {
                fileSaveAsMnu_Click(sender, e);
            }
        }

        // When the file option save as  is selected it saves the image as png format
        private void fileSaveAsMnu_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "PNG (*.PNG)|*.PNG";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                if (!imageFile.Save(saveDlg.FileName))
                    MessageBox.Show("Error");
                else
                    ShowImage();
                AddRecentFileData(imageFile.FileName);
            }
        }

        // When the file option exit is selected it exist the program
        private void fileExitMnu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // when the show image is it shows the image
        private void ShowImage()
        {
            string t = imageFile.FileName;
            Text = String.Format("Paint - [{0}]", t == null ? "Untitled" : new FileInfo(t).Name);

            imageBox.ClientSize = imageFile.Bitmap.Size;
            imageBox.Invalidate();
            toolArgs = new ToolArgs(imageFile.Bitmap, imageBox, pointPanel1, pointPanel2, settings);

            if (curTool != null)
                curTool.UnloadTool();
            curTool = new PointerTool(toolArgs);
            SetToolBarButtonsState(arrowBtn);
        }
        // When the we want to view recent opned file it performs the following functions
        private void SetupRecentFeature()
        {
            File.AppendAllText("Recent.txt", Environment.NewLine);
            var lines = File.ReadAllLines("Recent.txt");
            File.WriteAllLines("Recent.txt", lines.Skip(1).ToArray());
            this.mainMenu.MenuItems.Add(0, recent);
            LoadRecent();
        }

        private void LoadRecent()
        {
            recent.MenuItems.Clear();
            var recentItems = File.ReadAllLines("Recent.txt");
            foreach (var item in recentItems)
            {
                if (item.Trim() != string.Empty)
                {
                    recent.MenuItems.Add(item);
                }
            }
        }
        private void AddRecentFileData(string savedFileName)
        {
            string filePath = "Recent.txt";
            var count = File.ReadAllLines(filePath).Length;
            if (count < 5)
            {
                File.AppendAllText(filePath, Path.GetFileName(savedFileName) + Environment.NewLine);
            }
            else
            {
                var lines = File.ReadAllLines(filePath);
                File.WriteAllLines(filePath, lines.Skip(1).ToArray());
                File.AppendAllText(filePath, Path.GetFileName(savedFileName) + Environment.NewLine);
            }
            LoadRecent();
        }

        private void helpMnu_Click(object sender, EventArgs e)
        {

        }
        // The about option displays info abou the developers
        private void helpAboutMnu_Click(object sender, EventArgs e)
        {
            string message = "The following code was developed by graduate students of NIU  Palak, Mansoor and Bhavya";
            string title = " Trio Group info";
            DialogResult result = MessageBox.Show(message, title);

        }

        private void imgContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}