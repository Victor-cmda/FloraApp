using FloraApp.ViewModels;

namespace FloraApp.Pages;

public partial class EmployeePage : ContentPage
{
    public EmployeePage(EmployeeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}