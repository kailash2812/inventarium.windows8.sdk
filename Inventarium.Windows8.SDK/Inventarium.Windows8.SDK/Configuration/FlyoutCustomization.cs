using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Inventarium.Windows8.SDK.Configuration
{
    public sealed class FlyoutCustomization
    {
        /// <summary>
        /// The color of Text and Border, NOTE: the BackButton color binds to the ApplicationTextBrush, so it's best to stick to that too
        /// </summary>
        public Brush Foreground { get; private set; }

        /// <summary>
        /// Color of Background.
        /// </summary>
        public Brush Background { get; private set; }

        /// <summary>
        /// Header/Title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Width -> Narrow of Wide
        /// </summary>
        public FlyoutDimension Dimension { get; private set; }

        /// <summary>
        ///  an image next to the header
        /// </summary>
        public BitmapImage Image { get; private set; }

        /// <summary>
        /// Create FlyoutCustomization
        /// </summary>
        /// <param name="foreground">The color of Text and Border, NOTE: the BackButton color binds to the ApplicationTextBrush, so it's best to stick to that too.</param>
        /// <param name="background">Color of Background.</param>
        /// <param name="title">Header/Title</param>
        /// <param name="dimension">Width -> Narrow of Wide</param>
        /// <param name="image">optional: display an image next to the header</param>
        public FlyoutCustomization(
            Brush foreground,
            Brush background,
            string title,
            FlyoutDimension dimension,
            BitmapImage image = null)
        {
            Foreground = foreground;
            Background = background;
            Title = title;
            Dimension = dimension;
            Image = image;
        }
    }
}
