using System;

namespace Inventarium.Windows8.TestApp.SDK
{
    //todo: move to separate lib

    public class InventariumFeedback
    {
        public const string EmbedType = "windows8";
        // instead using non-ssl version.
        internal const string AppHost = @"http://mapp.inventarium.mobi";
        private string CustomerKey { get; set; }
        private const string KEY_ANONYM_USER = ".INVENTARIUM_KEY_ANONYM_USER";

        public InventariumFeedback(string customerKey)
        {
            if (string.IsNullOrEmpty(customerKey))
                throw new ArgumentException("customerKey must be specified");

            CustomerKey = customerKey;
        }

        /// <summary>
        /// Open feedback page
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="userFullName">Full name of the user.</param>
        public Uri Start(string userEmail, string userFullName)
        {
            if (string.IsNullOrEmpty(userFullName))
                throw new ArgumentException("userFullName must be specified");

            return new Uri(GenerateUrl(userEmail, userFullName));
        }

        public Uri StartAnonymously()
        {
            var user = GenerateRandomIdentity();
            return Start(user.Email, user.Name);
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
