using Counter.Models;
using Counter.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Counter.ViewModels {
	public class CountersViewModel : INotifyPropertyChanged {
		private readonly DataService _dataService;
		public ObservableCollection<CounterModel> Counters { get; set; }

		// ViewModel for the add-counter form
		public AddCounterFormViewModel AddCounterFormModel { get; }

		public CountersViewModel(DataService dataService) {
			_dataService = dataService;
			Counters = _dataService.LoadCounters();

			AddCounterFormModel = new AddCounterFormViewModel();
			AddCounterFormModel.AddRequested += AddCounterFormModel_AddRequested; // TODO: Sprawdzić jak to szczegółowo działa

			foreach (var counter in Counters) {
				counter.PropertyChanged += Counter_PropertyChanged;
				SetCounterCommands(counter);
			}
		}

		private void AddCounterFormModel_AddRequested(object? sender, AddCounterFormViewModel.AddCounterEventArgs e) {
			var newCounter = new CounterModel {
				Name = e.Name,
				Value = e.InitialValue,
				InitialValue = e.InitialValue,
				Color = e.ColorHex
			};

			newCounter.PropertyChanged += Counter_PropertyChanged;
			SetCounterCommands(newCounter);
			Counters.Add(newCounter);
			_dataService.SaveCounters(Counters);
		}

		private void SetCounterCommands(CounterModel counter) {
			counter.IncrementCommand = new Command<CounterModel>(IncrementCounter);
			counter.DecrementCommand = new Command<CounterModel>(DecrementCounter);
			counter.ResetCommand = new Command<CounterModel>(ResetCounter);
			counter.RemoveCommand = new Command<CounterModel>(RemoveCounter);
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