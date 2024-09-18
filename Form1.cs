namespace WinFormsApp1
{
	/*
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
	}
	*/

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			// Add a button to the form.
			Button button1 = new Button();
			button1.Text = "Click Me";
			button1.Location = new Point(140, 100);
			button1.Size = new Size(100, 50);
			button1.Click += new EventHandler(button1_Click);
			Controls.Add(button1);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Hello World!");
		}
	}
}
