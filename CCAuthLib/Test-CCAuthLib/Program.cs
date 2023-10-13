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

        public Program(IEncryptionKeyProvider encryptionKeyProvider, IEncryptionIVProvider encryptionIVProvider, IConfiguration configuration)
        {
            this._encryptionIVProvider = encryptionIVProvider;
            this._encryptionKeyProvider = encryptionKeyProvider;
            this._configuration = configuration;
        }

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();

            // Retrieve the encryption key and IV from the configuration
            byte[] encryptionKey = Encoding.UTF8.GetBytes(configuration["EncryptionKey"]);
            byte[] encryptionIV = Encoding.UTF8.GetBytes(configuration["EncryptionIV"]);

            var program = new Program(new EncryptionKeyProvider(), new EncryptionIVProvider(), configuration);

            byte[] encryptedData = program.EncryptData(encryptionKey, encryptionIV);
            byte[] decryptedData = program.DecryptData(encryptionKey, encryptionIV, encryptedData);


        }

        public byte[] EncryptData(byte[] encryptionKey, byte[] encryptionIV)
        {
            _encryptionKeyProvider.SetEncryptionKey(encryptionKey);
            _encryptionIVProvider.SetEncryptionIV(encryptionIV);

            var encryptionProvider = new AesEncryptionProvider(_encryptionKeyProvider, _encryptionIVProvider, new Logger());

           
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes("YourDataToEncrypt");

            // Encrypt the data
            byte[] encryptedData = encryptionProvider.Encrypt(dataToEncrypt);

            return encryptedData;
            Console.WriteLine(encryptedData);
        }

        public byte[] DecryptData(byte[] encryptionKey, byte[] encryptionIV, byte[] encryptedData)
        {
            _encryptionKeyProvider.SetEncryptionKey(encryptionKey);
            _encryptionIVProvider.SetEncryptionIV(encryptionIV);

            var encryptionProvider = new AesEncryptionProvider(_encryptionKeyProvider, _encryptionIVProvider, new Logger());

            // Decrypt the data
            byte[] decryptedData = encryptionProvider.Decrypt(encryptedData);

            return decryptedData;
            Console.WriteLine(decryptedData);
        }
    }
}
