using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using CCAuthLib;
using CCAuthLib.IV;
using CCAuthLib.Key;
using CCAuthLib.Logging;

namespace YourApplicationNamespace
{
    public class Program
    {
        private readonly IEncryptionIVProvider _encryptionIVProvider;
        private readonly IEncryptionKeyProvider _encryptionKeyProvider;
        private readonly IConfiguration _configuration;

        public Program(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();

            // Retrieve the encryption key and IV from the configuration
            byte[] encryptionKey = Encoding.UTF8.GetBytes(configuration["EncryptionKey"]);
            byte[] encryptionIV = Encoding.UTF8.GetBytes(configuration["EncryptionIV"]);



            IEncryptionIVProvider ivProvider = _encryptionIVProvider.SetEncryptionIV(encryptionIV);
            IEncryptionKeyProvider keyProvider = _encryptionKeyProvider.SetEncryptionKey(encryptionKey);



            AesEncryptionProvider encryptionProvider = new AesEncryptionProvider(keyProvider, ivProvider);

            // Input data
            string originalData = "This is a test message.";
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalData);

            // Encrypt
            byte[] encryptedData = encryptionProvider.Encrypt(originalBytes);
            Console.WriteLine("Encrypted Data: " + Convert.ToBase64String(encryptedData));

            // Decrypt
            byte[] decryptedData = encryptionProvider.Decrypt(encryptedData);
            if (decryptedData != null)
            {
                string decryptedText = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine("Decrypted Data: " + decryptedText);
            }
            else
            {
                Console.WriteLine("Decryption failed.");
            }
        }


    }
}
