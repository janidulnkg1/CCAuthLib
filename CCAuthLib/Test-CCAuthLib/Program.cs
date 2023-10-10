using System;
using System.IO;
using System.Text;
using CCAuthLib;
using CCAuthLib.IV;
using CCAuthLib.Key;
using CCAuthLib.Logging;
using Microsoft.Extensions.Configuration;

public class Program
{
    private readonly IKeyProvider _keyProvider;
    private readonly IProviderIV  _providerIV;
    private readonly ILogger _logger;

    public Program( IKeyProvider keyProvider)
    {
        _keyProvider = keyProvider;
    }

    public void Main()
    {

        // Get the encryption key from the configuration
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();

        string encryptionKey = configuration["EncryptionKey"];

        
        string plaintext = "Hello, World!";
        byte[] inputData = Encoding.UTF8.GetBytes(plaintext);

       
        _keyProvider.SetKey(encryptionKey);


        Crypt crypt = new Crypt.CryptBuilder()
         .SetKeyProvider(_keyProvider)
         .SetIVProvider(_providerIV)
         .SetLogger(_logger)
         .Build();

        // Encrypt the data.
        byte[] encryptedData = crypt.Encrypt(inputData);
        _logger.Log("Encryption Successful!");

        // Decrypt the data.
        byte[] decryptedData = crypt.Decrypt(encryptedData);
        _logger.Log("Decryption Successful!");

        // Convert the decrypted data back to a string.
        string decryptedText = Encoding.UTF8.GetString(decryptedData);

        // Display the results.
        Console.WriteLine("Original Text: " + plaintext);
        Console.WriteLine("Encrypted Data: " + BitConverter.ToString(encryptedData));
        Console.WriteLine("Decrypted Text: " + decryptedText);
    }
}
