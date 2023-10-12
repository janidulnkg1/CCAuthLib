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



    public AesEncryptionProvider(IEncryptionKeyProvider keyProvider, IEncryptionIVProvider ivProvider, ILogger logger, IConfiguration configuration)
    {
        _keyProvider = keyProvider;
        _ivProvider = ivProvider;
        _logger = logger;
        _configuration = configuration;

        IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("configuration.json", optional: true, reloadOnChange: true);

        IConfigurationBuilder _config = (IConfigurationBuilder)builder.Build(); 

        
    }

 
    public byte[]? Encrypt(byte[] data)
    {
        if (data == null || data.Length <= 0)
            throw new ArgumentNullException(nameof(data));

        using (Aes aesAlg = Aes.Create())
        {
            string IVfallback = _configuration["fallbackIV"];
            byte[]? fallbackiv = Encoding.UTF8.GetBytes(IVfallback);

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
                        return msEncrypt.ToArray();
                    }
                    catch (CryptographicException e)
                    {
                        _logger.Log("Encryption Failed!:" +e);
                        return null;
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
                        return msDecrypt.ToArray();
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
