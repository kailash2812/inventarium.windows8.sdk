using System;
using Inventarium.Windows8.SDK.Configuration;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Inventarium.Windows8.SDK
{
    public class InventariumFeedback
    {
        public const string DefaultTitle = "Inventarium";
        public const string EmbedType = "windows8";
        // using non-ssl version.
        internal const string AppHost = @"http://mapp.inventarium.mobi";
        private string CustomerKey { get; set; }

        private readonly FlyoutCustomization flyoutCustomization;

        public InventariumFeedback(string customerKey, FlyoutCustomization flyoutCustomization = null)
        {
            if (string.IsNullOrEmpty(customerKey))
                throw new ArgumentException("customerKey must be specified");

            CustomerKey = customerKey;

            if (flyoutCustomization == null)
            {
                this.flyoutCustomization = new FlyoutCustomization(
                    new SolidColorBrush(Colors.White),
                    new SolidColorBrush(Colors.Black),
                    DefaultTitle,
                    FlyoutDimension.Wide);
            }
            else
            {
                this.flyoutCustomization = flyoutCustomization;
            }
        }

        /// <summary>
        /// Open feedback page
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="userFullName">Full name of the user.</param>
        public void Start(string userEmail, string userFullName)
        {
            if (string.IsNullOrEmpty(userFullName))
                throw new ArgumentException("userFullName must be specified");

            var url = GenerateUrl(userEmail, userFullName);

            var inventariumFlyout = new InventariumFlyout(
                this.flyoutCustomization.Foreground,
                this.flyoutCustomization.Background,
                this.flyoutCustomization.Title,
                this.flyoutCustomization.Dimension,
                url,
                this.flyoutCustomization.Image
                );

            inventariumFlyout.Show();
        }

        public void StartAnonymously()
        {
            var user = GenerateRandomIdentity();
            Start(user.Email, user.Name);
        }

        private static User GenerateRandomIdentity()
        {
            var rnd = new Random();
            var num = rnd.Next(10, 65365);

            return new User
            {
                Name = string.Format("User {0}", num),
                Email = string.Format("user{0}@anonymous.com", num)
            };
        }

        private string GenerateUrl(string email, string fullName)
        {
            var url = string.Format("{0}/reg?customerKey={1}&email={2}&fullName={3}&embedType={4}",
                           AppHost,
                           Uri.EscapeDataString(CustomerKey),
                           Uri.EscapeDataString(email),
                           Uri.EscapeDataString(fullName),
                           Uri.EscapeDataString(EmbedType));

            return url;
        }
    }

    internal class User
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
