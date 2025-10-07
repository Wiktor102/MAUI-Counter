using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Counter.Models {
	public class CounterModel : INotifyPropertyChanged {
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

		private int _value;
		public int Value {
			get => _value;
			set {
				if (_value != value) {
					_value = value;
					OnPropertyChanged();
				}
			}
		}

		private int _initialValue;
		public int InitialValue {
			get => _initialValue;
			set {
				if (_initialValue != value) {
					_initialValue = value;
					OnPropertyChanged();
				}
			}
		}

		private string _color = string.Empty;
		public string Color {
			get => _color;
			set {
				if (_color != value) {
					_color = value;
					OnPropertyChanged();
				}
			}
		}

		[XmlIgnore]
		public ICommand? IncrementCommand { get; set; }
		[XmlIgnore]
		public ICommand? DecrementCommand { get; set; }
		[XmlIgnore]
		public ICommand? ResetCommand { get; set; }
		[XmlIgnore]
		public ICommand? RemoveCommand { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}