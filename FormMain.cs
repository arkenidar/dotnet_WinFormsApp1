using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
	partial class FormMain : Form
	{
		private readonly Panel buttonPanel = new();
		private readonly DataGridView booksDataGridView = new();
		private readonly Button addNewRowButton = new();
		private readonly Button deleteRowButton = new();

		public BookContext Context { get; }

		public FormMain()
		{
			var factory = new BookContextFactory();
			var empty = Array.Empty<string>();
			Context = factory.CreateDbContext(empty);
			this.Load += new EventHandler(Form0_Load);
		}

		private void Form0_Load(object? sender, System.EventArgs e)
		{
			// Initialize EntityFramework . ( prepare Data )
			Context.Database.EnsureCreated();
			TestEF.SeedData(Context);
			Console.WriteLine(">> Initialized EntityFramework . ( prepare Data ) ");

			// Initialize GUI . ( presenting Data )
			SetupLayout();
			SetupDataGridView();
			PopulateDataGridView();
			Console.WriteLine(">> Initialized GUI . ( presenting Data ) ");
		}

		// ...

		private void SongsDataGridView_CellFormatting(object? sender,
	System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
		{
		}

		private void AddNewRowButton_Click(object? sender, EventArgs e)
		{
			Console.WriteLine(">> Click.");
			var newRow = new string[] { "1", "2", "3", "id" };
			booksDataGridView.Rows.Add(newRow);
		}

		private void DeleteRowButton_Click(object? sender, EventArgs e)
		{
			Console.WriteLine(">> Click.");
			if (this.booksDataGridView.SelectedRows.Count > 0 &&
				this.booksDataGridView.SelectedRows[0].Index !=
				this.booksDataGridView.Rows.Count - 1)
			{
				var selected = booksDataGridView.SelectedRows[0];
				var idString = selected.Cells[selected.Cells.Count - 1].Value.ToString(); // id
				var converted = int.TryParse(idString, out int id);
				if (converted)
				{
					var book = Context.Books.Find(id);
					if (book != null)
					{
						Context.Books.Remove(book);
						Context.SaveChanges();
					}
					booksDataGridView.Rows.RemoveAt(selected.Index);
				}
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
			this.Controls.Add(booksDataGridView);

			booksDataGridView.ColumnCount = 4;

			booksDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
			booksDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			booksDataGridView.ColumnHeadersDefaultCellStyle.Font =
				new Font(booksDataGridView.Font, FontStyle.Bold);

			booksDataGridView.Name = "booksDataGridView";
			booksDataGridView.Location = new Point(8, 8);
			booksDataGridView.Size = new Size(500, 250);
			booksDataGridView.AutoSizeRowsMode =
				DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			booksDataGridView.ColumnHeadersBorderStyle =
				DataGridViewHeaderBorderStyle.Single;
			booksDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			booksDataGridView.GridColor = Color.Black;
			booksDataGridView.RowHeadersVisible = false;

			booksDataGridView.Columns[0].Name = "Title";
			booksDataGridView.Columns[1].Name = "Author";
			booksDataGridView.Columns[2].Name = "PublicationYear";
			booksDataGridView.Columns[3].Name = "Id";

			booksDataGridView.SelectionMode =
				DataGridViewSelectionMode.FullRowSelect;
			booksDataGridView.MultiSelect = false;
			booksDataGridView.Dock = DockStyle.Fill;

			booksDataGridView.CellFormatting += new
				DataGridViewCellFormattingEventHandler(
				SongsDataGridView_CellFormatting);
		}

		private void PopulateDataGridView()
		{
			var books = Context.Books.Include(b => b.Author).ToList();
			foreach (var book in books)
			{
				string title = book.Title ?? string.Empty;
				string author = book.Author?.Name ?? string.Empty;
				string publicationYear = book.PublicationYear.ToString();
				string id = book.Id.ToString();

				var row = new string[] { title, author, publicationYear, id };
				booksDataGridView.Rows.Add(row);
			}
		}

	}
}
