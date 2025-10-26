using Counter.ViewModels;

namespace Counter.Views;

public partial class CountersView : ContentPage {
	public CountersView(CountersViewModel countersViewModel, AddCounterFormViewModel addCounterFormViewModel) {
		InitializeComponent();
		BindingContext = countersViewModel;
		AddCounterFormView.BindingContext = addCounterFormViewModel;
	}
}