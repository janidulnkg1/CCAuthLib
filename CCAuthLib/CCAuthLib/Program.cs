using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CCAuthLib.IV;
using CCAuthLib.Key;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CCAuthLib
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("/Logs/CCAuthLib.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            IEncryptionKeyProvider keyProvider = new EncryptionKeyProvider();
            IEncryptionIVProvider ivProvider = new EncryptionIVProvider();

            AesEncryptionProvider aesProvider = new AesEncryptionProvider(keyProvider, ivProvider);

            // Example data to encrypt
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes("TEST message");

            // Encrypt data
            byte[] encryptedData = aesProvider.Encrypt(dataToEncrypt);

            // Display encrypted data
            Log.Information("Encrypted Data: " + Convert.ToBase64String(encryptedData));

            // Decrypt data
            byte[] decryptedData = aesProvider.Decrypt(encryptedData);

            if (decryptedData != null)
            {
                // Display decrypted data
                string decryptedText = Encoding.UTF8.GetString(decryptedData);
                Log.Information("Decrypted Data: " + decryptedText);
            }
            else
            {
                Log.Error("Decryption failed!");
            }
        }
    }
}