using Counter.ViewModels;

namespace Counter.Views;

public partial class AddCounterFormView : ContentView {
	public AddCounterFormView() {
		InitializeComponent();
	}

	private void OnAdd(object? sender, EventArgs e) {
		if (BindingContext is AddCounterFormViewModel vm) {
			vm.Add();
		}
	}
}
