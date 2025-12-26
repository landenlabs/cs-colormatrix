using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ColorMatrix_ns {
	/// <summary>
	/// Custom User Control which presents a 5x5 color matrix in a DataGridView object.
	/// The DataGridView is perfect because it manages the presentation and editing.
	/// 
	/// Author: Dennis Lang  2010     
	/// https://landenlabs.com
	/// </summary>
	public partial class FilterGrid : UserControl {
		public FilterGrid(string label, Color bgColor) {
			InitializeComponent();

			Label = label;
			LabelBackColor = bgColor;
		}

		public FilterGrid() {
			InitializeComponent();

			Label = string.Empty;
			LabelBackColor = Color.LightBlue;
		}

		public void Set(FilterGrid fg) {
			Label = fg.Label;
			Source = fg.Source;
			Image = fg.Image;
		}

		// Hide wrapper Label property from designer to avoid WFO1000 designer serialization warnings.
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Label {
			get { return label.Text; }
			set { label.Text = value; }
		}

		[Category("Appearance")]
		[Description("Background color of the label.")]
		[DefaultValue(typeof(Color), "LightBlue")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color LabelBackColor {
			get { return label.BackColor; }
			set { label.BackColor = value; }
		}

		// Designer helpers for LabelBackColor
		public bool ShouldSerializeLabelBackColor() {
			return label != null && label.BackColor != Color.LightBlue;
		}

		public void ResetLabelBackColor() {
			if (label != null) label.BackColor = Color.LightBlue;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Image {
			get { return panel.BackgroundImage; }
			set { panel.BackgroundImage = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridView GridView {
			get { return this.dataGridView; }
			set { this.dataGridView = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Source {
			get { return this.dataGridView.DataSource; }
			set {
				this.dataGridView.Columns.Clear();
				this.dataGridView.DataSource = value;

				// Resize columns to page grid into viewer
				if (dataGridView.Columns.Count > 0) {
					int extWidth = dataGridView.RowHeadersWidth + 1;
					int colWidth = (dataGridView.Width - extWidth) / dataGridView.Columns.Count;
					for (int col = 0; col < dataGridView.Columns.Count; col++)
						dataGridView.Columns[col].Width = colWidth;
				}
			}
		}

		private void label_Click(object sender, EventArgs e) {
			ClickEvent();
		}

		private void panel_Click(object sender, EventArgs e) {
			ClickEvent();
		}

		public EventHandler clickEvent;
		private void ClickEvent() {
			clickEvent?.Invoke(this, EventArgs.Empty);
		}

		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (GridView?.ReadOnly == true) {
				this.dataGridView.ClearSelection();
				ClickEvent();
			}
		}


	}
}
