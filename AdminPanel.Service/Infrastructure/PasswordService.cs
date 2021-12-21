using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AdminPanel.Services.Infrastructure
{
    public class PasswordService : IPasswordService
    {

        public string EncryptString(string plainText)
        {
            var inputText = Encoding.UTF8.GetBytes(plainText);
            string degisken = null;
            using (var rijndaelCipher = new RijndaelManaged())
            {
                var secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes("tBi2_YsU_p"), Encoding.ASCII.GetBytes("rQi_L41a_jP"));
                using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(inputText, 0, inputText.Length);
                            cryptoStream.FlushFinalBlock();
                            degisken = Convert.ToBase64String(memoryStream.ToArray());
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
        }
        public string DecryptString(string cipherText)
        {
            var encryptedData = Convert.FromBase64String(cipherText);
            var secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes("tBi2_YsU_p"), Encoding.ASCII.GetBytes("rQi_L41a_jP"));

            using (var rijndaelCipher = new RijndaelManaged())
            {
                using (var decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream(encryptedData))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainText = new byte[encryptedData.Length];
                            cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.UTF8.GetString(plainText).TrimEnd('\0');
                        }
                    }
                }
            }
        }

        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }
    }
}
