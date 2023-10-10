using CCAuthLib.IV;
using CCAuthLib.Key;
using CCAuthLib.Logging;
using System;

namespace CCAuthLib
{
    public class CryptBuilder
    {
        public required IKeyProvider _keyProvider;
        public required IProviderIV _ivProvider;
        public required ILogger _logger;

        public CryptBuilder SetKeyProvider(IKeyProvider provider)
        {
            _keyProvider = provider;
            return this;
        }

        public CryptBuilder SetIVProvider(IProviderIV provider)
        {
            _ivProvider = provider;
            return this;
        }

        public CryptBuilder SetLogger(ILogger logger)
        {
            this._logger = logger;
            return this;
        }

        public Crypt Build()
        {
            Crypt crypt = new Crypt
            {
                keyProvider = _keyProvider,
                ivProvider = _ivProvider,
                logger = _logger
            };
            return crypt;
        }
    }
}
