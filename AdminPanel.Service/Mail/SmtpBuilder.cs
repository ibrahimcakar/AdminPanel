using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AdminPanel.Service.Models;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace AdminPanel.Services.Mail
{
    public partial class SmtpBuilder : ISmtpBuilder
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Host"></param>
        /// <param name="Port"></param>
        /// <param name="EnableSsl"></param>
        /// <param name="UseDefaultCredentials"></param>
        /// <returns></returns>
        public virtual SmtpClient Build(EmailAccount emailAccount)
        {
            var client = new SmtpClient
            {
                ServerCertificateValidationCallback = ValidateServerCertificate
            };

            try
            {
                client.Connect(
                    emailAccount.Host,
                    emailAccount.Port,
                    emailAccount.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable);

                if (emailAccount.UseDefaultCredentials)
                {
                    client.Authenticate(CredentialCache.DefaultNetworkCredentials);
                }
                else if (!string.IsNullOrWhiteSpace(emailAccount.Username))
                {
                    client.Authenticate(new NetworkCredential(emailAccount.Username, emailAccount.Password));
                }

                return client;
            }
            catch (Exception ex)
            {
                client.Dispose();
                return null;
            }
        }

        /// <summary>
        /// Validates the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>A System.Boolean value that determines whether the specified certificate is accepted for authentication</returns>
        public virtual bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //By default, server certificate verification is disabled.
            return true;
        }

        #endregion
    }
}
