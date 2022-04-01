
using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System;

namespace BusinessSolution
{
    public class BasePage : Page
    {
        #region Public Properties
        /// <summary>
        /// The animation to play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation to play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasePage()
        {
            // If we are animating in, hide to begin with
            if (this.PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            this.Loaded += BasePage_Loaded;
        }

        #endregion

        #region Animation Load / Unload 

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Animate the page in
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            // Make sure we have something to do
            if (this.PageLoadAnimation == PageAnimation.None) 
                return;

            switch(this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    var storyboard = new Storyboard();
                    var slideAnimation = new ThicknessAnimation {
                        Duration = new Duration(TimeSpan.FromSeconds(this.SlideSeconds)),
                        From = new Thickness(this.WindowWidth, 0, -this.WindowWidth, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0.9f
                    };
                    Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
                    storyboard.Children.Add(slideAnimation);

                    storyboard.Begin(this);

                    this.Visibility = Visibility.Visible;
                    await Task.Delay((int)(this.SlideSeconds * 1000));
                    break;
            }
        }

        #endregion
    }
}
