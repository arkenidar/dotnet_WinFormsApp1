namespace WinFormsApp1
{
	partial class Form0 : Form
	{
		private readonly Panel buttonPanel = new();
		private readonly DataGridView songsDataGridView = new();
		private readonly Button addNewRowButton = new();
		private readonly Button deleteRowButton = new();

		private static readonly List<string[]> rows = new List<string[]>
										{
											new string[] { "13/12/1968", "29", "Revolution 9", "Beatles", "The Beatles [White Album]" },
											new string[] { "1/1/1960", "6", "Fools Rush In", "Frank Sinatra", "Nice 'N' Easy" },
											new string[] { "11/11/1971", "1", "One of These Days", "Pink Floyd", "Meddle" },
											new string[] { "1/1/1988", "7", "Where Is My Mind?", "Pixies", "Surfer Rosa" },
											new string[] { "1/5/1981", "9", "Can't Find My Mind", "Cramps", "Psychedelic Jungle" },
											new string[] { "6/10/2003", "13", "Scatterbrain. (As Dead As Leaves.)", "Radiohead", "Hail to the Thief" },
											new string[] { "6/3/1992", "3", "Dress", "P J Harvey", "Dry" }
										};

		public Form0()
		{
			this.Load += new EventHandler(Form0_Load);
		}

		private void Form0_Load(object? sender, System.EventArgs e)
		{
			SetupLayout();
			SetupDataGridView();
			PopulateDataGridView();
			Console.WriteLine(">> Initialized.");
		}

		// ...

		private void SongsDataGridView_CellFormatting(object? sender,
	System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
		{
			if (e != null)
			{
				if (this.songsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
				{
					if (e.Value != null)
					{
						try
						{
							var dateValue = e.Value.ToString();
							if (dateValue == null) throw new FormatException();
							e.Value = DateTime.Parse(dateValue)
								.ToLongDateString();
							e.FormattingApplied = true;
						}
						catch (FormatException)
						{
							Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
						}
					}
				}
			}
		}

		private void AddNewRowButton_Click(object? sender, EventArgs e)
		{
			Console.WriteLine(">> Click.");
			this.songsDataGridView.Rows.Add();
		}

		private void DeleteRowButton_Click(object? sender, EventArgs e)
		{
			Console.WriteLine(">> Click.");
			if (this.songsDataGridView.SelectedRows.Count > 0 &&
				this.songsDataGridView.SelectedRows[0].Index !=
				this.songsDataGridView.Rows.Count - 1)
			{
				this.songsDataGridView.Rows.RemoveAt(
					this.songsDataGridView.SelectedRows[0].Index);
			}
		}

		private void SetupLayout()
		{
			this.Size = new Size(600, 500);

			addNewRowButton.Text = "Add Row";
			addNewRowButton.Location = new Point(10, 10);
			addNewRowButton.Click += new EventHandler(AddNewRowButton_Click);

			deleteRowButton.Text = "Delete Row";
			deleteRowButton.Location = new Point(100, 10);
			deleteRowButton.Click += new EventHandler(DeleteRowButton_Click);

			buttonPanel.Controls.Add(addNewRowButton);
			buttonPanel.Controls.Add(deleteRowButton);
			buttonPanel.Height = 50;
			buttonPanel.Dock = DockStyle.Bottom;

			this.Controls.Add(this.buttonPanel);
		}

		private void SetupDataGridView()
		{
			this.Controls.Add(songsDataGridView);

			songsDataGridView.ColumnCount = 5;

			songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
			songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			songsDataGridView.ColumnHeadersDefaultCellStyle.Font =
				new Font(songsDataGridView.Font, FontStyle.Bold);

			songsDataGridView.Name = "songsDataGridView";
			songsDataGridView.Location = new Point(8, 8);
			songsDataGridView.Size = new Size(500, 250);
			songsDataGridView.AutoSizeRowsMode =
				DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			songsDataGridView.ColumnHeadersBorderStyle =
				DataGridViewHeaderBorderStyle.Single;
			songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			songsDataGridView.GridColor = Color.Black;
			songsDataGridView.RowHeadersVisible = false;

			songsDataGridView.Columns[0].Name = "Release Date";
			songsDataGridView.Columns[1].Name = "Track";
			songsDataGridView.Columns[2].Name = "Title";
			songsDataGridView.Columns[3].Name = "Artist";
			songsDataGridView.Columns[4].Name = "Album";
			songsDataGridView.Columns[4].DefaultCellStyle.Font =
				new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

			songsDataGridView.SelectionMode =
				DataGridViewSelectionMode.FullRowSelect;
			songsDataGridView.MultiSelect = false;
			songsDataGridView.Dock = DockStyle.Fill;

			songsDataGridView.CellFormatting += new
				DataGridViewCellFormattingEventHandler(
				SongsDataGridView_CellFormatting);
		}

		private void PopulateDataGridView()
		{
			foreach (var row in rows)
			{
				songsDataGridView.Rows.Add(row);
			}

			songsDataGridView.Columns[0].DisplayIndex = 3;
			songsDataGridView.Columns[1].DisplayIndex = 4;
			songsDataGridView.Columns[2].DisplayIndex = 0;
			songsDataGridView.Columns[3].DisplayIndex = 1;
			songsDataGridView.Columns[4].DisplayIndex = 2;
		}

	}
}
