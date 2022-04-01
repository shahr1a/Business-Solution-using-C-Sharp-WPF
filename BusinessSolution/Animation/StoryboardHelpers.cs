using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace BusinessSolution
{
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Add a slide and fade in animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="declarationRation">The rate of deceleration</param>
        public static void AddSlideFromRight(this Storyboard sb, float seconds, double offset, float declarationRation = 0.9f)
        { }
            
    }
}
