using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Counter.Models;
using Counter.Services;

namespace Counter.ViewModels
{
    public class CountersViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        public ObservableCollection<CounterModel> Counters { get; set; }

        private string _newCounterName = string.Empty;
        public string NewCounterName
        {
            get => _newCounterName;
            set
            {
                if (_newCounterName != value)
                {
                    _newCounterName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newCounterInitialValue = string.Empty;
        public string NewCounterInitialValue
        {
            get => _newCounterInitialValue;
            set
            {
                if (_newCounterInitialValue != value)
                {
                    _newCounterInitialValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newCounterColor = string.Empty;
        public string NewCounterColor
        {
            get => _newCounterColor;
            set
            {
                if (_newCounterColor != value)
                {
                    _newCounterColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddCounterCommand { get; }

        public CountersViewModel(DataService dataService)
        {
            _dataService = dataService;
            Counters = _dataService.LoadCounters();

            AddCounterCommand = new Command(AddCounter);

            foreach (var counter in Counters)
            {
                counter.PropertyChanged += Counter_PropertyChanged;
                SetCounterCommands(counter);
            }
        }

        private void SetCounterCommands(CounterModel counter)
        {
            counter.IncrementCommand = new Command<CounterModel>(IncrementCounter);
            counter.DecrementCommand = new Command<CounterModel>(DecrementCounter);
            counter.ResetCommand = new Command<CounterModel>(ResetCounter);
            counter.RemoveCommand = new Command<CounterModel>(RemoveCounter);
        }

        private void AddCounter()
        {
            if (string.IsNullOrWhiteSpace(NewCounterName))
                return;

            var initialValue = 0;
            if (!string.IsNullOrWhiteSpace(NewCounterInitialValue))
            {
                int.TryParse(NewCounterInitialValue, out initialValue);
            }

            var newCounter = new CounterModel
            {
                Name = NewCounterName,
                Value = initialValue,
                InitialValue = initialValue,
                Color = string.IsNullOrWhiteSpace(NewCounterColor) ? "#000000" : NewCounterColor
            };

            newCounter.PropertyChanged += Counter_PropertyChanged;
            SetCounterCommands(newCounter);
            Counters.Add(newCounter);
            _dataService.SaveCounters(Counters);

            NewCounterName = string.Empty;
            NewCounterInitialValue = string.Empty;
            NewCounterColor = string.Empty;
        }

        private void IncrementCounter(CounterModel counter)
        {
            if (counter != null)
            {
                counter.Value++;
            }
        }

        private void DecrementCounter(CounterModel counter)
        {
            if (counter != null)
            {
                counter.Value--;
            }
        }

        private void ResetCounter(CounterModel counter)
        {
            if (counter != null)
            {
                counter.Value = counter.InitialValue;
            }
        }

        private void RemoveCounter(CounterModel counter)
        {
            if (counter != null)
            {
                counter.PropertyChanged -= Counter_PropertyChanged;
                Counters.Remove(counter);
                _dataService.SaveCounters(Counters);
            }
        }

        private void Counter_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CounterModel.Value))
            {
                _dataService.SaveCounters(Counters);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}