using Counter.Models;
using Counter.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Counter.ViewModels {
	public class CountersViewModel : INotifyPropertyChanged {
		private readonly DataService _dataService;

		public ObservableCollection<CounterItemViewModel> Counters { get; set; }

		public CountersViewModel(DataService dataService) {
			_dataService = dataService;

			// Load counter models and wrap them in ViewModels
			var counterModels = _dataService.LoadCounters();
			Counters = new ObservableCollection<CounterItemViewModel>(
				counterModels.Select(CreateCounterItemViewModel)
			);
		}

		public void AddCounter(string name, int initialValue, string colorHex) {
			var newModel = new CounterModel {
				Name = name,
				Value = initialValue,
				InitialValue = initialValue,
				Color = colorHex
			};

			var counterItemViewModel = CreateCounterItemViewModel(newModel);
			Counters.Add(counterItemViewModel);
			SaveCounters();
		}

		private CounterItemViewModel CreateCounterItemViewModel(CounterModel model) {
			return new CounterItemViewModel(
				model,
				onCounterChanged: _ => SaveCounters(),
				onCounterRemoved: RemoveCounter
			);
		}

		private void RemoveCounter(CounterModel model) {
			var itemToRemove = Counters.FirstOrDefault(c => c.Model == model);
			if (itemToRemove != null) {
				Counters.Remove(itemToRemove);
				SaveCounters();
			}
		}

		private void SaveCounters() {
			var models = new ObservableCollection<CounterModel>(
				Counters.Select(c => c.Model)
			);
			_dataService.SaveCounters(models);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}