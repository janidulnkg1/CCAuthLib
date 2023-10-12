using CCAuthLib;
using CCAuthLib.IV;
using CCAuthLib.Key;
using Microsoft.Extensions.Configuration;

public class Program
{
    private readonly IEncryptionIVProvider _encryptionIVProvider;
    private readonly IEncryptionKeyProvider _encryptionKeyProvider;
    private readonly IConfiguration _configuration;
    public Program( IEncryptionKeyProvider encryptionKeyProvider, IEncryptionIVProvider encryptionIVProvider)
    {
      
        this._encryptionIVProvider = encryptionIVProvider;
        this._encryptionKeyProvider = encryptionKeyProvider;

        var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                   .Build();

        _configuration = configuration;
    }

    public static void Main(string[] args)
    {
        
    }
}
