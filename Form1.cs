namespace WinFormsApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			// Add a button to the form.
			Button button1 = new Button
			{
				Text = "Click Me",
				Location = new Point(140, 100),
				Size = new Size(100, 50)
			};
			button1.Click += new EventHandler(Button1_Click);
			Controls.Add(button1);

			Console.WriteLine(">> Initialized.");
		}

		private void Button1_Click(object? sender, EventArgs e)
		{
			Console.WriteLine(">> Button clicked.");
			MessageBox.Show("Hello World!");
		}
	}
}
