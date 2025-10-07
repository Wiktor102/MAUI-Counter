using Counter.ViewModels;

namespace Counter.Views;

public partial class CountersView : ContentPage {
	public CountersView(CountersViewModel viewModel) {
		InitializeComponent();
		BindingContext = viewModel;
	}
}