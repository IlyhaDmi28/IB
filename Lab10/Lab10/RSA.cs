using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public static class RSA
    {
        public static (RSAParameters publicKey, RSAParameters privateKey) GenerateRSAKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                return (rsa.ExportParameters(true), rsa.ExportParameters(true));
            }
        }

        public static byte[] RSAEncrypt(string plainText, RSAParameters publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                var bytes = Encoding.UTF8.GetBytes(plainText);
                return rsa.Encrypt(bytes, false);
            }
        }

        public static string RSADecrypt(byte[] cipherText, RSAParameters privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                var decryptedBytes = rsa.Decrypt(cipherText, false);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
