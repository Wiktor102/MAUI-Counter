using Counter.Models;
using Counter.ViewModels;
using System.Collections.ObjectModel;

namespace Counter.Views {
	public partial class ColorSelectorView : ContentView {
		public ObservableCollection<ColorOption> Options { get; } = [];

		public ColorSelectorView() {
			InitializeComponent();

			// Seed default options
			Options.Clear();
			Options.Add(new ColorOption { Name = "Sea Blue", HexCode = "#3298c7" });
			Options.Add(new ColorOption { Name = "Sunset Orange", HexCode = "#F97316" });
			Options.Add(new ColorOption { Name = "Forest Green", HexCode = "#15803D" });
			Options.Add(new ColorOption { Name = "Royal Purple", HexCode = "#7C3AED" });
			Options.Add(new ColorOption { Name = "Slate Gray", HexCode = "#475569", IsSelected = true });
		}

		protected override void OnBindingContextChanged() {
			base.OnBindingContextChanged();

			// If the host VM provides a SelectColorCommand, execute with default selected
			if (BindingContext is CountersViewModel vm) {
				var selected = Options.FirstOrDefault(o => o.IsSelected);
				if (selected != null) vm.SelectColorCommand.Execute(selected);
			}
		}
	}
}
