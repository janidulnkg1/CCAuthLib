using CCAuthLib.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Builder
{
    public class CryptBuilder
    {
        private IKeyProvider _keyProvider;

        public CryptBuilder SetKeyProvider(IKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
            return this;    
        }
        public Crypt Build()
        {
            if( _keyProvider == null)
            {
                throw new InvalidOperationException("Key must be set for Encryption / Decryption");
            }

            return new Crypt(_keyProvider);
        }

        internal void SetKeyProvider(KeyProvider keyProvider) 
        {
            throw new NotImplementedException();
        }
    }
}
