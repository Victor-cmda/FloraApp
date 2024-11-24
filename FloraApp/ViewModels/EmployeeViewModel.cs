using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;

namespace FloraApp.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject
    {
        [ObservableProperty]
        private Employee employee;

        [ObservableProperty]
        private ObservableCollection<Employee> employees;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string errorMessage;

        public EmployeeViewModel()
        {
            Employee = new Employee();
            Employees = new ObservableCollection<Employee>();
        }

        [RelayCommand]
        private async Task SaveEmployee()
        {
            if (string.IsNullOrEmpty(Employee.Name) || string.IsNullOrEmpty(Employee.CPF))
            {
                ErrorMessage = "Nome e CPF são obrigatórios!";
                return;
            }

            try
            {
                IsBusy = true;
                // Aqui você implementará a lógica para salvar no banco de dados
                Employees.Add(Employee);
                Employee = new Employee();
                await Shell.Current.DisplayAlert("Sucesso", "Funcionário cadastrado com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
