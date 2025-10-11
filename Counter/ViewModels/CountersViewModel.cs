using Counter.Models;
using Counter.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Counter.ViewModels {
	public class CountersViewModel : INotifyPropertyChanged {
		private readonly DataService _dataService;
		public ObservableCollection<CounterModel> Counters { get; set; }

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

		public ICommand AddCounterCommand { get; }
		public ICommand SelectColorCommand { get; }

		public CountersViewModel(DataService dataService) {
			_dataService = dataService;
			Counters = _dataService.LoadCounters();

			AddCounterCommand = new Command(AddCounter);
			SelectColorCommand = new Command<ColorOption>(SelectColor);

			foreach (var counter in Counters) {
				counter.PropertyChanged += Counter_PropertyChanged;
				SetCounterCommands(counter);
			}
		}

		private void SetCounterCommands(CounterModel counter) {
			counter.IncrementCommand = new Command<CounterModel>(IncrementCounter);
			counter.DecrementCommand = new Command<CounterModel>(DecrementCounter);
			counter.ResetCommand = new Command<CounterModel>(ResetCounter);
			counter.RemoveCommand = new Command<CounterModel>(RemoveCounter);
		}

		private void AddCounter() {
			if (string.IsNullOrWhiteSpace(NewCounterName))
				return;

			var initialValue = 0;
			if (!string.IsNullOrWhiteSpace(NewCounterInitialValue)) {
				if (!int.TryParse(NewCounterInitialValue, out initialValue)) {
					initialValue = 0;
				}
			}

			var newCounter = new CounterModel {
				Name = NewCounterName,
				Value = initialValue,
				InitialValue = initialValue,
				Color = SelectedColor?.HexCode ?? "#000000"
			};

			newCounter.PropertyChanged += Counter_PropertyChanged;
			SetCounterCommands(newCounter);
			Counters.Add(newCounter);
			_dataService.SaveCounters(Counters);

			NewCounterName = string.Empty;
			NewCounterInitialValue = string.Empty;
		}

		private void SelectColor(ColorOption? option) {
			if (option == null)
				return;

			SelectedColor = option;
		}

		private void IncrementCounter(CounterModel counter) {
			if (counter != null) {
				counter.Value++;
			}
		}

		private void DecrementCounter(CounterModel counter) {
			if (counter != null) {
				counter.Value--;
			}
		}

		private void ResetCounter(CounterModel counter) {
			if (counter != null) {
				counter.Value = counter.InitialValue;
			}
		}

		private void RemoveCounter(CounterModel counter) {
			if (counter != null) {
				counter.PropertyChanged -= Counter_PropertyChanged;
				Counters.Remove(counter);
				_dataService.SaveCounters(Counters);
			}
		}

		private void Counter_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(CounterModel.Value)) {
				_dataService.SaveCounters(Counters);
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}