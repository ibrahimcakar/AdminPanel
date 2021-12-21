using Cogito.Services.Models;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Linq;

namespace AdminPanel.Services.Mail
{
    public partial class EmailSender : IEmailSender
    {
        #region Fields

        private readonly ISmtpBuilder _smtpBuilder;

        #endregion

        #region Ctor

        public EmailSender(ISmtpBuilder smtpBuilder)
        {
            _smtpBuilder = smtpBuilder;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create an file attachment for the specific download object from DB
        /// </summary>
        /// <param name="download">Attachment download (another attachment)</param>
        /// <returns>A leaf-node MIME part that contains an attachment.</returns>
        //protected MimePart CreateMimeAttachment(Download download)
        //{
        //    if (download is null)
        //        throw new ArgumentNullException(nameof(download));

        //    var fileName = !string.IsNullOrWhiteSpace(download.Filename) ? download.Filename : download.Id.ToString();

        //    return CreateMimeAttachment($"{fileName}{download.Extension}", download.DownloadBinary, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow);
        //}

        /// <summary>
        /// Create an file attachment for the specific file path
        /// </summary>
        /// <param name="filePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name</param>
        /// <returns>A leaf-node MIME part that contains an attachment.</returns>
        //protected MimePart CreateMimeAttachment(string filePath, string attachmentFileName = null)
        //{
        //    if (string.IsNullOrWhiteSpace(filePath))
        //        throw new ArgumentNullException(nameof(filePath));

        //    if (string.IsNullOrWhiteSpace(attachmentFileName))
        //        attachmentFileName = Path.GetFileName(filePath);

        //    return CreateMimeAttachment(
        //            attachmentFileName,
        //            _fileProvider.ReadAllBytes(filePath),
        //            _fileProvider.GetCreationTime(filePath),
        //            _fileProvider.GetLastWriteTime(filePath),
        //            _fileProvider.GetLastAccessTime(filePath));
        //}

        /// <summary>
        /// Create an file attachment for the binary data
        /// </summary>
        /// <param name="attachmentFileName">Attachment file name</param>
        /// <param name="binaryContent">The array of unsigned bytes from which to create the attachment stream.</param>
        /// <param name="cDate">Creation date and time for the specified file or directory</param>
        /// <param name="mDate">Date and time that the specified file or directory was last written to</param>
        /// <param name="rDate">Date and time that the specified file or directory was last access to.</param>
        /// <returns>A leaf-node MIME part that contains an attachment.</returns>
        protected MimePart CreateMimeAttachment(string attachmentFileName, byte[] binaryContent, DateTime cDate, DateTime mDate, DateTime rDate)
        {
            if (!ContentType.TryParse(MimeTypes.GetMimeType(attachmentFileName), out var mimeContentType))
                mimeContentType = new ContentType("application", "octet-stream");

            return new MimePart(mimeContentType)
            {
                FileName = attachmentFileName,
                Content = new MimeContent(new MemoryStream(binaryContent), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition
                {
                    CreationDate = cDate,
                    ModificationDate = mDate,
                    ReadDate = rDate
                }
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="replyTo">ReplyTo address</param>
        /// <param name="replyToName">ReplyTo display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachment)</param>
        /// <param name="headers">Headers</param>
        [Obsolete]
        public virtual void SendEmail(EmailAccount emailAccount, EmailProperties emailProperties)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(emailProperties.fromName, emailProperties.fromAddress));
            message.To.Add(new MailboxAddress(emailProperties.toName, emailProperties.toAddress));

            if (!string.IsNullOrEmpty(emailProperties.replyTo))
            {
                message.ReplyTo.Add(new MailboxAddress(emailProperties.replyToName, emailProperties.replyTo));
            }

            //BCC
            if (emailProperties.bcc != null)
            {
                foreach (var address in emailProperties.bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(new MailboxAddress(address.Trim()));
                }
            }

            //CC
            if (emailProperties.cc != null)
            {
                foreach (var address in emailProperties.cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                {
                    message.Cc.Add(new MailboxAddress(address.Trim()));
                }
            }

            //content
            message.Subject = emailProperties.subject;

            //headers
            if (emailProperties.headers != null)
                foreach (var header in emailProperties.headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

            var multipart = new Multipart("mixed")
            {
                new TextPart(TextFormat.Html) { Text = emailProperties.body }
            };

            //create the file attachment for this e-mail message
            //if (!string.IsNullOrEmpty(emailProperties.attachmentFilePath))
            //{
            //    multipart.Add(CreateMimeAttachment(emailProperties.attachmentFilePath, emailProperties.attachmentFileName));
            //}

            //another attachment?
            if (emailProperties.attachedDownloadId > 0)
            {
                //var download = _downloadService.GetDownloadById(attachedDownloadId);
                ////we do not support URLs as attachments
                //if (!download?.UseDownloadUrl ?? false)
                //{
                //    multipart.Add(CreateMimeAttachment(download));
                //}
            }

            message.Body = multipart;

            //send email
            using var smtpClient = _smtpBuilder.Build(emailAccount);
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }

        #endregion
    }
}
