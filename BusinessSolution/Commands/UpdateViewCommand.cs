using System;
using System.Windows.Input;

namespace BusinessSolution
{
    public class UpdateViewCommand : ICommand
    {
        private AdminWindowViewModel adminWindowViewModel;
        private EmployeeWindowViewModel employeeWindowViewModel;
        //private EmployeeManagerViewModel employeeManagerViewModel;
        //private ProductManagerViewModel ProductManagerViewModel;
        //private AddEmployeeViewModel AddEmployeeViewModel;

        public UpdateViewCommand(AdminWindowViewModel adminWindowViewModel)
        {
            this.adminWindowViewModel = adminWindowViewModel;

        }
        public UpdateViewCommand(EmployeeWindowViewModel employeeWindowViewModel)
        {
            this.employeeWindowViewModel = employeeWindowViewModel;
        }
        //public UpdateViewCommand(EmployeeManagerViewModel employeeManagerViewModel)
        //{
        //    this.employeeManagerViewModel = employeeManagerViewModel;
        //}

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //if (parameter.ToString() == "EmployeeManager")
            //{
            //    adminWindowViewModel.SelectedViewModel = new EmployeeManagerViewModel();
            //}
            //if (parameter.ToString() == "ProductManager")
            //{
            //    employeeWindowViewModel.SelectedViewModel = new ProductManagerViewModel();
            //}
            //else if (parameter.ToString() == "Add")
            //{
            //    adminWindowViewModel.SelectedViewModel = new AddEmployeeViewModel();
            //}
        }
    }
}
