using CCAuthLib.IV;
using CCAuthLib.Key;
using CCAuthLib.Logging;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

public class AesEncryptionProvider
{
    private IEncryptionKeyProvider _keyProvider;
    private IEncryptionIVProvider _ivProvider;
    private ILogger _logger;
    private IConfiguration _configuration;



    public AesEncryptionProvider(IEncryptionKeyProvider keyProvider, IEncryptionIVProvider ivProvider)
    {
        _keyProvider = keyProvider;
        _ivProvider = ivProvider;
  


        var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                    .Build();

        _configuration = configuration;
    }

    public byte[] Encrypt(byte[] data)
    {
        if (data == null || data.Length <= 0)
            throw new ArgumentNullException(nameof(data));

        using (Aes aesAlg = Aes.Create())
        {
            string IVfallback = _configuration["fallbackIV"];
            byte[] fallbackiv = Encoding.UTF8.GetBytes(IVfallback);

            byte[] ivProvider = _ivProvider.GetEncryptionIV() ?? fallbackiv;

            aesAlg.Key = _keyProvider.GetEncryptionKey();
            aesAlg.IV = ivProvider;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    try
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                        byte[] encryptedData = msEncrypt.ToArray();
                        return encryptedData;
                    }
                    catch (CryptographicException e)
                    {
                        _logger.Log("Encryption Failed!:" + e);
                        throw; 
                    }
                }
            }
        }
    }

    public byte[]? Decrypt(byte[] encryptedData)
    {
        if (encryptedData == null || encryptedData.Length <= 0)
            throw new ArgumentNullException(nameof(encryptedData));

        using (Aes aesAlg = Aes.Create())
        {
            string IVfallback = _configuration["fallbackIV"];
            byte[]? fallbackiv = Encoding.UTF8.GetBytes(IVfallback);

            byte[] ivProvider = _ivProvider.GetEncryptionIV() ?? fallbackiv;

            aesAlg.Key = _keyProvider.GetEncryptionKey();
            aesAlg.IV = ivProvider;

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    try
                    {
                        csDecrypt.Write(encryptedData, 0, encryptedData.Length);
                        csDecrypt.FlushFinalBlock();
                        byte[] decryptedData = msDecrypt.ToArray();
                        return decryptedData;
                    }
                    catch (CryptographicException e)
                    {
                        _logger.Log("Decryption failed!" + e);
                        return null;
                    }
                }
            }
        }
    }
}
