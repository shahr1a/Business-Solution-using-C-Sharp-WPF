using BusinessSolution;
using System;
using System.Diagnostics;
using System.Globalization;

namespace BusinessSolution
{
    public class ApplicationPageValueConverter : BaseValueConverters<ApplicationPageValueConverter>
    {
        /// <summary>
        /// Converts the <see cref="ApplicationPage"/> to an actual view /page
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                // Find the appropiate page
                case ApplicationPage.WelcomePage:
                    return new WelcomePage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
