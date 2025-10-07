
using System.Globalization;

namespace Counter.Converters {
	public class StringHexToColorConverter : IValueConverter {
		public object Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
			if (value is string hexCode) {
				return Color.FromArgb(hexCode);
			}
			return Colors.Black;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture) {
			throw new NotImplementedException();
		}
	}
}
