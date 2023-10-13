using CCAuthLib.IV;
using CCAuthLib.Key;
using CCAuthLib.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

public class Program
{
    // Initialize the necessary components
    private readonly IEncryptionKeyProvider _keyProvider;
    private readonly IEncryptionIVProvider _ivProvider;
    private readonly ILogger _logger;

    public Program (IEncryptionIVProvider iVProvider, IEncryptionKeyProvider keyProvider, ILogger logger)
    {
        this._keyProvider = keyProvider;
        this._ivProvider = iVProvider;
        _logger = logger;
    }

    static void Main(string[] args)
    {
         

        //  32-byte key and 16-byte IV
        byte[] encryptionKey = Encoding.UTF8.GetBytes("Your32ByteEncryptionKey");
        byte[] encryptionIV = Encoding.UTF8.GetBytes("Your16ByteEncryptionIV");

        keyProvider.SetEncryptionKey(encryptionKey);
        ivProvider.SetEncryptionIV(encryptionIV);

        var aesEncryptionProvider = new AesEncryptionProvider(keyProvider, ivProvider, logger);

        // Data to encrypt
        string dataToEncrypt = "Hello, world!";
        byte[] dataBytes = Encoding.UTF8.GetBytes(dataToEncrypt);

        // Encrypt the data
        byte[] encryptedData = aesEncryptionProvider.Encrypt(dataBytes);

        if (encryptedData != null)
        {
            Console.WriteLine("Data encrypted successfully:");
            Console.WriteLine(Convert.ToBase64String(encryptedData));

            // Decrypt the data
            byte[] decryptedData = aesEncryptionProvider.Decrypt(encryptedData);

            if (decryptedData != null)
            {
                string decryptedText = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine("Decrypted data: " + decryptedText);
            }
            else
            {
                Console.WriteLine("Decryption failed.");
            }
        }
        else
        {
            Console.WriteLine("Encryption failed.");
        }
    }
}
