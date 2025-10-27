using Counter.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Counter.ViewModels {
	public class AddCounterFormViewModel(Action<string, int, string> onAddCounter) : INotifyPropertyChanged {
		private readonly Action<string, int, string> _onAddCounter = onAddCounter;
		private string _newCounterName = string.Empty;
		public string NewCounterName {
			get => _newCounterName;
			set {
				if (_newCounterName != value) {
					_newCounterName = value;
					OnPropertyChanged();
				}
			}
		}

		private string _newCounterInitialValue = string.Empty;
		public string NewCounterInitialValue {
			get => _newCounterInitialValue;
			set {
				if (_newCounterInitialValue != value) {
					_newCounterInitialValue = value;
					OnPropertyChanged();
				}
			}
		}

		private ColorOption? _selectedColor;
		public ColorOption? SelectedColor {
			get => _selectedColor;
			set {
				if (_selectedColor != value) {
					if (_selectedColor != null) {
						_selectedColor.IsSelected = false;
					}

					_selectedColor = value;
					if (_selectedColor != null) {
						_selectedColor.IsSelected = true;
					}

					OnPropertyChanged();
				}
			}
		}

		public void SelectColor(ColorOption? option) {
			if (option == null) return;
			SelectedColor = option;
		}

		public void Add() {
			if (string.IsNullOrWhiteSpace(NewCounterName)) return;

			var initialValue = 0;
			if (!string.IsNullOrWhiteSpace(NewCounterInitialValue)) {
				if (!int.TryParse(NewCounterInitialValue, out initialValue)) {
					initialValue = 0;
				}
			}

			var colorHex = SelectedColor?.HexCode ?? "#ffffff";

			// Call the callback to notify that a counter should be added
			_onAddCounter(NewCounterName, initialValue, colorHex);

			// Clear the form
			NewCounterName = string.Empty;
			NewCounterInitialValue = string.Empty;
			if (SelectedColor != null) {
				SelectedColor.IsSelected = false;
				SelectedColor = null;
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}