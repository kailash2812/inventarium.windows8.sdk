﻿/* 
 * This control is based on http://kuchenzeit.wordpress.com/2012/03/26/flyout-control-for-windows-8-metro-xamlc/
 * recoded by inventarium team =)
 */

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Inventarium.Windows8.SDK
{
    /// <summary>
    /// Provides a 'InventariumFlyout' control with customizable Content, Theme and Title
    /// </summary>
    public sealed partial class InventariumFlyout : UserControl
    {
        //OnClosing
        public delegate void AddOnClosingDelegate(object sender, CloseReason reason);
        public event AddOnClosingDelegate OnClosing;
        public void OnClosingEvent(object sender, CloseReason reason)
        {
            try { OnClosing(sender, reason); }
            catch { }
        }

        private FlyoutDimension Dimension { get; set; }

        /// <summary>
        /// Create an InventariumFlyout.
        /// </summary>
        /// <param name="foreground">The color of Text and Border, NOTE: the BackButton color binds to the ApplicationTextBrush, so it's best to stick to that too.</param>
        /// <param name="background">Color of Background.</param>
        /// <param name="title">Header/Title</param>
        /// <param name="dimension">Width -> Narrow of Wide</param>
        /// <param name="url">url to open in browser.</param>
        /// <param name="image">optional: display an image next to the header</param>
        public InventariumFlyout(
            Brush foreground,
            Brush background,
            string title,
            FlyoutDimension dimension,
            string url,
            BitmapImage image = null)
        {
            this.InitializeComponent();
            this.Dimension = dimension;
            //to handle app activation -> close
            Window.Current.Activated += OnWindowActivated;
            //prepare the frame
            mainBorder.Width = (int)dimension;
            mainBorder.Height = Window.Current.Bounds.Height;
            //fill in the content
            flyoutTitle.Text = title;
            //contentPanel.Children.Add(content);
            smallImage.Source = image;
            //brush the controls according to the parameters
            mainBorder.BorderBrush = foreground;
            flyoutTitle.Foreground = foreground;
            mainFrame.Background = background;

            progressRing.IsActive = true;
            webView.LoadCompleted += OnWebViewOnLoadCompleted;

            webView.Navigate(new Uri(url));
        }

        private void OnWebViewOnLoadCompleted(object sender, NavigationEventArgs args)
        {
            webView.Visibility = Visibility.Visible;
            progressRing.IsActive = false;
            //loadingStatus.Visibility = Visibility.Collapsed;
        }

        //show/hide
        public void Show()
        {
            flyoutPopup.IsOpen = true;//open the flyout
            Canvas.SetLeft(flyoutPopup, Window.Current.Bounds.Width - (int)Dimension);//move it into the screen
        }
        public void Hide(CloseReason reason)
        {
            Dispose();
            OnClosingEvent(this, reason);
            Window.Current.Activated -= OnWindowActivated;//stop listening to app activation
            Canvas.SetLeft(flyoutPopup, Window.Current.Bounds.Width);//move it out of the screen
        }
        private void Dispose()
        {
            contentPanel.Children.Clear();//let go of the content control
        }

        //close reasons
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)//if the user re-activates the application, the flyout shall be closed
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                Hide(CloseReason.Other);//hide, so that the transition is reset
            }
        }
        private void OnPopupClosed(object sender, object e)//if the flyout is closed by 'light dismissal'
        {
            Hide(CloseReason.LightDismissal);//hide, so that the transition is reset            
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)//if the flyout is closed via the back button
        {
            Hide(CloseReason.BackButton);//hide, so that the transition is reset
        }
    }

    /// <summary>
    /// Possible flyout dimensions.
    /// </summary>
    public enum FlyoutDimension
    {
        Wide = 646,
        Narrow = 346
    }

    /// <summary>
    /// Reasons for a InventariumFlyout being closed.
    /// </summary>
    public enum CloseReason
    {
        BackButton,
        LightDismissal,
        Other
    }
}
