using Counter.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Counter.ViewModels {
	public class CounterItemViewModel : INotifyPropertyChanged {
		private readonly CounterModel _model;
		private readonly Action<CounterModel> _onCounterChanged;
		private readonly Action<CounterModel> _onCounterRemoved;

		public CounterItemViewModel(
			CounterModel model,
			Action<CounterModel> onCounterChanged,
			Action<CounterModel> onCounterRemoved) {
			_model = model;
			_onCounterChanged = onCounterChanged;
			_onCounterRemoved = onCounterRemoved;

			// Subskrybcja na zmiany w modelu
			_model.PropertyChanged += Model_PropertyChanged;

			IncrementCommand = new Command(Increment);
			DecrementCommand = new Command(Decrement);
			ResetCommand = new Command(Reset);
			RemoveCommand = new Command(Remove);
		}

		// Expose model properties
		public string Name => _model.Name;
		public int Value => _model.Value;
		public int InitialValue => _model.InitialValue;
		public string Color => _model.Color;

		public CounterModel Model => _model;

		// Commands
		public ICommand IncrementCommand { get; }
		public ICommand DecrementCommand { get; }
		public ICommand ResetCommand { get; }
		public ICommand RemoveCommand { get; }

		private void Increment() {
			_model.Value++;
			_onCounterChanged(_model);
		}

		private void Decrement() {
			_model.Value--;
			_onCounterChanged(_model);
		}

		private void Reset() {
			_model.Value = _model.InitialValue;
			_onCounterChanged(_model);
		}

		private void Remove() {
			_model.PropertyChanged -= Model_PropertyChanged;
			_onCounterRemoved(_model);
		}

		private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// Przekazanie dalej powiadomienia o zmianie właściwości do UI
			OnPropertyChanged(e.PropertyName);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
