
using Counter.Models;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Counter.Services {
	public class DataService {
		private const string FileName = "counters.xml";
		private readonly string _filePath;

		public DataService() {
			_filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
		}

		public void SaveCounters(ObservableCollection<CounterModel> counters) {
			var serializer = new XmlSerializer(typeof(ObservableCollection<CounterModel>));
			using (var writer = new StreamWriter(_filePath)) {
				serializer.Serialize(writer, counters);
			}
		}

		public ObservableCollection<CounterModel> LoadCounters() {
			if (!File.Exists(_filePath))
				return new ObservableCollection<CounterModel>();

			var serializer = new XmlSerializer(typeof(ObservableCollection<CounterModel>));
			using (var reader = new StreamReader(_filePath)) {
				var counters = (ObservableCollection<CounterModel>?)serializer.Deserialize(reader);
				return counters ?? new ObservableCollection<CounterModel>();
			}
		}
	}
}
