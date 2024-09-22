using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    public static class SHA
    {
        public static string HashSHA256(string rawData)
        {
            // Создание объекта SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Вычисление хеша
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Преобразование байтового массива в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateSalt(int length)
        {
            // Генерация случайной соли заданной длины
            byte[] saltBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}
