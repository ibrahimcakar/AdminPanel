namespace AdminPanel.Services.Infrastructure
{
    public interface IPasswordService
    {
        /// <summary>
        /// Gets plainText returns cipherText.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>cipherText</returns>
        public string EncryptString(string plainText);
        /// <summary>
        /// Gets cipherText returns plainText.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>plainText</returns>
        public string DecryptString(string cipherText);
        public string GeneratePassword();
    }
}
