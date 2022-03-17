using ReactiveUI;
using System.Text.RegularExpressions;

namespace VisualProgLab5.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		string InputText = "", OutputText = "", regex = "";
		public string MyRegex
		{
			get => regex;
			set
			{
				if (value != null)
				{
					regex = value;
				}
			}
		}
		public string Output
		{
			get => OutputText;
			set
			{
				if (MyRegex == "")
				{
					this.RaiseAndSetIfChanged(ref OutputText, "Error");
				}
				else
				{
					this.RaiseAndSetIfChanged(ref OutputText, value);
				}
			}
		}
		public string Input
		{
			get => InputText;
			set
			{
				InputText = "";
				if (MyRegex != "")
				{
					Regex rgx = new Regex(MyRegex);
					foreach (Match match in rgx.Matches(value))
					{
						Output += match.Value + "\n";
					}
					if (Output == "")
					{
						Output = "Not found";
					}
				}
				this.RaiseAndSetIfChanged(ref InputText, value);
			}
		}
	}
}
