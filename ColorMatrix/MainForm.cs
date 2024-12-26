using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Microsoft.Win32;
using System.Drawing.Imaging;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.IO.Compression;

namespace ColorMatrix_ns
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Title: 
        ///   ColorMatrix experiment/demo program.
        /// 
        /// Purpose:
        ///   Show the effect of ColorMatrix values used to draw an overlay image.
        /// 
        /// Description:
        ///   This program loads two images, background and overlay.
        ///   There is a grid view to display the 5 x 5 color matrix. 
        ///   You can change the grid values and press Apply to see the effect of drawing
        ///   the overlay image with a colorMatrix filter.
        ///   
        /// Uses:
        ///   Uses custom User Component objects FilterGrid and ImagePanel.
        /// 
        /// Author: Dennis Lang  2010     
        /// https://landenlabs.com
        /// 
        /// </summary>
        public MainForm()
        {
            this.filterGrid = new FilterGrid("", this.BackColor);
            InitializeComponent();
            this.title.Text = this.Text = "ColorMatrix v" + Application.ProductVersion + " By:DLang 2010";

            // Adjust various parts of the UI
            ipanel1.Buttons = ImagePanel.ButtonSet.Load;
            ipanel1.buttonClick += new EventHandler(panelButton_Click);
            ipanel1.loadClick += new EventHandler(panelLoad_Click);
            ipanel1.Label = "Background";

            ipanel2.Buttons = ImagePanel.ButtonSet.Load;
            ipanel2.buttonClick += new EventHandler(panelButton_Click);
            ipanel2.loadClick += new EventHandler(panelLoad_Click);
            ipanel2.Label = "Overlay";

            ipanel3.Buttons = ImagePanel.ButtonSet.Save;
            ipanel3.Label = "Background + Overlay(colorMatrix)";
            ipanel3.loadClick += new EventHandler(panelSave_Click);

            string appName = Application.ProductName;

            // Load defaults from registry, values set when program closes.
            this.regKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\" + appName);

            string defFilterPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Filters";
            loadFilterDialog.InitialDirectory = this.regKey.GetValue("FilterPath", defFilterPath) as string;
            if (loadFilterDialog.InitialDirectory.Length == 0)
                loadFilterDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            loadImageDialog.InitialDirectory = this.regKey.GetValue("ImagePath",
               Path.GetDirectoryName(Application.ExecutablePath) + @"\Images") as string;

            // Load default filters from embedded file.
            LoadDefaultFilter();

            ipanel1.Image = (global::ColorMatrix_ns.Properties.Resources.image3);
            ipanel2.Image = (global::ColorMatrix_ns.Properties.Resources.image2);
            ApplyFilter();

            try
            {
                // Load built-in filters
                Assembly a = Assembly.GetExecutingAssembly();
                Stream zipStream = a.GetManifestResourceStream("ColorMatrix_ns.filters.gzip");
                LoadCompressedFilters(zipStream);

                // Load filters on disk.
                DirectoryInfo dirInfo = new DirectoryInfo(loadFilterDialog.InitialDirectory);
                foreach (FileInfo fileInfo in dirInfo.GetFiles("*.xml"))
                {
                    LoadFilterGridFlow(fileInfo.FullName);
                }
            }
            catch { }
        }


        float[][] m_f2Array;                // Array to store color matrix cell values
        DataTable m_dt;                     // DataTable used to manipulate Grid values.
        Microsoft.Win32.RegistryKey regKey; // Our registry key section

        /// <summary>
        /// On Close - save file paths to registry.
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                this.regKey.SetValue("FilterPath", Path.GetDirectoryName(loadFilterDialog.FileName));
                this.regKey.SetValue("ImagePath", Path.GetDirectoryName(loadImageDialog.FileName));
            }
            catch { }

            base.OnClosing(e);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Resize grid columns so entire 5x5 matrix is visible.
        /// </summary>
        /// <param name="dataGridView"></param>
        private void ResizeColumns(DataGridView dataGridView)
        {
            int extWidth = dataGridView.RowHeadersWidth + 1;
            int colWidth = (dataGridView.Width - extWidth) / dataGridView.Columns.Count;
            for (int col = 0; col < dataGridView.Columns.Count; col++)
                dataGridView.Columns[col].Width = colWidth;
        }

        /// <summary>
        /// Load embedded compressed filters.
        /// </summary>
        private void LoadCompressedFilters(Stream fileStream)
        {
            GZipStream decompressStream = new GZipStream(fileStream, CompressionMode.Decompress, false);

            BinaryFormatter bFormatter = new BinaryFormatter();

            while (true)
            {
                try
                {
                    string label = (string)bFormatter.Deserialize(decompressStream);
                    DataTable dt = (DataTable)bFormatter.Deserialize(decompressStream);
                    Image image = (Image)bFormatter.Deserialize(decompressStream);
                    FilterGrid fg = new FilterGrid(label, Color.LightBlue);
                    fg.GridView.ReadOnly = true;

                    fg.Source = dt;
                    fg.Image = image;
                    fg.clickEvent += new System.EventHandler(GridSelectionChanged);

                    this.filterFlow.Controls.Add(fg);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    break;
                }
            }

            decompressStream.Close();
        }

        /// <summary>
        /// Populate color matrix filter with Unit transform.
        /// </summary>
        private void LoadDefaultFilter()
        {
            float brightness = 1.0f;    // no change in brightness
            float contrast = 1.0f;      // normal, unit value

            float adjBrightness = brightness - 1.0f;
            // create matrix that will brighten and contrast the image
            m_f2Array = new float[][] {
                    new float[] {contrast, 0, 0, 0, 0}, // scale red
                    new float[] {0, contrast, 0, 0, 0}, // scale green
                    new float[] {0, 0, contrast, 0, 0}, // scale blue
                    new float[] {0, 0, 0, 1.0f, 0},     // don't scale alpha
                    new float[] {adjBrightness, adjBrightness, adjBrightness, 0, 1}};

            // dataGridView.Rows.Clear();
            // dataGridView.Columns.Clear();
            // dataGridView.DataBindings.Clear();
            m_dt = Fill(new DataTable("dt"), m_f2Array);
            filterGrid.Source = m_dt;
        }

        /// <summary>
        /// Fill DataGridView table with values from float array.
        /// </summary>
        /// <param name="dt">Output Table</param>
        /// <param name="f2Array">Input 5x5 float array</param>
        /// <returns></returns>
        private DataTable Fill(DataTable dt, float[][] f2Array)
        {
            dt.Columns.Add("Red", typeof(float));  
            dt.Columns.Add("Green", typeof(float));
            dt.Columns.Add("Blue", typeof(float));
            dt.Columns.Add("Alpha", typeof(float));
            dt.Columns.Add("Int", typeof(float));

            foreach (float[] fRow in f2Array)
            {
                DataRow dRow = dt.NewRow();
                for (int col = 0; col < fRow.Length; col++)
                    dRow[col] = fRow[col];
                dt.Rows.Add(dRow);
            }

            dt.AcceptChanges();
            return dt;
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// Adjust data grid values as slider moves.
        /// </summary>
        private void numBar_Scroll(object sender, EventArgs e)
        {
            float baseValue = (float)numMinBox.Value;
            float rangeValue = (float)(numMaxBox.Value - numMinBox.Value);
            foreach (DataGridViewCell cell in this.filterGrid.GridView.SelectedCells)
            {
                cell.Value = numBar.Value / 100.0f * rangeValue + baseValue;
            }

            ApplyFilter();
        }

        #region ==== Draw methods
        /// <summary>
        /// Render overlay with active color matrix on top of background image.
        /// </summary>
        private void ApplyFilter()
        {
            if (ipanel1.Image != null && ipanel2.Image != null)
            {
                Bitmap newImage = new Bitmap(ipanel1.Image);
                Graphics g = Graphics.FromImage(newImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                DrawImage(g, ipanel2.Image);
                ipanel3.Image = newImage;
                filterGrid.Image = newImage;
            }
        }

        private void DrawImage(Graphics g, Image image)
        {
            m_dt = (DataTable)filterGrid.Source;
            for (int r = 0; r < m_f2Array.Length; r++)
            {
                m_dt.Rows[r].ItemArray.CopyTo(m_f2Array[r], 0);
            }

            float gamma = 1.0f;         // no change in gamma
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(m_f2Array), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);

            Rectangle rect = new Rectangle(Point.Empty, image.Size);
            g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
        }
        #endregion

        #region ==== ImagePanel Quick Image load buttons
        /// <summary>
        /// Quick image load buttons, load internal images.
        /// </summary>
        private void panelButton_Click(object sender, EventArgs e)
        {
            ImagePanel.EventClick ie = (ImagePanel.EventClick)e;
            Image image = null;
            switch (ie.Button)
            {
                case "1":
                    image = (global::ColorMatrix_ns.Properties.Resources.image1);
                    break;
                case "2":
                    image = (global::ColorMatrix_ns.Properties.Resources.image2);
                    break;
                case "3":
                    image = (global::ColorMatrix_ns.Properties.Resources.image3);
                    break;
            }
            if (image != null)
            {
                ImagePanel ipanel = (ImagePanel)sender;
                ipanel.Image = image;
                ApplyFilter();
            }
        }
        #endregion

        #region ==== Load and Save Image
        private void panelLoad_Click(object sender, EventArgs e)
        {
            Image image = LoadImage();
            if (image != null)
            {
                ImagePanel ipanel = (ImagePanel)sender;
                ipanel.Image = image;
                ApplyFilter();
            }
        }

        private Image LoadImage()
        {
            if (loadImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Bitmap.FromFile(loadImageDialog.FileName);
                    return image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return null;
        }

        private void panelSave_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImagePanel ipanel = (ImagePanel)sender;
                    Image image = ipanel.Image;
                    image.Save(saveImageDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region ==== Load and Save Active Filter
        private void loadFilterBtn_Click(object sender, EventArgs e)
        {
            loadFilterDialog.Multiselect = false;
            if (loadFilterDialog.ShowDialog() == DialogResult.OK)
            {
                FilterGrid fg = LoadFilterGrid(loadFilterDialog.FileName);
                if (fg != null)
                {
                    filterGrid.Set(fg);
                }
            }
        }

        private void saveFilterBtn_Click(object sender, EventArgs e)
        {
            if (saveFilterDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filename = saveFilterDialog.FileName;
                    m_dt.WriteXml(filename, XmlWriteMode.WriteSchema);
                    ipanel3.Image.Save(Path.ChangeExtension(filename, ".png"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region ==== Load and select Filters
        private void loadFiltersBtn_Click(object sender, EventArgs e)
        {
            loadFilterDialog.Multiselect = true;
            loadFilterDialog.FileName = string.Empty;

            if (loadFilterDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in loadFilterDialog.FileNames)
                {
                    LoadFilterGridFlow(filename);
                }
            }
        }

        private void GridSelectionChanged(object obj, EventArgs e)
        {
            FilterGrid activeFg = (FilterGrid)obj;
            DataTable dt = (DataTable)activeFg.Source;
            this.filterGrid.Source = dt.Copy();
            this.filterGrid.Label = activeFg.Label;
            this.filterGrid.Image = new Bitmap(activeFg.Image);

            ApplyFilter();
        }

        /// <summary>
        ///  Data Grid Value Equal compare function.
        /// </summary>
        private bool dgvEqual(DataGridView dgv1, DataGridView dgv2)
        {
            bool eq = (dgv1.Rows.Count == dgv2.Rows.Count &&
                dgv1.ColumnCount == dgv2.ColumnCount);
            if (eq)
            {
                for (int row = 0; row < dgv1.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv1.ColumnCount; col++)
                    {
                        float f1 = (float)dgv1.Rows[row].Cells[col].Value;
                        float f2 = (float)dgv2.Rows[row].Cells[col].Value;
                        if (f1 != f2) 
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Load filters into selection gallery (uses a layoutflow control).
        /// </summary>
        /// <param name="filename">Filename of previously serialized filter object</param>
        private void LoadFilterGridFlow(string filename)
        {
            FilterGrid fg = LoadFilterGrid(filename);
            if (fg != null)
            {
                fg.clickEvent += new System.EventHandler(GridSelectionChanged);
                fg.GridView.ReadOnly = true;

                bool duplicate = false;
                foreach (FilterGrid fgScan in filterFlow.Controls)
                {
                    if (dgvEqual(fg.GridView, fgScan.GridView))
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (!duplicate)
                    this.filterFlow.Controls.Add(fg);
            }
        }

        /// <summary>
        /// Load previously serialize filter grid.
        /// </summary>
        private FilterGrid LoadFilterGrid(string filename)
        {
            FilterGrid fg = null;
            try
            {
                DataTable dt = new DataTable();
                dt.ReadXml(filename);

                fg = new FilterGrid(Path.GetFileName(filename), Color.LightBlue);
                fg.Source = dt;

                try
                {
                    Image image = Bitmap.FromFile(Path.ChangeExtension(filename, ".png"));
                    fg.Image = image;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return fg;
        }
        #endregion


        ///  The following methods are not normally exposed to the user.
        ///  I use them to create the sample color matrix gallery which gets embedded in the application.
        #region ==== Internal functions to save compressed filters and load compressed filters
       
        private void saveFiltersBtn_Click(object sender, EventArgs e)
        {
            if (saveFilterDialog.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = File.OpenWrite(saveFilterDialog.FileName);
                GZipStream compressStream = new GZipStream(fileStream, CompressionMode.Compress, false);

                BinaryFormatter bFormatter = new BinaryFormatter();

                foreach (Control control in filterFlow.Controls)
                {
                    FilterGrid fg = (FilterGrid)control;
                    bFormatter.Serialize(compressStream, fg.Label);
                    bFormatter.Serialize(compressStream, fg.Source);
                    bFormatter.Serialize(compressStream, fg.Image);
                }

                compressStream.Close();
            }

        }

        private void LoadCompressedFiltersBtn_Click(object sender, EventArgs e)
        {
            if (loadFilterDialog.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = File.OpenRead(loadFilterDialog.FileName);
                LoadCompressedFilters(fileStream);
            }
        }
        #endregion

        /// Collection of mouse handles to support moving & resizing of our custom frameless dialog.
        #region ==== Move Drag
        
        Point lastLoc = Point.Empty;
        private void MouseLeave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            mainPanel.Visible = true;
        }

        private void MouseEnter_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Cross;
            lastLoc = Point.Empty;
        }

        private void MouseMove_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouseLoc = System.Windows.Forms.Control.MousePosition;

                if (lastLoc != Point.Empty)
                {
                    Point delta = new Point(mouseLoc.X - lastLoc.X, mouseLoc.Y - lastLoc.Y);
                    if (Math.Abs(delta.X) < 5 && Math.Abs(delta.Y) < 5)
                        return;
                    this.Location = new Point(Location.X + delta.X, Location.Y + delta.Y);
                }
                lastLoc = mouseLoc;
                return;
            }

            lastLoc = Point.Empty;
        }

        private void MouseSizeEnter_Click(object sender, EventArgs e)
        {
            string corner = (string)((Panel)sender).Tag;
            switch (corner)
            {
                case "upperright":
                case "lowerleft":
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case "upperleft":
                case "lowerright":
                    this.Cursor = Cursors.SizeNWSE;
                    break;
            }

            lastLoc = Point.Empty;
        }

        Point delta = Point.Empty;
        private void MouseSizeMove_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouseLoc = System.Windows.Forms.Control.MousePosition;

                if (lastLoc != Point.Empty)
                {
                    delta = new Point(mouseLoc.X - lastLoc.X, mouseLoc.Y - lastLoc.Y);
                    if (Math.Abs(delta.X) < 5 && Math.Abs(delta.Y) < 5)
                        return;
                    string corner = (string)((Panel)sender).Tag;
                    switch (corner)
                    {
                        case "upperright":
                            this.Size = new Size(this.Width + delta.X, this.Height - delta.Y);
                            this.Location = new Point(Location.X, Location.Y + delta.Y);
                            break;
                        case "lowerleft":
                            this.Size = new Size(this.Width - delta.X, this.Height + delta.Y);
                            this.Location = new Point(Location.X + delta.X, Location.Y);
                            break;
                        case "upperleft":
                            this.Size = new Size(this.Width - delta.X, this.Height - delta.Y);
                            this.Location = new Point(Location.X + delta.X, Location.Y + delta.Y);
                            break;
                        case "lowerright":
                            this.Size = new Size(this.Width + delta.X, this.Height + delta.Y);
                            break;
                    }
                }
                lastLoc = mouseLoc;
                mainPanel.Visible = false;
                return;
            }

            mainPanel.Visible = true;
            delta = Point.Empty;
            lastLoc = Point.Empty;
        }
        #endregion

        #region ==== About and Help Dialog
        AboutDialog about;
        private void aboutBtn_Click(object sender, EventArgs e)
        {
            if (about != null)
                about.Dispose();
            about = new AboutDialog();
            about.Show();
        }

        HelpDialog helpDialog;
        private void helpBtn_Click(object sender, EventArgs e)
        {
            if (helpDialog == null)
                helpDialog = new HelpDialog();

            this.helpDialog.Show();
        }
        #endregion

        private void minIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maxIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = 
                (this.WindowState == FormWindowState.Maximized) ?
                FormWindowState.Normal : 
                FormWindowState.Maximized;
        }
    }
}
