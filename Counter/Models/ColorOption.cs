using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Counter.Models {
	public class ColorOption : INotifyPropertyChanged {
		private string _name = string.Empty;
		public string Name {
			get => _name;
			set {
				if (_name != value) {
					_name = value;
					OnPropertyChanged();
				}
			}
		}

		private string _hexCode = string.Empty;
		public string HexCode {
			get => _hexCode;
			set {
				if (_hexCode != value) {
					_hexCode = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _isSelected;
		public bool IsSelected {
			get => _isSelected;
			set {
				if (_isSelected != value) {
					_isSelected = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
