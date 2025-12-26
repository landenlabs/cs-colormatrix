using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ColorMatrix_ns {
	public partial class MainForm : Form {
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
		public MainForm() {
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

			try {
				// Load built-in filters
				Assembly a = Assembly.GetExecutingAssembly();
				Stream zipStream = a.GetManifestResourceStream("ColorMatrix_ns.filters.gzip");
				LoadCompressedFilters(zipStream);

				// Load filters on disk.
				DirectoryInfo dirInfo = new DirectoryInfo(loadFilterDialog.InitialDirectory);
				foreach (FileInfo fileInfo in dirInfo.GetFiles("*.xml")) {
					LoadFilterGridFlow(fileInfo.FullName);
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}


		float[][] m_f2Array;                // Array to store color matrix cell values
		DataTable m_dt;                     // DataTable used to manipulate Grid values.
		Microsoft.Win32.RegistryKey regKey; // Our registry key section

		/// <summary>
		/// On Close - save file paths to registry.
		/// Use OnFormClosing instead of obsolete OnClosing.
		/// </summary>
		protected override void OnFormClosing(FormClosingEventArgs e) {
			try {
				this.regKey.SetValue("FilterPath", Path.GetDirectoryName(loadFilterDialog.FileName));
				this.regKey.SetValue("ImagePath", Path.GetDirectoryName(loadImageDialog.FileName));
			} catch { }

			base.OnFormClosing(e);
		}

		private void closeBtn_Click(object sender, EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Resize grid columns so entire 5x5 matrix is visible.
		/// </summary>
		/// <param name="dataGridView"></param>
		private void ResizeColumns(DataGridView dataGridView) {
			int extWidth = dataGridView.RowHeadersWidth + 1;
			int colWidth = (dataGridView.Width - extWidth) / dataGridView.Columns.Count;
			for (int col = 0; col < dataGridView.Columns.Count; col++)
				dataGridView.Columns[col].Width = colWidth;
		}

		/// <summary>
		/// Load embedded compressed filters.
		/// zip file with one folder per filter containing label.txt, table.xml, optional image.png
		/// </summary>
		private void LoadCompressedFilters(Stream fileStream) {
			if (fileStream == null)
				return;

			// Read stream into memory so we can attempt multiple formats safely.
			using var ms = new MemoryStream();
			fileStream.CopyTo(ms);
			ms.Position = 0;

			// Try to open as a ZIP archive first (preferred, safe format).
			try {
				using var archive = new ZipArchive(ms, ZipArchiveMode.Read, leaveOpen: false);

				// Group entries by top-level folder name (e.g. "filter0/")
				var groups = archive.Entries
					.Where(e => !string.IsNullOrEmpty(e.FullName))
					.GroupBy(e => e.FullName.Split(new[] { '/' }, 2)[0]);

				foreach (var group in groups) {
					try {
						string label = null;
						DataTable dt = null;
						Image image = null;

						foreach (var entry in group) {
							string name = entry.FullName;
							if (name.EndsWith("label.txt", StringComparison.OrdinalIgnoreCase)) {
								using var s = entry.Open();
								using var sr = new StreamReader(s, Encoding.UTF8);
								label = sr.ReadToEnd();
							} else if (name.EndsWith("table.xml", StringComparison.OrdinalIgnoreCase)) {
								dt = new DataTable();
								using var s = entry.Open();
								dt.ReadXml(s);
							} else if (name.EndsWith("image.png", StringComparison.OrdinalIgnoreCase) || name.EndsWith("image.jpg", StringComparison.OrdinalIgnoreCase) || name.EndsWith("image.jpeg", StringComparison.OrdinalIgnoreCase)) {
								using var s = entry.Open();
								image = Image.FromStream(s);
							}

							if (label != null && dt != null && image != null) {
								FilterGrid fg = new FilterGrid(label, Color.LightBlue);
								fg.GridView.ReadOnly = true;
								fg.Source = dt;
								fg.Image = image;
								fg.clickEvent += new System.EventHandler(GridSelectionChanged);
								this.filterFlow.Controls.Add(fg);
								label = null;
								dt = null;
								image = null;
							}
						}

					} catch (Exception ex) {
						MessageBox.Show(ex.Message);
					}
				}

				return;
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}

			// Legacy: attempt to treat stream as a GZip of a simple length-prefixed sequence (label, xml table, png image)
			// This is not compatible with BinaryFormatter and is only attempted if zip open fails.
			try {
				ms.Position = 0;
				using var gzip = new GZipStream(ms, CompressionMode.Decompress, leaveOpen: true);
				using var reader = new BinaryReader(gzip, Encoding.UTF8, leaveOpen: true);

				while (gzip.CanRead) {
					// read label length (int32)
					int labelLen;
					try { labelLen = reader.ReadInt32(); } catch { break; }
					if (labelLen <= 0) break;
					var labelBytes = reader.ReadBytes(labelLen);
					string label = Encoding.UTF8.GetString(labelBytes);

					// read xml length and content
					int xmlLen = reader.ReadInt32();
					var xmlBytes = reader.ReadBytes(xmlLen);
					DataTable dt = new DataTable();
					using (var xmlMs = new MemoryStream(xmlBytes)) {
						dt.ReadXml(xmlMs);
					}

					// read image length and image bytes
					int imgLen = reader.ReadInt32();
					Image image = null;
					if (imgLen > 0) {
						var imgBytes = reader.ReadBytes(imgLen);
						using var imgMs = new MemoryStream(imgBytes);
						image = Image.FromStream(imgMs);
					}

					FilterGrid fg = new FilterGrid(label, Color.LightBlue);
					fg.GridView.ReadOnly = true;
					fg.Source = dt;
					if (image != null) fg.Image = image;
					fg.clickEvent += new System.EventHandler(GridSelectionChanged);
					this.filterFlow.Controls.Add(fg);
				}
			} catch {
				// give up on unknown format
			}
		}

		/// <summary>
		/// Populate color matrix filter with Unit transform.
		/// </summary>
		private void LoadDefaultFilter() {
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
		private DataTable Fill(DataTable dt, float[][] f2Array) {
			dt.Columns.Add("Red", typeof(float));
			dt.Columns.Add("Green", typeof(float));
			dt.Columns.Add("Blue", typeof(float));
			dt.Columns.Add("Alpha", typeof(float));
			dt.Columns.Add("Int", typeof(float));

			foreach (float[] fRow in f2Array) {
				DataRow dRow = dt.NewRow();
				for (int col = 0; col < fRow.Length; col++)
					dRow[col] = fRow[col];
				dt.Rows.Add(dRow);
			}

			dt.AcceptChanges();
			return dt;
		}

		private void applyBtn_Click(object sender, EventArgs e) {
			ApplyFilter();
		}

		/// <summary>
		/// Adjust data grid values as slider moves.
		/// </summary>
		private void numBar_Scroll(object sender, EventArgs e) {
			float baseValue = (float)numMinBox.Value;
			float rangeValue = (float)(numMaxBox.Value - numMinBox.Value);
			foreach (DataGridViewCell cell in this.filterGrid.GridView.SelectedCells) {
				cell.Value = numBar.Value / 100.0f * rangeValue + baseValue;
			}

			ApplyFilter();
		}

		#region ==== Draw methods
		/// <summary>
		/// Render overlay with active color matrix on top of background image.
		/// </summary>
		private void ApplyFilter() {
			if (ipanel1.Image != null && ipanel2.Image != null) {
				Bitmap newImage = new Bitmap(ipanel1.Image);
				Graphics g = Graphics.FromImage(newImage);
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				DrawImage(g, ipanel2.Image);
				ipanel3.Image = newImage;
				filterGrid.Image = newImage;
			}
		}

		private void DrawImage(Graphics g, Image image) {
			m_dt = (DataTable)filterGrid.Source;
			for (int r = 0; r < m_f2Array.Length; r++) {
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
		private void panelButton_Click(object sender, EventArgs e) {
			ImagePanel.EventClick ie = (ImagePanel.EventClick)e;
			Image image = null;
			switch (ie.Button) {
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
			if (image != null) {
				ImagePanel ipanel = (ImagePanel)sender;
				ipanel.Image = image;
				ApplyFilter();
			}
		}
		#endregion

		#region ==== Load and Save Image
		private void panelLoad_Click(object sender, EventArgs e) {
			Image image = LoadImage();
			if (image != null) {
				ImagePanel ipanel = (ImagePanel)sender;
				ipanel.Image = image;
				ApplyFilter();
			}
		}

		private Image LoadImage() {
			if (loadImageDialog.ShowDialog() == DialogResult.OK) {
				try {
					Image image = Bitmap.FromFile(loadImageDialog.FileName);
					return image;
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}

			return null;
		}

		private void panelSave_Click(object sender, EventArgs e) {
			if (saveImageDialog.ShowDialog() == DialogResult.OK) {
				try {
					ImagePanel ipanel = (ImagePanel)sender;
					Image image = ipanel.Image;
					image.Save(saveImageDialog.FileName);
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}
		}
		#endregion

		#region ==== Load and Save Active Filter
		private void loadFilterBtn_Click(object sender, EventArgs e) {
			loadFilterDialog.Multiselect = false;
			if (loadFilterDialog.ShowDialog() == DialogResult.OK) {
				FilterGrid fg = LoadFilterGrid(loadFilterDialog.FileName);
				if (fg != null) {
					filterGrid.Set(fg);
				}
			}
		}

		private void saveFilterBtn_Click(object sender, EventArgs e) {
			if (saveFilterDialog.ShowDialog() == DialogResult.OK) {
				try {
					string filename = saveFilterDialog.FileName;
					m_dt.WriteXml(filename, XmlWriteMode.WriteSchema);
					ipanel3.Image.Save(Path.ChangeExtension(filename, ".png"));
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}
		}
		#endregion

		#region ==== Load and select Filters
		private void loadFiltersBtn_Click(object sender, EventArgs e) {
			loadFilterDialog.Multiselect = true;
			loadFilterDialog.FileName = string.Empty;

			if (loadFilterDialog.ShowDialog() == DialogResult.OK) {
				foreach (string filename in loadFilterDialog.FileNames) {
					LoadFilterGridFlow(filename);
				}
			}
		}

		private void GridSelectionChanged(object obj, EventArgs e) {
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
		private bool dgvEqual(DataGridView dgv1, DataGridView dgv2) {
			bool eq = (dgv1.Rows.Count == dgv2.Rows.Count &&
				dgv1.ColumnCount == dgv2.ColumnCount);
			if (eq) {
				for (int row = 0; row < dgv1.Rows.Count; row++) {
					for (int col = 0; col < dgv1.ColumnCount; col++) {
						float f1 = (float)dgv1.Rows[row].Cells[col].Value;
						float f2 = (float)dgv2.Rows[row].Cells[col].Value;
						if (f1 != f2) {
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
		private void LoadFilterGridFlow(string filename) {
			FilterGrid fg = LoadFilterGrid(filename);
			if (fg != null) {
				fg.clickEvent += new System.EventHandler(GridSelectionChanged);
				fg.GridView.ReadOnly = true;

				bool duplicate = false;
				foreach (FilterGrid fgScan in filterFlow.Controls) {
					if (dgvEqual(fg.GridView, fgScan.GridView)) {
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
		private FilterGrid LoadFilterGrid(string filename) {
			FilterGrid fg = null;
			try {
				DataTable dt = new DataTable();
				dt.ReadXml(filename);

				fg = new FilterGrid(Path.GetFileName(filename), Color.LightBlue);
				fg.Source = dt;

				try {
					Image image = Bitmap.FromFile(Path.ChangeExtension(filename, ".png"));
					fg.Image = image;
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}

			return fg;
		}
		#endregion


		///  The following methods are not normally exposed to the user.
		///  I use them to create the sample color matrix gallery which gets embedded in the application.
		#region ==== Internal functions to save compressed filters and load compressed filters

		private void saveFiltersBtn_Click(object sender, EventArgs e) {
			if (saveFilterDialog.ShowDialog() == DialogResult.OK) {
				// Create a ZIP archive with one folder per filter. Each folder contains:
				// - label.txt (UTF8)
				// - table.xml (DataTable.WriteXml schema + data)
				// - image.png (optional)
				using (var fs = File.Open(saveFilterDialog.FileName, FileMode.Create, FileAccess.Write))
				using (var archive = new ZipArchive(fs, ZipArchiveMode.Create, leaveOpen: false)) {
					int i = 0;
					foreach (Control control in filterFlow.Controls) {
						if (control is FilterGrid fg) {
							string folder = $"filter{i++}/";

							// label
							var labelEntry = archive.CreateEntry(folder + "label.txt", CompressionLevel.Optimal);
							using (var s = labelEntry.Open())
							using (var sw = new StreamWriter(s, Encoding.UTF8)) {
								sw.Write(fg.Label ?? string.Empty);
							}

							// table
							var tableEntry = archive.CreateEntry(folder + "table.xml", CompressionLevel.Optimal);
							using (var s = tableEntry.Open()) {
								if (fg.Source is DataTable dt) {
									dt.WriteXml(s, XmlWriteMode.WriteSchema);
								} else {
									try {
										var dt2 = fg.Source as DataTable;
										dt2?.WriteXml(s, XmlWriteMode.WriteSchema);
									} catch { }
								}
							}

							// image
							if (fg.Image != null) {
								var imgEntry = archive.CreateEntry(folder + "image.png", CompressionLevel.Optimal);
								using (var s = imgEntry.Open()) {
									fg.Image.Save(s, System.Drawing.Imaging.ImageFormat.Png);
								}
							}
						}
					}
				}

			}
		}

		private void LoadCompressedFiltersBtn_Click(object sender, EventArgs e) {
			if (loadFilterDialog.ShowDialog() == DialogResult.OK) {
				using var fileStream = File.OpenRead(loadFilterDialog.FileName);
				LoadCompressedFilters(fileStream);
			}
		}
		#endregion

		/// Collection of mouse handles to support moving & resizing of our custom frameless dialog.
		#region ==== Move Drag

		Point lastLoc = Point.Empty;
		private void MouseLeave_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.Default;
			mainPanel.Visible = true;
		}

		private void MouseEnter_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.Cross;
			lastLoc = Point.Empty;
		}

		private void MouseMove_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Point mouseLoc = System.Windows.Forms.Control.MousePosition;

				if (lastLoc != Point.Empty) {
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

		private void MouseSizeEnter_Click(object sender, EventArgs e) {
			string corner = (string)((Panel)sender).Tag;
			switch (corner) {
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
		private void MouseSizeMove_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Point mouseLoc = System.Windows.Forms.Control.MousePosition;

				if (lastLoc != Point.Empty) {
					delta = new Point(mouseLoc.X - lastLoc.X, mouseLoc.Y - lastLoc.Y);
					if (Math.Abs(delta.X) < 5 && Math.Abs(delta.Y) < 5)
						return;
					string corner = (string)((Panel)sender).Tag;
					switch (corner) {
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
		private void aboutBtn_Click(object sender, EventArgs e) {
			if (about != null)
				about.Dispose();
			about = new AboutDialog();
			about.Show();
		}

		HelpDialog helpDialog;
		private void helpBtn_Click(object sender, EventArgs e) {
			if (helpDialog == null)
				helpDialog = new HelpDialog();

			this.helpDialog.Show();
		}
		#endregion

		private void minIcon_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Minimized;
		}

		private void maxIcon_Click(object sender, EventArgs e) {
			this.WindowState =
				(this.WindowState == FormWindowState.Maximized) ?
				FormWindowState.Normal :
				FormWindowState.Maximized;
		}
	}
}
