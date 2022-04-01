using System.Windows.Input;

namespace BusinessSolution
{
    public class EmployeeManagerViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// The page which is selected
        /// </summary>
        private BaseViewModel selectedViewModel;

        #endregion

        #region Public Properties
        /// <summary>
        /// Selected page to view
        /// </summary>
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        #endregion

        #region Constructor

        public EmployeeManagerViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }

        #endregion
    }
}
