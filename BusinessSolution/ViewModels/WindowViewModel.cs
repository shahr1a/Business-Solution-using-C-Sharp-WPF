using BusinessSolution;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using System;

namespace BusinessSolution
{
    /// <summary>
    /// The view model for the custom flat window
    /// </summary>
    class WindowViewModel : BaseViewModel
    {
        #region Private Member

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window window;

        /// <summary>
        /// The margin around the windows to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;
       

        #endregion

        #region Public Properties

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>

       
        
        
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The margin around the windows to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize 
        { 
            get
            {
                return window.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return window.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// the height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// the height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder);}}

        /// <summary>
        /// The welcome page of the applicaiton
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.WelcomePage;
        public ApplicationPage LoginPage { get; set; } = ApplicationPage.Login;

       

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }
        public object WindowDockPosiiton { get; private set; }



        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this.window = window;

            // Listen out for the window resizing
            this.window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            // Create Commands
            MinimizeCommand = new RelayCommand(() => this.window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => this.window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => this.window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(this.window, GetMousePosition()));

            // Fix Window Resize issue
            var resizer = new WindowResizer(this.window);

        }

        #endregion

        #region Private Helpers


        /// <summary>
        /// Gets the current mouse poisiton on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // position of the mouse relative to the window
            var position = Mouse.GetPosition(this.window);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + this.window.Left, position.Y + this.window.Top);
        }

        #endregion

        #region EventHandler

        public void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.window.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
