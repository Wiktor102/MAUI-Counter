using Counter.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Counter.ViewModels {
    public class AddCounterFormViewModel : INotifyPropertyChanged {
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

            var args = new AddCounterEventArgs {
                Name = NewCounterName,
                InitialValue = initialValue,
                ColorHex = SelectedColor?.HexCode ?? "#000000"
            };

            AddRequested?.Invoke(this, args);

            // Clear the form
            NewCounterName = string.Empty;
            NewCounterInitialValue = string.Empty;
            if (SelectedColor != null) {
                SelectedColor.IsSelected = false;
                SelectedColor = null;
            }
        }

        public event EventHandler<AddCounterEventArgs>? AddRequested;

        public class AddCounterEventArgs : EventArgs {
            public string Name { get; set; } = string.Empty;
            public int InitialValue { get; set; }
            public string ColorHex { get; set; } = "#000000";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
