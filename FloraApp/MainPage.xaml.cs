namespace FloraApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSalesClicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Em desenvolvimento", "Função em desenvolvimento", "OK");
        }

        private async void OnProductsClicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Em desenvolvimento", "Função em desenvolvimento", "OK");
        }

        private async void OnEmployeesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//EmployeePage");
        }

        private async void OnReportsClicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Em desenvolvimento", "Função em desenvolvimento", "OK");
        }
    }
}
