using System;
using System.IO;
using System.Text;
using CCAuthLib;
using CCAuthLib.Key;
using Microsoft.Extensions.Configuration;

public class Program
{
    private readonly IKeyProvider _keyProvider;

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


        Crypt crypt = new Crypt();

        // Encrypt the data.
        byte[] encryptedData = crypt.Encrypt(inputData);

        // Decrypt the data.
        byte[] decryptedData = crypt.Decrypt(encryptedData);

        // Convert the decrypted data back to a string.
        string decryptedText = Encoding.UTF8.GetString(decryptedData);

        // Display the results.
        Console.WriteLine("Original Text: " + plaintext);
        Console.WriteLine("Encrypted Data: " + BitConverter.ToString(encryptedData));
        Console.WriteLine("Decrypted Text: " + decryptedText);
    }
}
