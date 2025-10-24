using Counter.Models;
using Counter.ViewModels;
using System.Collections.ObjectModel;

namespace Counter.Views {
	public partial class ColorSelectorView : ContentView {
		// Kolekcja obserwowalna, bo umożliwa dynamiczną aktualizację UI przy zmianach w kolekcji
		public ObservableCollection<ColorOption> Options { get; } = [];

		public ColorSelectorView() {
			InitializeComponent();

			// Default options
			Options.Clear();
			Options.Add(new ColorOption { Name = "Sea Blue", HexCode = "#3298c7" });
			Options.Add(new ColorOption { Name = "Sunset Orange", HexCode = "#F97316" });
			Options.Add(new ColorOption { Name = "Forest Green", HexCode = "#15803D" });
			Options.Add(new ColorOption { Name = "Royal Purple", HexCode = "#7C3AED" });
			Options.Add(new ColorOption { Name = "Slate Gray", HexCode = "#475569" });
		}

		private void OnColorTapped(object? sender, TappedEventArgs e) {
			if (e.Parameter is ColorOption color && BindingContext is AddCounterFormViewModel vm) {
				vm.SelectColor(color);
			}
		}
	}
}
