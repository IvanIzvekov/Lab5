using Avalonia.Controls;
using VisualProgLab5.ViewModels;
using System.IO;

namespace VisualProgLab5.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.FindControl<Button>("OpenButtonDialog").Click += async delegate
			{
				var taskPath = new OpenFileDialog()
				{
					Title = "Open File",
					Filters = null
				}.ShowAsync((Window)this.VisualRoot);
				string[]? path = await taskPath;
				if (path != null)
				{
					var context = this.DataContext as MainWindowViewModel;
					var truepath = string.Join(@"\", path);
					context.Input = "";
					StreamReader reading = File.OpenText(truepath);
					string tmp;
					while ((tmp = reading.ReadLine()) != null)
					{
						context.Input += tmp + "\n";
					}
					reading.Close();
				}
			};
			this.FindControl<Button>("SetRegexButtonDialog").Click += async delegate
			{
				var context = this.DataContext as MainWindowViewModel;
				string? regex = await new RegexWindow(context.MyRegex).ShowDialog<string>((Window)this.VisualRoot);
				context.MyRegex = regex;
				context.Input = context.Input;
			};
			this.FindControl<Button>("SaveButtonDialog").Click += async delegate
			{
				var taskPath = new SaveFileDialog()
				{
					Title = "Save File",
					Filters = null
				}.ShowAsync((Window)this.VisualRoot);
				string? path = await taskPath;
				if (path != null)
				{
					var context = this.DataContext as MainWindowViewModel;
					var truepath = string.Join(@"\", path);
					StreamWriter writing = new StreamWriter(truepath);
					writing.WriteLine(context.Output);
					writing.Close();
				}
			};
		}
	}
}


