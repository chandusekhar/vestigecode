using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//using Plaza.POS.BusinessObjects;

namespace POSClient.UI.Controls
{
	public partial class ItemSelector : UserControl
	{
		#region Private Constants

		private const int MIN_ROWCOUNT = 2;
		private const int MIN_COLUMNCOUNT = 1;

		#endregion

		#region Member Variables

		private bool m_multiSelect;
		private int m_rowendex, m_colendex, m_pageindex, m_pagecount, m_level;
		private TextImageRelation m_itemTextImageRelation;
		private Color m_itemControlColor, m_actionControlColor, m_selectedControlColor, m_parentControlColor;
		private Image m_itemControlBg, m_actionControlBg;
		private List<string> m_lselectedButton;

		private string m_idField, m_parentField, m_textField, m_imageField, m_childField, m_actionField, m_argsField;
		private Dictionary<int, List<SelectableItem>> m_dataSource;
		private List<SelectableItem> m_levelSource;

		#endregion

		#region Constructor

		public ItemSelector()
		{
			InitializeComponent();

			m_rowendex = m_colendex = -1;
			m_level = m_pageindex = m_pagecount = 0;
			m_itemControlBg = m_actionControlBg = null;
			m_lselectedButton = new List<string>();

			tlpItemCont.RowStyles[0].SizeType = tlpItemCont.RowStyles[1].SizeType = SizeType.Absolute;
			RowHeight = 70;
			RowCount = 3;

			tlpItemCont.ColumnStyles[0].SizeType = tlpItemCont.ColumnStyles[1].SizeType = SizeType.Absolute;
			ColumnWidth = 70;
			ColumnCount = 5;

			ItemControlColor = Color.Transparent;
			ActionControlColor = Color.Transparent;
			ParentControlColor = Color.Transparent;
			SelectedControlColor = Color.Orange;
			
			ItemIDField = ItemImageField = ItemParentIDField = ItemTextField = ItemChildField = ItemArgsField = ItemActionField = string.Empty;
		}

		#endregion

		#region Events

		[Category("Action"), Description("EventHandler for Item Selected Event")]
		public event ItemSelectedEventHandler ItemSelected;

		[Category("Action"), Description("EventHandler for Item Un-Selected Event")]
		public event ItemUnSelectedEventHandler ItemUnSelected;

		#endregion

		#region Properties

		[Category("Appearance"), Description("The background color of item button."), DefaultValue(typeof(Color), "Transparent")]
		public Color ItemControlColor
		{
			get { return m_itemControlColor; }
			set
			{
				m_itemControlColor = value;

				this.SuspendLayout();
				for (int row = 0; row <= m_rowendex; row++)
					for (int cell = 0; cell <= (row == m_rowendex ? m_colendex : tlpItemCont.ColumnCount - 2); cell++)
						tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].BackColor = m_itemControlColor;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background image of item button.")]
		public Image ItemControlBackgroundImage
		{
			get { return m_itemControlBg; }
			set
			{
				m_itemControlBg = value;

				this.SuspendLayout();
				for (int row = 0; row <= m_rowendex; row++)
					for (int cell = 0; cell <= (row == m_rowendex ? m_colendex : tlpItemCont.ColumnCount - 2); cell++)
						tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].BackgroundImage = m_itemControlBg;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background color of action button."), DefaultValue(typeof(Color), "Transparent")]
		public Color ActionControlColor
		{
			get { return m_actionControlColor; }
			set
			{
				m_actionControlColor = value;

				this.SuspendLayout();
				btnBack.BackColor = btnUp.BackColor = btnDown.BackColor = m_actionControlColor;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background image of action button.")]
		public Image ActionControlBackgroundImage
		{
			get { return m_actionControlBg; }
			set
			{
				m_actionControlBg = value;

				this.SuspendLayout();
				btnBack.BackgroundImage = btnUp.BackgroundImage = btnDown.BackgroundImage = m_actionControlBg;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background color of selected button."), DefaultValue(typeof(Color), "Orange")]
		public Color SelectedControlColor
		{
			get { return m_selectedControlColor; }
			set
			{
				m_selectedControlColor = value;
				PopulateItems(false);
			}
		}

		[Category("Appearance"), Description("The background color of parent item button."), DefaultValue(typeof(Color), "Transparent")]
		public Color ParentControlColor
		{
			get { return m_parentControlColor; }
			set
			{
				m_parentControlColor = value;
				PopulateItems(false);
			}
		}

		[Category("Appearance"), Description("Item TextImageRelation."), DefaultValue(typeof(TextImageRelation), "Overlay")]
		public TextImageRelation ItemTextImageRelation
		{
			get { return m_itemTextImageRelation; }
			set { m_itemTextImageRelation = value; }
		}

		[Category("Behavior"), Description("Multi-Select Option."), DefaultValue(false)]
		public bool MultiSelect
		{
			get { return m_multiSelect; }
			set { m_multiSelect = value; }
		}

		[Category("Data"), Description("Class property name for itemid"), DefaultValue("")]
		public string ItemIDField
		{
			get { return m_idField; }
			set { m_idField = value; }
		}

		[Category("Data"), Description("Class property name for parentid"), DefaultValue("")]
		public string ItemParentIDField
		{
			get { return m_parentField; }
			set { m_parentField = value; }
		}

		[Category("Data"), Description("Class property name for text"), DefaultValue("")]
		public string ItemTextField
		{
			get { return m_textField; }
			set { m_textField = value; }
		}

		[Category("Data"), Description("Class property name for image"), DefaultValue("")]
		public string ItemImageField
		{
			get { return m_imageField; }
			set { m_imageField = value; }
		}

		[Category("Data"), Description("Class property name for action"), DefaultValue("")]
		public string ItemActionField
		{
			get { return m_actionField; }
			set { m_actionField = value; }
		}

		[Category("Data"), Description("Class property name for children"), DefaultValue("")]
		public string ItemChildField
		{
			get { return m_childField; }
			set { m_childField = value; }
		}

		[Category("Data"), Description("Class property name for arguments"), DefaultValue("")]
		public string ItemArgsField
		{
			get { return m_argsField; }
			set { m_argsField = value; }
		}

		[Category("Data"), Description("Number of Items at root level")]
		public int ItemCount
		{
			get { return m_dataSource == null || !m_dataSource.ContainsKey(0) ? 0 : m_dataSource[0].Count; }
		}

		[Category("Layout"), Description("Number of Rows of Items."), DefaultValue(3)]
		public int RowCount
		{
			get { return tlpItemCont.RowCount; }
			set
			{
				if (value < MIN_ROWCOUNT) throw new FormatException("Value should be greater than or equal to " + MIN_ROWCOUNT.ToString());

				this.SuspendLayout();
				tlpItemCont.RowCount = value;
				tlpItemCont.SetRow(btnBack, value - 1);
				tlpItemCont.SetRowSpan(tlpPager, value);
				this.ResumeLayout(true);
			}
		}

		[Category("Layout"), Description("Height of each row."), DefaultValue(70)]
		public float RowHeight
		{
			get { return tlpItemCont.RowStyles[0].Height; }
			set
			{
				this.SuspendLayout();
				for (int index = 0; index < tlpItemCont.RowStyles.Count; index++)
				{
					tlpItemCont.RowStyles[index].SizeType = SizeType.Absolute;
					tlpItemCont.RowStyles[index].Height = value;
				}
				this.ResumeLayout(true);
			}
		}

		[Category("Layout"), Description("Number of Columns of Items."), DefaultValue(5)]
		public int ColumnCount
		{
			get { return tlpItemCont.ColumnCount - 1; }
			set
			{
				if (value < MIN_COLUMNCOUNT) throw new FormatException("Value should be greater than or equal to " + MIN_COLUMNCOUNT.ToString());

				this.SuspendLayout();
				tlpItemCont.ColumnCount = value + 1;
				tlpItemCont.SetColumn(btnBack, value - 1);
				tlpItemCont.SetColumn(tlpPager, value);
				this.ResumeLayout(true);
			}
		}

		[Category("Layout"), Description("Width of each column."), DefaultValue(70)]
		public float ColumnWidth
		{
			get { return tlpItemCont.ColumnStyles[0].Width; }
			set
			{
				this.SuspendLayout();
				for (int index = 0; index < tlpItemCont.ColumnStyles.Count; index++)
				{
					tlpItemCont.ColumnStyles[index].SizeType = SizeType.Absolute;
					tlpItemCont.ColumnStyles[index].Width = value;
				}
				this.ResumeLayout(true);
			}
		}

		#endregion

		#region Methods

		public void ClearSelection()
		{
			if (m_lselectedButton.Count > 0)
			{
				this.SuspendLayout();
				for (int row = 0; row <= m_rowendex; row++)
					for (int cell = 0; cell <= (row == m_rowendex ? m_colendex : tlpItemCont.ColumnCount - 2); cell++)
						tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].BackColor = (m_dataSource.ContainsKey(m_level + 1) && m_dataSource[m_level + 1].Exists(delegate(SelectableItem child) { return child.ParentID.CompareTo(m_levelSource[(int)tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].Tag].ID) == 0; })) ? this.ParentControlColor : this.ItemControlColor;
				this.ResumeLayout(true);
				m_lselectedButton.Clear();
			}
		}

		public void ClearItems()
		{
			this.SuspendLayout();
			tlpItemCont.Controls.Clear();

			m_level = 0;
			if (m_dataSource != null && m_dataSource.ContainsKey(m_level)) m_levelSource = m_dataSource[m_level];
			m_pageindex = 0;
			m_pagecount = 0;

			btnBack.Visible = (m_level > 0);
			tlpItemCont.Controls.Add(tlpPager, this.ColumnCount, 0);
			tlpItemCont.SetRowSpan(tlpPager, this.RowCount);
			btnUp.Enabled = btnDown.Enabled = false;
			lblPager.Text = string.Format("{0}/{1}", 0, 0);

			m_lselectedButton.Clear();
			this.ResumeLayout(true);
		}

		public void Reset()
		{
			int maxdispcount = 0;

			m_level = 0;
			if (m_dataSource.ContainsKey(m_level))
			{
				m_levelSource = m_dataSource[m_level];

				m_pageindex = 0;
				maxdispcount = (this.RowCount * this.ColumnCount);
				m_pagecount = (m_levelSource.Count / maxdispcount) + (m_levelSource.Count % maxdispcount > 0 ? 1 : 0);
				PopulateItems(false);
				btnUp.Enabled = false;
				btnDown.Enabled = (m_pagecount > 1);
			}
			else
				ClearItems();
		}

		public void Select(params int[] indexes)
		{
			Select(true, indexes);
		}

		public void Select(bool raiseEvent, params int[] indexes)
		{
			if (m_levelSource == null || m_levelSource.Count == 0) return;
			ClearSelection();
			if (indexes == null || indexes.Length == 0) return;

			m_pageindex = 0;
			int maxdisp = (this.RowCount * this.ColumnCount) - (m_level > 0 ? 1 : 0);
			m_pagecount = (m_levelSource.Count / maxdisp) + (m_levelSource.Count % maxdisp > 0 ? 1 : 0);

			for (int index = 0; index < indexes.Length; index++)
				if (indexes[index] < m_levelSource.Count) m_lselectedButton.Add(m_levelSource[indexes[index]].ID);

			PopulateItems(raiseEvent);
		}

		public void Select(params string[] itemid)
		{
			this.Select(true, itemid);
		}

		public void Select(bool raiseEvent, params string[] itemid)
		{
			if (m_dataSource == null || m_dataSource.Count == 0) return;
			ClearSelection();
			if (itemid == null || itemid.Length == 0) return;

			int maxKeyL = -1, maxMatch = 0;
			string maxKeyP = string.Empty;
			Dictionary<int, Dictionary<string, int>> keyTable = new Dictionary<int, Dictionary<string, int>>();

			foreach (int key in m_dataSource.Keys)
			{
				keyTable.Add(key, new Dictionary<string, int>());

				for (int ii = 0; ii < itemid.Length; ii++)
				{
					for(int index=0; index<m_dataSource[key].Count; index++)
						if (m_dataSource[key][index].ID.CompareTo(itemid[ii]) == 0)
						{
							if (keyTable[key].ContainsKey(m_dataSource[key][index].ParentID))
								keyTable[key][m_dataSource[key][index].ParentID]++;
							else
								keyTable[key].Add(m_dataSource[key][index].ParentID, 1);
						}
				}
			}

			foreach(int key in keyTable.Keys)
				foreach(string pkey in keyTable[key].Keys)
					if (keyTable[key][pkey] > maxMatch) { maxKeyL = key; maxKeyP = pkey; }

			if (maxKeyL >= 0)
			{
				m_level = maxKeyL;
				m_levelSource = m_dataSource[m_level].FindAll(delegate(SelectableItem item) { return item.ParentID.CompareTo(maxKeyP) == 0; });
				m_pageindex = 0;
				int maxdisp = (this.RowCount * this.ColumnCount) - (m_level > 0 ? 1 : 0);
				m_pagecount = (m_levelSource.Count / maxdisp) + (m_levelSource.Count % maxdisp > 0 ? 1 : 0);
				if (m_levelSource != null)
				{
					for (int index = 0, row = 0, col = 0, pindex = 0; index < m_levelSource.Count; index++)
					{
						for (int ii = 0; ii < itemid.Length; ii++)
							if (m_levelSource[index].ID.CompareTo(itemid[ii]) == 0) m_lselectedButton.Add(itemid[ii]);

						col++;
						col %= this.ColumnCount;
						row += (col == 0 ? 1 : 0);
						col = (row == this.RowCount ? 0 : col);
						row %= this.RowCount;
						pindex += (index == maxdisp ? 1 : 0);
					}
				}
				PopulateItems(raiseEvent);
			}
		}

		public void LoadItems<T>(IList<T> collection)
		{
			int maxdispcount = 0;

			if (collection == null || this.ItemIDField.Trim().Length == 0) return;

			m_dataSource = SelectableItem.Load<T>(collection, this.ItemIDField, this.ItemParentIDField, this.ItemTextField, this.ItemImageField, this.ItemActionField, this.ItemChildField, this.ItemArgsField);
			m_level = 0;
			if (m_dataSource.ContainsKey(m_level))
			{
				m_levelSource = m_dataSource[m_level];

				m_pageindex = 0;
				maxdispcount = (this.RowCount * this.ColumnCount);
				m_pagecount = (collection.Count / maxdispcount) + (collection.Count % maxdispcount > 0 ? 1 : 0);
				PopulateItems(false);
				btnUp.Enabled = false;
				btnDown.Enabled = (m_pagecount > 1);
			}
			else
			{
				ClearItems();
				btnUp.Enabled = btnDown.Enabled = false;
				lblPager.Text = string.Format("{0}/{1}", m_pagecount == 0 ? 0 : m_pageindex + 1, m_pagecount);
			}
		}

        public void ReloadItems<T>(IList<T> collection, bool isFirstOrder)
        {
            int maxdispcount = 0;

            if (collection == null || this.ItemIDField.Trim().Length == 0) return;

            if (isFirstOrder)
            {
                int count = 0;
                T objToRemove;
                foreach (T item in collection)
                {

                    Type type = collection[count].GetType();
                    // object obj = collection[count];

                    System.Reflection.PropertyInfo pi = type.GetProperty("Code");   //("KIT-PRODUC")
                    object codeVal = pi.GetValue(collection[count], null);

                    if (codeVal.ToString() == "KIT-PRODUC")
                    {
                        objToRemove = item;
                        break;
                    }
                    count++;
                }
                collection.Remove(collection[count]);
            }

            m_dataSource = SelectableItem.Load<T>(collection, this.ItemIDField, this.ItemParentIDField, this.ItemTextField, this.ItemImageField, this.ItemActionField, this.ItemChildField, this.ItemArgsField);
            m_level = 0;
            if (m_dataSource.ContainsKey(m_level))
            {
                m_levelSource = m_dataSource[m_level];

                m_pageindex = 0;
                maxdispcount = (this.RowCount * this.ColumnCount);
                m_pagecount = (collection.Count / maxdispcount) + (collection.Count % maxdispcount > 0 ? 1 : 0);
                PopulateItems(false);
                btnUp.Enabled = false;
                btnDown.Enabled = (m_pagecount > 1);
            }
            else
            {
                ClearItems();
                btnUp.Enabled = btnDown.Enabled = false;
                lblPager.Text = string.Format("{0}/{1}", m_pagecount == 0 ? 0 : m_pageindex + 1, m_pagecount);
            }
        }
        private void PopulateItems(bool autoInvoke)
		{
			int maxdisp;
			Button btnItem;
			List<Button> autoButton = new List<Button>();

			tlpItemCont.Controls.Clear();
			tlpItemCont.SuspendLayout();
			tlpPager.SuspendLayout();
			//this.Visible = false;
			this.SuspendLayout();

			maxdisp = (this.RowCount * this.ColumnCount) - (m_level > 0 ? 1 : 0);
			if (m_levelSource != null)
			{
				for (int index = maxdisp * m_pageindex, row = 0, col = 0, offset = 0; index + offset < m_levelSource.Count && offset < maxdisp; offset++)
				{
					btnItem = new Button();
					btnItem.Name = "btn_" + m_pageindex.ToString() + row.ToString() + col.ToString();

					btnItem.BackgroundImage = this.ItemControlBackgroundImage;
					btnItem.FlatStyle = FlatStyle.Popup;
					btnItem.BackgroundImageLayout = ImageLayout.Stretch;

					btnItem.Image = m_levelSource[index + offset].DisplayImage;
					btnItem.TextImageRelation = this.ItemTextImageRelation;

					btnItem.Text = m_levelSource[index + offset].DisplayText;
					btnItem.Tag = index + offset;

					if (m_dataSource.ContainsKey(m_level + 1) && m_dataSource[m_level + 1].Exists(delegate(SelectableItem child) { return child.ParentID.CompareTo(m_levelSource[index + offset].ID) == 0; }))
						btnItem.BackColor = this.ParentControlColor;
					else if (m_lselectedButton.Exists(delegate(string bname) { return bname.CompareTo(m_levelSource[index + offset].ID) == 0; }))
					{
						autoButton.Add(btnItem);
						btnItem.BackColor = this.SelectedControlColor;
					}
					else
						btnItem.BackColor = this.ItemControlColor;

					btnItem.Dock = DockStyle.Fill;
					btnItem.Click += new EventHandler(ItemButton_Click);

					m_colendex = col;
					m_rowendex = row;
					tlpItemCont.Controls.Add(btnItem, col++, row);
					col %= this.ColumnCount;
					row += (col == 0 ? 1 : 0);
				}
			}

			if (m_level > 0) tlpItemCont.Controls.Add(btnBack, this.ColumnCount - 1, this.RowCount - 1);
			btnBack.Visible = (m_level > 0);
			tlpItemCont.Controls.Add(tlpPager, this.ColumnCount, 0);
			tlpItemCont.SetRowSpan(tlpPager, this.RowCount);

			lblPager.Text = string.Format("{0}/{1}", m_pagecount == 0 ? 0 : m_pageindex + 1, m_pagecount);
			tlpItemCont.ResumeLayout(false);
			tlpItemCont.PerformLayout();
			tlpPager.ResumeLayout(false);
			tlpPager.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
			//this.Visible = true;

			// AutoInvoke
			if (autoInvoke)
				for (int index = 0; index < autoButton.Count; index++)
					ItemButton_Click(autoButton[index], new EventArgs());
		}

		protected virtual void OnItemSelected(SelectableItem item)
		{
			if (ItemSelected != null) ItemSelected(this, item);
		}

		protected virtual void OnItemUnSelected(SelectableItem item)
		{
			if (ItemUnSelected != null) ItemUnSelected(this, item);
		}

		#endregion

		#region Event Handlers

		private void ActionButton_Click(object sender, EventArgs e)
		{
			int maxdispcount;

			switch (((Button)sender).Name)
			{
				case "btnBack":
					m_lselectedButton.Clear();
					m_level--;
					m_levelSource = m_dataSource[m_level].FindAll(delegate(SelectableItem parent) { return parent.ParentID.CompareTo(m_dataSource[m_level].Find(delegate(SelectableItem item) { return item.ID.CompareTo(m_levelSource[0].ParentID) == 0; }).ParentID) == 0; });
					m_pageindex = 0;
					maxdispcount = (this.RowCount * this.ColumnCount) - (m_level > 0 ? 1 : 0);
					m_pagecount = (m_levelSource.Count / maxdispcount) + (m_levelSource.Count % maxdispcount > 0 ? 1 : 0);
					btnUp.Enabled = (m_pageindex > 0);
					btnDown.Enabled = (m_pageindex < m_pagecount - 1);
					PopulateItems(false);
					break;

				case "btnUp":
					m_pageindex -= (m_pageindex == 0 ? 0 : 1);
					btnUp.Enabled = (m_pageindex > 0);
					btnDown.Enabled = true;
					PopulateItems(false);
					break;

				case "btnDown":
					m_pageindex += (m_pageindex == m_pagecount - 1 ? 0 : 1);
					btnDown.Enabled = (m_pageindex < m_pagecount - 1);
					btnUp.Enabled = true;
					PopulateItems(false);
					break;
			}
		}

		private void ItemButton_Click(object sender, EventArgs e)
		{
			int maxdispcount;
			Button isender;

			isender = ((Button)sender);
			SelectableItem item = m_levelSource[(int)isender.Tag];

			if (m_dataSource.ContainsKey(m_level + 1) && m_dataSource[m_level + 1].Exists(delegate(SelectableItem child) { return child.ParentID.CompareTo(item.ID) == 0; }))
			{
				m_level++;
				m_levelSource = m_dataSource[m_level].FindAll(delegate(SelectableItem child) { return child.ParentID.CompareTo(item.ID) == 0; });
				m_pageindex = 0;
				maxdispcount = (this.RowCount * this.ColumnCount) - 1;
				m_pagecount = (m_levelSource.Count / maxdispcount) + (m_levelSource.Count % maxdispcount > 0 ? 1 : 0);
				btnUp.Enabled = (m_pageindex > 0);
				btnDown.Enabled = (m_pageindex < m_pagecount - 1);
				PopulateItems(false);
			}
			else if (item.Action != null)
				item.Action.Invoke(item.DataInstance, item.Args);
			else
			{
				if (!this.MultiSelect)
				{
					for (int row = 0; row <= m_rowendex; row++)
						for (int cell = 0; cell <= (row == m_rowendex ? m_colendex : tlpItemCont.ColumnCount - 2); cell++)
							tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].BackColor = (m_dataSource.ContainsKey(m_level + 1) && m_dataSource[m_level + 1].Exists(delegate(SelectableItem child) { return child.ParentID.CompareTo(m_levelSource[(int)tlpItemCont.Controls["btn_" + m_pageindex.ToString() + row.ToString() + cell.ToString()].Tag].ID) == 0; })) ? this.ParentControlColor : this.ItemControlColor;
					m_lselectedButton.Clear();
				}

				if (m_lselectedButton.Exists(delegate(string bname) { return bname.CompareTo(item.ID) == 0; }))
				{
					isender.BackColor = (m_dataSource.ContainsKey(m_level + 1) && m_dataSource[m_level + 1].Exists(delegate(SelectableItem child) { return child.ParentID.CompareTo(m_levelSource[(int)isender.Tag].ID) == 0; })) ? this.ParentControlColor : this.ItemControlColor;
					m_lselectedButton.RemoveAt(m_lselectedButton.FindIndex(delegate(string bname) { return bname.CompareTo(item.ID) == 0; }));
					OnItemUnSelected(item);
				}
				else
				{
					isender.BackColor = this.SelectedControlColor;
					m_lselectedButton.Add(item.ID);
					OnItemSelected(item);
				}
			}
		}

		#endregion
	}

	public delegate void ItemSelectedEventHandler(object sender, SelectableItem i);

	public delegate void ItemUnSelectedEventHandler(object sender, SelectableItem i);
}
