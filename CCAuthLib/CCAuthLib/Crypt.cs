using System.Security.Cryptography;
using System.Text;
using CCAuthLib.Key;
using CCAuthLib.IV;
using CCAuthLib.Logging;

namespace CCAuthLib
{
    public class Crypt
    {

        public static readonly Guid _fallbackIV = Guid.NewGuid();


        public required IKeyProvider keyProvider;
        public IProviderIV? ivProvider;
        public ILogger? logger;



        public byte[] Encrypt(byte[] inputData)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    string key = keyProvider.GetKey(); // Getting encryption key as a string
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key); // Converting the string key to bytes

                    Guid customIV = ivProvider.GetIV();
                    byte[] iv = customIV.HasValue ? customIV.Value.ToByteArray() : _fallbackIV.ToByteArray();
                    byte[] encryptedBytes;

                    using (var encryptor = aes.CreateEncryptor(keyBytes, iv))
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(iv, 0, iv.Length);
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(inputData, 0, inputData.Length);
                            cs.FlushFinalBlock(); // Flushing the final block
                        }

                        encryptedBytes = ms.ToArray();
                    }
                    return encryptedBytes;
                }
            }
            catch (Exception ex)
            {
                logger.Log("Encryption Failed!" +ex);
                throw;
            }
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    string key = keyProvider.GetKey(); // Getting the key as a string
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key); // Converting the string key to bytes
                    byte[] iv = new byte[aes.IV.Length];
                    byte[] decryptedBytes;

                    Array.Copy(encryptedData, iv, iv.Length);

                    using (var decryptor = aes.CreateDecryptor(keyBytes, iv))
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(encryptedData, iv.Length, encryptedData.Length - iv.Length);
                            cs.FlushFinalBlock(); // Flushing the final block
                        }

                        decryptedBytes = ms.ToArray();
                    }

                    return decryptedBytes;
                }
            }
            catch (Exception ex)
            {
                logger.Log("Decryption Failed!" + ex);
                throw;
            }
        }
    }
}